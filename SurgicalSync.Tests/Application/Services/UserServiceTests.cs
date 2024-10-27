using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserRepository> _repoMock;
        private UserMapper _mapper;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IUserRepository>();
            _mapper = new UserMapper();
            
            _userService = new UserService(
                _unitOfWorkMock.Object,
                _repoMock.Object,
                _mapper);
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_ReturnsListOfUserDtos()
        {
            // Arrange
            var users = new List<User>
            {
                new User(new UserId("1"), new Username("Test1"), new UserEmail("email@email.com"), UserRole.Technician),
                new User(new UserId("2"), new Username("Test2"), new UserEmail("email2@gmail.com"), UserRole.Technician)
            };

            _repoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));

            _repoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_WhenCalled_ReturnsUserDto()
        {
            // Arrange
            var user = new User(new UserId("1"), new Username("Test1"), new UserEmail("email@email.com"),
                UserRole.Technician);

            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(user);

            // Act
            var result = await _userService.GetByIdAsync(new UserId("1"));

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("1"));
            Assert.That(result.UserName, Is.EqualTo("Test1"));
            Assert.That(result.UserEmail, Is.EqualTo("email@email.com"));
            Assert.That(result.UserRole, Is.EqualTo(UserRole.Technician.ToString()));

            _repoMock.Verify(x => x.GetByIdAsync(It.IsAny<UserId>()), Times.Once);
        }
        
        [Test]
        public async Task GetByIdAsync_NoUserFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetByIdAsync(new UserId("1"));

            // Assert
            Assert.That(result, Is.Null);

            _repoMock.Verify(x => x.GetByIdAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenCalled_UpdatesUser()
        {
            // Arrange
            var user = new User(new UserId("22QNNHEvtPdIWB8xPvYC0tMbI6k2"), new Username("Test1"), new UserEmail("email@email.com"), UserRole.Technician);
            var userDto = new UserDto
            {
                Id = "22QNNHEvtPdIWB8xPvYC0tMbI6k2",
                UserName = "Test1",
                UserEmail = "email@email.com",
                UserRole = UserRole.Technician.ToString()
            };

            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateAsync(userDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test1", result.UserName);
            Assert.AreEqual("email@email.com", result.UserEmail);
            Assert.AreEqual(UserRole.Technician.ToString(), result.UserRole);
        }
        
        [Test]
        public async Task UpdateAsync_NoUserFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.UpdateAsync(new UserDto{Id = "1"});

            // Assert
            Assert.That(result, Is.Null);

            _repoMock.Verify(x => x.GetByIdAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_WhenCalled_DeletesUser()
        {
            // Arrange
            var user = new User(new UserId("22QNNHEvtPdIWB8xPvYC0tMbI6k2"), new Username("Test1"),
                new UserEmail("email@email.com"), UserRole.Technician);

            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteAsync(new UserId("22QNNHEvtPdIWB8xPvYC0tMbI6k2"));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test1", result.UserName);
            Assert.AreEqual("email@email.com", result.UserEmail);
            Assert.AreEqual(UserRole.Technician.ToString(), result.UserRole);
        }
        
        [Test]
        public async Task DeleteAsync_NoUserFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<UserId>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.DeleteAsync(new UserId("1"));

            // Assert
            Assert.That(result, Is.Null);

            _repoMock.Verify(x => x.GetByIdAsync(It.IsAny<UserId>()), Times.Once);
        }
        [Test]
        public async Task GetUserByEmail_NullEmail_ReturnsNull()
        {
            // Act
            var result = await _userService.GetUserByEmail(null);

            // Assert
            Assert.That(result, Is.Null);
            _repoMock.Verify(x => x.GetAllAsync(), Times.Never);
        }

        [Test]
        public async Task GetUserByEmail_UserFound_ReturnsUserDto()
        {
            // Arrange
            var userEmail = new UserEmail("email@email.com");
            var user = new User(new UserId("1"), new Username("Test1"), userEmail, UserRole.Technician);

            _repoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User> { user });

            // Act
            var result = await _userService.GetUserByEmail(userEmail);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test1", result.UserName);
            Assert.AreEqual(userEmail.ToString(), result.UserEmail);
            Assert.AreEqual(UserRole.Technician.ToString(), result.UserRole);

            _repoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetUserByEmail_UserNotFound_ReturnsNull()
        {
            // Arrange
            var userEmail = new UserEmail("nonexistent@email.com");
            _repoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>());

            // Act
            var result = await _userService.GetUserByEmail(userEmail);

            // Assert
            Assert.That(result, Is.Null);
            _repoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
    
}