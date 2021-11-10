using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Business.Services;
using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Model;
using Portal.DevTest.Date.Repositorys;
using PortalTele.DevTest.API.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Portal.DevTest.Test.Controllers
{
    public class OrderControllerTest
    {
        [Fact]
        public void Order_CreateNewOrder_ReturnsOkResult()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var idUser = Guid.NewGuid();
                var idProduct = Guid.NewGuid();

                context.Add(new UserModel()
                {
                    Id = idUser,
                    CreationDate = DateTime.Now,
                    DisplayName = "Jonas Lopes",
                    UserName = "jonas.lopes",
                    Email = "jonas.lopes@email.com",
                    IsActive = true,
                    Password = "senhajs"
                }
                );

                context.Add(new ProductModel()
                {
                    Id = idProduct,
                    Name = "Iphone 13",
                    Description = "Iphone 13 Pro - 1TB",
                    IsActive = true,
                    Price = 14499.33M,
                    CreationDate = DateTime.Now
                }
                );

                context.SaveChanges();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var orderRepository = new OrderRepository(context);
                var userRepository = new UserRepository(context);
                var productRepository = new ProductRepository(context);

                var orderService = new OrderService(orderRepository, productRepository, userRepository);
                var orderController = new OrderController(mapper, orderService, orderRepository);

                var actionResult = orderController.Add(
                   new OrderModel()
                   {
                       UserId = idUser,
                       IsActive = true,
                       CreationDate = DateTime.Now,
                       lstOrderItems = new List<OrderItemModel>()
                        {
                             new OrderItemModel()
                            {
                                Amount = 1,
                                CurrentPrice = 28432.89M,
                                ProductId =  idProduct
                            },
                            new OrderItemModel()
                            {
                                Amount = 2,
                                CurrentPrice = 12432.89M,
                                ProductId =  idProduct
                            }
                        }
                   }).Result as OkObjectResult;

                // Assert
                Assert.IsType<OkObjectResult>(actionResult);
            }
        }

        [Fact]
        public void Order_CreateNewOrder_ReturnsFailed()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var orderRepository = new OrderRepository(context);
                var userRepository = new UserRepository(context);
                var productRepository = new ProductRepository(context);

                var orderService = new OrderService(orderRepository, productRepository, userRepository);
                var orderController = new OrderController(mapper, orderService, orderRepository);

                // Act
                var actionResult = orderController.Add(
                        new OrderModel()
                        {
                            Id = Guid.NewGuid(),
                            CreationDate = DateTime.Now,
                            UserId = Guid.Parse("ddccfa2e-f03a-4bd6-a38a-d668829aa53d"),
                            lstOrderItems = new List<OrderItemModel>()
                        }).Result as BadRequestObjectResult;

                // Assert
                Assert.IsType<BadRequestObjectResult>(actionResult);
            }
        }

        [Fact]
        public void Order_GetFilterOrder_ReturnsAll()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var idUser = Guid.NewGuid();
                var idProduct = Guid.NewGuid();

                context.Add(new OrderModel()
                {
                    UserId = idUser,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    lstOrderItems = new List<OrderItemModel>()
                        {
                             new OrderItemModel()
                            {
                                Amount = 1,
                                CurrentPrice = 28432.89M,
                                ProductId =  idProduct
                            },
                            new OrderItemModel()
                            {
                                Amount = 2,
                                CurrentPrice = 12432.89M,
                                ProductId =  idProduct
                            }
                        }
                });

                context.SaveChanges();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var orderRepository = new OrderRepository(context);
                var userRepository = new UserRepository(context);
                var productRepository = new ProductRepository(context);

                var orderService = new OrderService(orderRepository, productRepository, userRepository);
                var orderController = new OrderController(mapper, orderService, orderRepository);

                // Act
                var result = orderController.GetAll().Result as OkObjectResult;
                var lst = result.Value as List<OrderModel>;

                // Assert
                Assert.NotEmpty(lst);
            }
        }

    }
}
