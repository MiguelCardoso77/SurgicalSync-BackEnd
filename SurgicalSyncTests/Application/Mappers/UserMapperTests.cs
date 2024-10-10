using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Users;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Application.Mappers
{
    [TestFixture]
    public class UserMapperTests
    {
        [Test]
        public void TestToDomain()
        {
            var dto = new UserDto()
            {
                Id = "1", UserName = "Test", UserEmail = "emailTest@gmail.com", UserRole = "Admin"
            };

            var user = UserMapper.ToDomain(dto, new UserId(dto.Id));

            Assert.AreEqual(user.Id.AsString(), dto.Id);
            Assert.AreEqual(user.Username.ToString(), dto.UserName);
            Assert.AreEqual(user.UserEmail.ToString(), dto.UserEmail);
            Assert.AreEqual(user.UserRole.ToString(), dto.UserRole);
        }

        [Test]
        public void TestToDto()
        {
            var user = new User(new UserId("1"), new Username("Test"), new UserEmail("emailTest@gmail.com"),
                new UserRole("Admin"));

            var dto = UserMapper.ToDto(user);

            Assert.AreEqual(dto.Id, user.Id.AsString());
            Assert.AreEqual(dto.UserName, user.Username.ToString());
            Assert.AreEqual(dto.UserEmail, user.UserEmail.ToString());
            Assert.AreEqual(dto.UserRole, user.UserRole.ToString());
        }
    }
}