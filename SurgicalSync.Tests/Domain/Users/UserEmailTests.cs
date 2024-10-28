using System;
using DDDNetCore.Domain.Users;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Users
{
    [TestFixture]
    public class UserEmailTests
    {
        [Test]
        public void TestConstructor()
        {
            var email = new UserEmail("email@email.com");
            Assert.AreEqual("email@email.com", email.Value);
        }
        
        [Test]
        public void TestNullConstructor()
        {
            Assert.Throws<FormatException>(() => new UserEmail(null));
        }

        [Test]
        public void TestToString()
        {
            var email = new UserEmail("email@email.com");
            Assert.AreEqual("email@email.com", email.ToString());
        }

        [Test]
        public void TestEquals()
        {
            var email1 = new UserEmail("email@email.com");
            var email2 = new UserEmail("email@email.com");
            Assert.AreEqual(email1, email2);
        }
        
        [Test]
        public void TestHashCode()
        {
            // Arrange
            var email1 = new UserEmail("email@email.com");
            var email2 = new UserEmail("email@email.com");
            
            // Act
            var hashCode1 = email1.GetHashCode();
            var hashCode2 = email2.GetHashCode();
            
            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var user = (UserEmail)Activator.CreateInstance(typeof(UserEmail), true);

            Assert.NotNull(user);
            Assert.IsNull(user.Value);
        }

    }
}