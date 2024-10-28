using System;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Users
{
    [TestFixture]
    public class UserTests
    {
        private Mock<UserId> _mockUserId;
        private Mock<Username> _mockUsername;
        private Mock<UserEmail> _mockUserEmail;
        private UserRole _mockUserRole;
        
        [SetUp]
        public void SetUp()
        {
            _mockUserId = new Mock<UserId>("1");
            _mockUsername = new Mock<Username>("SEM5PI");
            _mockUserEmail = new Mock<UserEmail>("email@gmail.com");
            _mockUserRole = UserRole.Admin;
        }
        
        [Test]
        public void TestConstructor()
        {
            var user = new User(_mockUserId.Object, _mockUsername.Object, _mockUserEmail.Object, _mockUserRole);
            
            Assert.AreEqual("SEM5PI", user.Username.Value);
            Assert.AreEqual("email@gmail.com", user.UserEmail.Value);
            Assert.AreEqual(UserRole.Admin, user.UserRole);
        }

        [Test]
        public void TestChangeUserName()
        {
            var user = new User(_mockUserId.Object, _mockUsername.Object, _mockUserEmail.Object, _mockUserRole);

            user.ChangeUserName(new Mock<Username>("newtest").Object);
            Assert.AreEqual("newtest", user.Username.Value);
        }

        [Test]
        public void TestChangeUserRole()
        {
            var user = new User(_mockUserId.Object, _mockUsername.Object, _mockUserEmail.Object, _mockUserRole);

            user.ChangeUserRole(UserRole.Technician);
            Assert.AreEqual(UserRole.Technician, user.UserRole);
        }

        [Test]
        public void TestActivateUser()
        {
            var user = new User(_mockUserId.Object, _mockUsername.Object, _mockUserEmail.Object, _mockUserRole);

            user.ActivateUser();
            Assert.IsTrue(user.IsActive);
        }
        
        [Test]
        public void TestDeactivateUser()
        {
            var user = new User(_mockUserId.Object, _mockUsername.Object, _mockUserEmail.Object, _mockUserRole);

            user.DeactivateUser();
            Assert.IsFalse(user.IsActive);
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var user = (User)Activator.CreateInstance(typeof(User), true);

            Assert.NotNull(user);
            Assert.IsNull(user.Id);
            Assert.IsNull(user.UserEmail);
            Assert.IsNull(user.Username);
        }

        [Test]
        public void TestChangeUserEmail()
        {
            var user = new User(_mockUserId.Object, _mockUsername.Object, _mockUserEmail.Object, _mockUserRole);

            user.ChangeUserEmail(new Mock<UserEmail>("email@outlook.pt").Object);

            Assert.AreEqual("email@outlook.pt", user.UserEmail.Value);
        }
    }
}