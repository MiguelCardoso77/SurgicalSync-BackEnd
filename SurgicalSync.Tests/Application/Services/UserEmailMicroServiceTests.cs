using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services
{
    public class UserEmailMicroServiceTests
    {
        private Mock<IUserRepository> _mockIUserRepository;
        private UserEmailMicroService _service;

        [SetUp]
        public void SetUp()
        {
            _mockIUserRepository = new Mock<IUserRepository>();

            _service = new UserEmailMicroService(
                _mockIUserRepository.Object
            );
        }
        
        [Test]
        public async Task AddAsync_CreatesNewPatientProfile_WhenValidDtoIsProvided()
        {
            // Arrange
            var userEmail = "1220813.isep.ipp.pt";
            
            var user = new User(
                new UserId("1"),
                new Username("Diogo"),
                new UserEmail("1220812.isep.ipp.pt"),
                UserRole.Technician
                );
            
            var users = new List<User> { user };
            _mockIUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = _service.VerifyEmail(userEmail);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true , result.Result);
        }
    }
}