using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Application.DTO
{
    [TestFixture]
    public class UserDtoTests
    {
        [Test]
        public void TestCreateIncompleteOperationTypeDto()
        {
            var dto = new UserDto()
            {
                Id = "1",
                UserName = "Test",
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.UserName, "Test");
        }

        [Test]
        public void TestCreateCompleteOperationTypeDto()
        {
            var dto = new UserDto()
            {
                Id = "1",
                UserName = "Test",
                UserEmail = "email@gmail.com",
                UserRole = "Admin"
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.UserName, "Test");
            Assert.AreEqual(dto.UserEmail, "email@gmail.com");
            Assert.AreEqual(dto.UserRole, "Admin");
        }

    }
}