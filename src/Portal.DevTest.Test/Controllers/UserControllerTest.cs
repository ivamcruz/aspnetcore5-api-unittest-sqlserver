using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Model;
using Portal.DevTest.Date.Repositorys;
using PortalTele.DevTest.API.Controllers;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Portal.DevTest.Business.Services;

namespace Portal.DevTest.Test.Controllers
{
    public class UserControllerTest
    {

        [Fact]
        public void Users_GetAll_ReturnsAllItems()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var _mapper = config.CreateMapper();

                var lstUser = new List<UserModel>
                {
                    new UserModel
                    {
                            CreationDate = DateTime.Now,
                            UserName = "jose.alves",
                            DisplayName = "José Alves",
                            Email = "josealves@email.com",
                            Id = Guid.NewGuid(),
                            IsActive = true,
                            Password = "senha"
                    },
                    new UserModel
                    {
                            CreationDate = DateTime.Now,
                            UserName = "dani.alves",
                            DisplayName = "Dani Alves",
                            Email = "danialves@email.com",
                            Id = Guid.NewGuid(),
                            IsActive = true,
                            Password = "senha"
                    },
                };

                context.AddRange(lstUser);
                context.SaveChanges();

                //Arrange
                var userRepository = new UserRepository(context);
                var userService = new UserService(userRepository);
                var userController = new UserController(_mapper, userRepository);

                // Act
                var okResult = userController.GetAll().Result as OkObjectResult;

                // Assert
                Assert.IsType<OkObjectResult>(okResult);
                var lstUsers = okResult.Value as List<UserModel>;
                Assert.NotEmpty(lstUsers);
            }
        }

    }
}
