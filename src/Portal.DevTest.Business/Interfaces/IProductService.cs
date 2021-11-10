using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DevTest.Business.Interfaces
{
    public interface IProductService
    {
        Task<StringBuilder> AddNew(ProductModel product);
        Task<StringBuilder> Update(Guid id, ProductModel product);
        List<ProductModel> Search(ProductsFilter productFilter);
        Task<ProductModel> GetById(Guid id);
    }
}
