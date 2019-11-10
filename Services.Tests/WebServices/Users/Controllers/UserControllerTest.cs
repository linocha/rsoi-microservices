using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

using Users.Controllers;
using Users.Domain.Models;
using Users.Domain.Services;
using Users.Domain.Services.Communication;
using Users.Mapping;
using Users.Resources;

using Services.Tests.Utils.AssertHelpers;
using Services.Tests.Utils.Generators;


namespace Services.Tests.WebServices.Users.Controllers
{
    public class UsersControllerTest
    {
//        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersControllerTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new UserDtoMappingProfile()));
            _mapper = config.CreateMapper();
        }

        
        [Fact]
        public async Task GetAllAsyncTest()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var users = UserGenerator.GetTestUsers();
            service.Setup(e => e.ListAsync()).ReturnsAsync(users);
            
            var controller = new UsersController(service.Object, _mapper);

            //Act
            var result = await controller.GetAllAsync();
            
            //Assert
            var actionResult = Assert.IsType<List<UserResource>>(result);
            var userResources = Assert.IsAssignableFrom<List<UserResource>>(actionResult);
            AssertHelperBase.AssertEqualLists(users, actionResult, UserAssertHelper.AssertEquals);
            Assert.Equal(3, userResources.Count());
        }

        
        [Fact]
        public async Task GetByIdOkTest()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var userResponse = UserGenerator.GetTestUserResponse();
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userResponse);
            
            var controller = new UsersController(service.Object, _mapper);
            
            //Act
            var result = await controller.GetById(userResponse.User.Id);
            
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var userResource = Assert.IsAssignableFrom<UserResource>(actionResult.Value);
            UserAssertHelper.AssertEquals(userResponse.User, userResource);
        }

        
        [Fact]
        public async Task GetByIdNotFoundTest()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var userResponse = new UserResponse("User not found");
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userResponse);
            
            var controller = new UsersController(service.Object, _mapper);
            
            //Act
            var result = await controller.GetById(1);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
        [Fact]
        public async Task PostAsyncOkTest()
        {
            //Arrange
            var saveUserResource = UserGenerator.GetTestSaveUserResource();

            var user = UserGenerator.GetTestUser();
            user.Email = saveUserResource.Email;
            user.FirstName = saveUserResource.FirstName;
            user.LastName = saveUserResource.LastName;

            var userResponse = new UserResponse(user);
            
            var service = new Mock<IUserService>();
            service.Setup(e => e.SaveAsync(It.IsAny<User>())).ReturnsAsync(userResponse);
            
            var controller = new UsersController(service.Object, _mapper);

            //Act
            var result = await controller.PostAsync(saveUserResource);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var userResource = Assert.IsAssignableFrom<UserResource>(actionResult.Value);
            
            UserAssertHelper.AssertEquals(saveUserResource, userResource);
        }


        [Fact]
        public async Task PostAsyncBadRequestTest()
        {
            // Arrange
            var controller = new UsersController(Mock.Of<IUserService>(), _mapper);
            controller.ModelState.AddModelError("error", "some error");
            
            // Act
            var result = await controller.PostAsync(UserGenerator.GetTestSaveUserResource());
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
        [Fact]
        public async Task PutAsyncOkTest()
        {
            //Arrange
            var updateUserResource = UserGenerator.GetTestUpdateUserResource();
            
            var user = UserGenerator.GetTestUser();
            user.Email = updateUserResource.Email;
            user.FirstName = updateUserResource.FirstName;
            user.LastName = updateUserResource.LastName;
            
            var userResponse = new UserResponse(user);
            
            var service = new Mock<IUserService>();
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userResponse);
            service.Setup(e => e.UpdateAsync(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(userResponse);
            
            var controller = new UsersController(service.Object, _mapper);

            //Act
            var result = await controller.PutAsync(1, updateUserResource);
            
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var userResource = Assert.IsAssignableFrom<UserResource>(actionResult.Value);
            
            UserAssertHelper.AssertEquals(updateUserResource, userResource);
        }

        
        [Fact]
        public async Task PutAsyncBadRequestTest()
        {
            // Arrange
            var controller = new UsersController(Mock.Of<IUserService>(), _mapper);
            controller.ModelState.AddModelError("error", "some error");
            
            // Act
            var result = await controller.PutAsync(1, UserGenerator.GetTestUpdateUserResource());
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
        [Fact]
        public async Task DeleteAsyncOkTest()
        {
            var user = UserGenerator.GetTestUser();
            var userResponse = new UserResponse(user);
            
            var service = new Mock<IUserService>();
            service.Setup(e => e.DeleteAsync(It.IsAny<int>())).ReturnsAsync(userResponse);
            
            var controller = new UsersController(service.Object, _mapper);
            
            // Act
            var result = await controller.DeleteAsync(1);
            
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var userResource = Assert.IsAssignableFrom<UserResource>(actionResult.Value);
            
            UserAssertHelper.AssertEquals(userResponse, userResource);
        }

        
        [Fact]
        public async Task DeleteAsyncNotFoundTest()
        {
            // Arrange
            var userResponse = new UserResponse("error");
            
            var service = new Mock<IUserService>();
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userResponse);
            service.Setup(e => e.DeleteAsync(It.IsAny<int>())).ReturnsAsync(userResponse);
            
            var controller = new UsersController(service.Object, _mapper);
            
            // Act
            var result = await controller.DeleteAsync(1);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}