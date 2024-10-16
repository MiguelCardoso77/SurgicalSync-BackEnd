using DDDNetCore.Domain.Users;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Users
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void TestConstructor()
        {
            var user = new User(
                new UserId("1"),
                new Username("test"),
                new UserEmail("email@gmail.com"),
                UserRole.Admin);
            
            Assert.AreEqual("1", user.Id.AsString());
            Assert.AreEqual("test", user.Username.UsernameValue);
            Assert.AreEqual("email@gmail.com", user.UserEmail.UserEmailValue);
            Assert.AreEqual(UserRole.Admin, user.UserRole);
        }

        [Test]
        public void TestChangeUserName()
        {
            var user = new User(
                new UserId("1"),
                new Username("test"),
                new UserEmail("email@gmail.com"),
                UserRole.Admin);

            user.ChangeUserName(new Username("newtest"));
            Assert.AreEqual("newtest", user.Username.UsernameValue);
        }

        [Test]
        public void TestChangeUserRole()
        {
            var user = new User(
                new UserId("1"),
                new Username("test"),
                new UserEmail("email@gmail.com"),
                UserRole.Admin);

            user.ChangeUserRole(UserRole.Technician);
            Assert.AreEqual(UserRole.Technician, user.UserRole);
        }

        [Test]
        public void TestActivateUser()
        {
            var user = new User(
                new UserId("1"),
                new Username("test"),
                new UserEmail("email@gmail.com"),
                UserRole.Admin);

            user.ActivateUser();
            Assert.IsTrue(user.IsActive);
        }
        
        [Test]
        public void TestDeactivateUser()
        {
            var user = new User(
                new UserId("1"),
                new Username("test"),
                new UserEmail("email@gmail.com"),
                UserRole.Admin);

            user.DeactivateUser();
            Assert.IsFalse(user.IsActive);
        }

    }
}