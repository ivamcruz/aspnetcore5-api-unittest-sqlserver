using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Business.Services;
using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Model;
using Portal.DevTest.Date.Repositorys;
using Portal.DevTest.Test.Configuration;
using PortalTele.DevTest.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal.DevTest.Test.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task Product_GetProduct_ReturnsOkResult()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var idGuidProduct = Guid.NewGuid();
                var newProduct = new ProductModel()
                {
                    Id = idGuidProduct,
                    Name = "Iphone 13",
                    Description = "Iphone 13 Pro - 1TB",
                    IsActive = true,
                    Price = 14499.33M,
                    CreationDate = DateTime.Now
                };

                context.Add(newProduct);
                context.SaveChanges();

                //Arrange
                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);

                // Act
                var result = await productController.Get(idGuidProduct);

                // Assert
                Assert.IsType<OkResult>(result);
            }
        }

        [Fact]
        public async Task Product_CreateNewProduct_ReturnOk()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                //Arrange
                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);

                var result = await productController.Add(
                        new ProductViewModel()
                        {
                            Name = "Mouse XRT",
                            Description = "Mouse XRT 3D",
                            Price = 2300.34M
                        });

                // Assert
                Assert.IsType<OkResult>(result);
            }
        }

        [Fact]
        public async Task Product_CreateNewProductFieldsInvalids_ReturnsFailed()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                //Arrange
                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);

                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var productViewModel = new ProductViewModel() { };
                viewStates.AddViewValidate(productController, productViewModel);

                // Act
                var result = await productController.Add(productViewModel);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }

        }

        [Fact]
        public void Product_GetFilterProduct_ReturnsOkResult()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var listProducts = new List<ProductModel>()
                {
                    new ProductModel()
                    {
                            Id = Guid.NewGuid(),
                            Name = "Iphone 13",
                            Description = "Iphone 13 Pro - 1TB",
                            IsActive = true,
                            Price = 14499.33M,
                            CreationDate = DateTime.Now
                    },
                    new ProductModel()
                    {
                            Id = Guid.NewGuid(),
                            Name = "Iphone 11",
                            Description = "Iphone 11 Pro - 512GB",
                            IsActive = true,
                            Price = 6499.33M,
                            CreationDate = DateTime.Now
                    }
                };

                context.AddRange(listProducts);
                context.SaveChanges();

                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);

                // Arrange
                var productFilter = new ProductsFilter() { Name = "Iphone 13" };

                // Act
                var results = productController.Search(productFilter).Result as OkObjectResult;
                var lstProducts = results.Value as List<ProductModel>;

                string nameResult = lstProducts.Where(x => x.Name.ToLower().Contains(productFilter.Name.ToString().ToLower())).FirstOrDefault().Name;

                // Assert
                Assert.Contains("Iphone 13", nameResult);
            }
        }

        [Fact]
        public async Task Product_UpdateProduct_ReturnsOkResult()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var idGuidProduct = Guid.NewGuid();
                var newProduct = new ProductModel()
                {
                    Id = idGuidProduct,
                    Name = "Phenom 10",
                    Description = "Phenom 10 - 1TB",
                    IsActive = true,
                    Price = 12499.33M,
                    CreationDate = DateTime.Now
                };

                context.Add(newProduct);
                context.SaveChanges();

                //Arrange
                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);


                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var productViewModel = new ProductViewModel() { Price = 34.56M, Name = "Phenom 10 TT" };
                viewStates.AddViewValidate(productController, productViewModel);

                //Act
                var returnUpdated = await productController.Update(idGuidProduct, productViewModel);

                // Assert
                Assert.IsType<OkObjectResult>(returnUpdated);
            }
        }

        [Fact]
        public async Task Product_UpdateProductFieldsInvalid_ReturnsBadRequest()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var idGuidProduct = Guid.NewGuid();
                var newProduct = new ProductModel()
                {
                    Id = idGuidProduct,
                    Name = "Phenom 10",
                    Description = "Phenom 10 - 1TB",
                    IsActive = true,
                    Price = 12499.33M,
                    CreationDate = DateTime.Now
                };

                context.Add(newProduct);
                context.SaveChanges();

                //Arrange
                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);

                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var productViewModel = new ProductViewModel() { };
                viewStates.AddViewValidate(productController, productViewModel);

                //Act
                var returnUpdated = await productController.Update(idGuidProduct, productViewModel);

                // Assert
                Assert.IsType<BadRequestObjectResult>(returnUpdated);
            }
        }

        [Fact]
        public async Task Product_UpdateProductNotFound_ReturnsBadRequest()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                //Arrange
                var productRepository = new ProductRepository(context);
                var productService = new ProductService(productRepository);
                var productController = new ProductController(productService);
                
                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var productViewModel = new ProductViewModel() { };
                viewStates.AddViewValidate(productController, productViewModel);

                //Act
                var returnUpdated = await productController.Update(
                    Guid.Parse("fd454f2b-a43e-4154-a324-8ff1e4a9eadb"),
                    new ProductViewModel()
                    {
                        Id = Guid.Parse("ec933eb9-e42b-4408-81c9-1ffc87a8f091"),
                        Name = "Iphone 13 PRO",
                        Price = 34.45M
                    });

                // Assert
                Assert.IsType<BadRequestObjectResult>(returnUpdated);
            }

        }


    }
}
