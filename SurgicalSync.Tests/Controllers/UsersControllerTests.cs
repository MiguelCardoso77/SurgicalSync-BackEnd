using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController _controller;
        private UserService _userServiceMock;
        private Mock<UserMapper> _userMapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserRepository> _userRepoMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepoMock = new Mock<IUserRepository>();
            _userMapperMock = new Mock<UserMapper>();
            
            // Create the mock for UserService
            _userServiceMock = new UserService(_unitOfWorkMock.Object, _userRepoMock.Object, _userMapperMock.Object);
        
            // Inject the mock service into the controller
            _controller = new UsersController(_userServiceMock);
        }
        
        [Test]
        public async Task GetAll_WhenCalled_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new ( new UserId("1"), new Username("Test1"), new UserEmail("email1@test.com"), UserRole.Admin),
                new ( new UserId("2"), new Username("Test2"), new UserEmail("email2@gmail.com"), UserRole.Technician)
            };
            
            _userRepoMock.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();
    
            // Assert
            Assert.IsNotNull(result);
            var returnUsers = result.Value as List<UserDto>;  
            Assert.AreEqual(2, returnUsers.Count);
        }
        
        [Test]
        public async Task GetById_UserExists_ReturnsUser()
        {
            // Arrange
            var user = new User(new UserId("1"), new Username("Test1"), new UserEmail("email1@test.com"), UserRole.Admin);
    
            _userRepoMock.Setup(service => service.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById("1");
            Console.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetById_UserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _userRepoMock.Setup(service => service.GetByIdAsync(It.IsAny<UserId>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task Update_InvalidUserId_ReturnsBadRequest()
        {
            // Arrange
            var userDto = new UserDto { Id = "2", UserName = "Test2", UserEmail = "test2@test.com", UserRole = "Admin" };

            // Act
            var result = await _controller.Update("1", userDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }
        
        [Test]
        public async Task Delete_UserExists_ReturnsOk()
        {
            // Arrange
            var userId = new UserId("1");
            var userDto = new UserDto { Id = "1", UserName = "TestUser", UserEmail = "testuser@test.com", UserRole = "Admin" };
            var user = new User(new UserId("1"), new Username("TestUser"), new UserEmail("testuser@test.com"), UserRole.Admin);

            // Simulate that the user exists by returning a valid UserDto when the method is called
            _userRepoMock.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync(user);
            _userRepoMock.Setup(service => service.Remove(user));

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult, but got null.");
        }

        [Test]
        public async Task Delete_UserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            // Simulate that the user does not exist by returning null
            _userRepoMock.Setup(service => service.Remove(It.IsAny<User>()));

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        
    }
}