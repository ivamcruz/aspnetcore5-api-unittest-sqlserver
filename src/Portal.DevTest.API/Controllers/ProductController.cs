using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTele.DevTest.API.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products/{id:guid}")]
        [Authorize("write:messages")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return BadRequest("Ids are not the same");

            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var resultProduct = new ProductModel();

            try { resultProduct = await _productService.GetById(id); } catch { return BadRequest("An error occurred while trying to get"); };

            return Ok();
        }

        [HttpPost("products")]
        [Authorize("write:messages")]
        public async Task<ActionResult> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var logsAttemptSaveNewProduct = await _productService.AddNew(
                new ProductModel()
                {
                    Description = productViewModel.Description,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    IsActive = true
                }
                );

            if (!string.IsNullOrEmpty(logsAttemptSaveNewProduct.ToString()))
                return BadRequest(logsAttemptSaveNewProduct.ToString());

            return Ok();
        }

        [HttpPost("products/search")]
        [Authorize("read:messages")]
        public ActionResult<List<ProductModel>> Search(ProductsFilter productFilter)
        {
            List<ProductModel> lstProducts;

            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try { lstProducts = _productService.Search(productFilter).ToList(); } catch (Exception ex) { return BadRequest(ex.Message.ToString()); }

            return Ok(lstProducts);
        }

        [HttpPut("products/{id:guid}")]
        [Authorize("write:messages")]
        public async Task<ActionResult> Update(Guid id, ProductViewModel productViewModel)
        {
            if (string.IsNullOrEmpty(id.ToString())) return BadRequest("Ids are not the same");

            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var logsAttemptSaveNewProduct = await _productService
                .Update(id,
                new ProductModel() { Name = productViewModel.Name, Description = productViewModel.Description, Price = 45.54M });

            if (!string.IsNullOrEmpty(logsAttemptSaveNewProduct.ToString()))
                return BadRequest(logsAttemptSaveNewProduct.ToString());

            return Ok("Updated");
        }
    }
}
