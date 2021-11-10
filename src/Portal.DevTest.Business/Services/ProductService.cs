using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DevTest.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private StringBuilder _logsErros;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logsErros = new StringBuilder();
        }

        public async Task<ProductModel> GetById(Guid id)
        {
            var productResult = new ProductModel();
            productResult = await _productRepository.GetById(id);
            return productResult;
        }

        public async Task<StringBuilder> AddNew(ProductModel product)
        {
            product.Id = Guid.NewGuid();
            product.CreationDate = DateTime.Now;
            product.IsActive = true;

            try { await _productRepository.Add(product); }
            catch { return _logsErros.AppendFormat("An error occurred while trying to save"); }

            return _logsErros;
        }

        public List<ProductModel> Search(ProductsFilter filterProduct)
        {
            List<ProductModel> lstProducts = new List<ProductModel>();

            try
            {
                lstProducts = _productRepository.Search(a => a.IsActive.HasValue).Result.ToList();

                if (filterProduct.Price.HasValue)
                    lstProducts = lstProducts.Where(x => x.Price.Equals(filterProduct.Price)).ToList();

                if (!string.IsNullOrEmpty(filterProduct.Name))
                    lstProducts = lstProducts.Where(x => x.Name.ToLower().Contains(filterProduct.Name.ToLower())).ToList();

                if (!string.IsNullOrEmpty(filterProduct.Description))
                    lstProducts = lstProducts.Where(x => x.Name.ToLower().Contains(filterProduct.Description.ToLower())).ToList();

                if (filterProduct.StartDate.HasValue && filterProduct.EndDate.HasValue)
                    lstProducts = lstProducts.Where(x =>
                        x.CreationDate >= filterProduct.StartDate && x.CreationDate <= filterProduct.EndDate)
                                                .ToList();

                if (!string.IsNullOrEmpty(filterProduct.Order) && !string.IsNullOrEmpty(filterProduct.PropertySort))
                {
                    if (filterProduct.Order.ToLower() == "asc" && filterProduct.PropertySort.ToLower() == "name")
                        lstProducts = lstProducts.OrderBy(x => x.Name).ToList();

                    else if (filterProduct.Order.ToLower() == "desc" && filterProduct.PropertySort.ToLower() == "name")
                        lstProducts = lstProducts.OrderByDescending(x => x.Name).ToList();

                    else if (filterProduct.Order.ToLower() == "asc" && filterProduct.PropertySort.ToLower() == "description")
                        lstProducts = lstProducts.OrderBy(x => x.Description).ToList();

                    else if (filterProduct.Order.ToLower() == "desc" && filterProduct.PropertySort.ToLower() == "description")
                        lstProducts = lstProducts.OrderByDescending(x => x.Description).ToList();
                }
            }
            catch { throw new NotImplementedException("An error occurred while trying to search"); }

            return lstProducts;
        }

        public async Task<StringBuilder> Update(Guid id, ProductModel product)
        {
            var productUpdated = _productRepository.GetById(id).Result;

            if (productUpdated == null)
                return _logsErros.AppendLine("Product not found");

            productUpdated.Name = product.Name;
            productUpdated.Description = product.Description;
            productUpdated.Price = product.Price;

            try { await _productRepository.Update(productUpdated); }
            catch (Exception ex) { return _logsErros.AppendLine("An error occurred while trying to updated:" + ex.Message.ToString()); }

            return _logsErros;
        }
    }
}
