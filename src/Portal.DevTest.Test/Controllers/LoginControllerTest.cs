using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Business.Services;
using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Model;
using Portal.DevTest.Date.Repositorys;
using Portal.DevTest.Test.Configuration;
using PortalTele.DevTest.API.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Portal.DevTest.Test.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public async Task Signup_CreateNewUser_ReturnsOkResult()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var userRepository = new UserRepository(context);
                var userService = new UserService(userRepository);
                var loginController = new LoginController(mapper, userService, null);

                // Act
                var result = await loginController.Add(
                    new UserViewModel()
                    {
                        Id = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                        UserName = "dantas.1222",
                        DisplayName = "Jonas Dantas",
                        Email = "jonas@email.com",
                        Password = "senha@123"
                    });

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async Task Signup_CreateNewUserFieldsInvalid_ReturnsFailed()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var userRepository = new UserRepository(context);
                var userService = new UserService(userRepository);
                var loginController = new LoginController(mapper, userService, null);

                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var userViewModel = new UserViewModel() { };
                viewStates.AddViewValidate(loginController, userViewModel);

                var result = await loginController.Add(userViewModel);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public void Signin_UserAccessValid_ReturnOK()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                context.Add(new UserModel() { IsActive = true, CreationDate = DateTime.Now, DisplayName = "Jupiter Dol", UserName = "jupiter.dol", Email = "nome@email.com", Password = "senha@123" });
                context.SaveChanges();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var userRepository = new UserRepository(context);
                var userService = new UserService(userRepository);
                var loginController = new LoginController(mapper, userService, null);

                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var userViewModel = new UserViewModel() { DisplayName = "Jupiter Dol", UserName = "jupiter.dol", Email = "nome@email.com", Password = "senha@123" };
                viewStates.AddViewValidate(loginController, userViewModel);

                var result = loginController.Signin(userViewModel);

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }

        }

        [Fact]
        public void Signin_UserAccessInvalid_ReturnBadRequest()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                context.Add(new UserModel() { IsActive = true, CreationDate = DateTime.Now, DisplayName = "Jupiter Dol", UserName = "jupiter.dol", Email = "nome@email.com", Password = "senha@123" });
                context.SaveChanges();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var userRepository = new UserRepository(context);
                var userService = new UserService(userRepository);
                var loginController = new LoginController(mapper, userService, null);

                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var userViewModel = new UserViewModel() { DisplayName = "Jupiter Dol", UserName = "jupiter.d", Email = "nome@email.com", Password = "senha@153" };
                viewStates.AddViewValidate(loginController, userViewModel);

                var result = loginController.Signin(userViewModel);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }

        }

        [Fact]
        public void Signin_UserAccessFieldsInvalid_ReturnBadRequest()
        {
            var builder = new DbContextOptionsBuilder<ContextSQLServer>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new ContextSQLServer(options))
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserModel>());
                var mapper = config.CreateMapper();

                //Arrange
                var userRepository = new UserRepository(context);
                var userService = new UserService(userRepository);
                var loginController = new LoginController(mapper, userService, null);

                ActiveViewStateValidation viewStates = new ActiveViewStateValidation();
                var userViewModel = new UserViewModel() { };
                viewStates.AddViewValidate(loginController, userViewModel);

                var result = loginController.Signin(userViewModel);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }

        }
    }
}
