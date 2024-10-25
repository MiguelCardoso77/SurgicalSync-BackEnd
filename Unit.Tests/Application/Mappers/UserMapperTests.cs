using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Mappers
{
    [TestFixture]
    public class UserMapperTests
    {
        private UserMapper _mapper;
        private Mock<UserId> _mockUserId;
        private Mock<Username> _mockUsername;
        private Mock<UserEmail> _mockUserEmail;
        private UserRole _mockUserRole;
        
        [SetUp]
        public void Setup()
        {
            _mapper = new UserMapper();
            _mockUserId = new Mock<UserId>("1");
            _mockUsername = new Mock<Username>("Test");
            _mockUserEmail = new Mock<UserEmail>("emailTest@gmail.com");
            _mockUserRole = UserRole.Admin;
        }
        
        [Test]
        public void TestToDomain()
        {
            var dto = new UserDto()
            {
                Id = "1", UserName = "Test", UserEmail = "emailTest@gmail.com", UserRole = "Admin"
            };
            
            var user = _mapper.ToDomain(dto, new UserId(dto.Id));

            Assert.AreEqual(user.Id.AsString(), dto.Id);
            Assert.AreEqual(user.Username.ToString(), dto.UserName);
            Assert.AreEqual(user.UserEmail.ToString(), dto.UserEmail);
            Assert.AreEqual(user.UserRole.ToString(), dto.UserRole);
        }

        [Test]
        public void TestToDto()
        {
            var user = new User(
                _mockUserId.Object, 
                _mockUsername.Object,
                _mockUserEmail.Object,
                _mockUserRole
            );

            var dto = _mapper.ToDto(user);

            Assert.AreEqual(dto.Id, user.Id.AsString());
            Assert.AreEqual(dto.UserName, user.Username.ToString());
            Assert.AreEqual(dto.UserEmail, user.UserEmail.ToString());
            Assert.AreEqual(dto.UserRole, user.UserRole.ToString());
        }
        
        [Test]
        public void TestToDtoList()
        {
            var user = new Mock<User>(
                _mockUserId.Object, 
                _mockUsername.Object,
                _mockUserEmail.Object,
                _mockUserRole
            );

            var userList = new List<Mock<User>>() { user };
            var dtoList = _mapper.ToDtoList(userList.Select(u => u.Object).ToList());
            
            Assert.AreEqual(dtoList.First().Id, user.Object.Id.AsString());
            Assert.AreEqual(dtoList.First().UserName, user.Object.Username.ToString());
            Assert.AreEqual(dtoList.First().UserEmail, user.Object.UserEmail.ToString());
            Assert.AreEqual(dtoList.First().UserRole, user.Object.UserRole.ToString());
        }
    }
}