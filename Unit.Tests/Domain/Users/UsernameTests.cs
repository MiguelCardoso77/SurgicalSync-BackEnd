using System;
using DDDNetCore.Domain.Users;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Users
{
    [TestFixture]
    public class UsernameTests
    {
        [Test]
        public void TestConstructor()
        {
            var username = new Username("johndoe");
            Assert.AreEqual("johndoe", username.UsernameValue);
        }
        
        [Test]
        public void TestNullConstructor()
        {
            Assert.Throws<FormatException>(() => new Username(null));
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var user = (Username)Activator.CreateInstance(typeof(Username), true);

            Assert.NotNull(user);
            Assert.IsNull(user.UsernameValue);
        }
        
        [Test]
        public void TestToString()
        {
            var username = new Username("johndoe");
            Assert.AreEqual("johndoe", username.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var username1 = new Username("johndoe");
            var username2 = new Username("johndoe");
            Assert.AreEqual(username1, username2);
        }

        [Test]
        public void TestHashCode()
        {
            var username1 = new Username("johndoe");
            var username2 = new Username("johndoe");
            
            var hashCode1 = username1.GetHashCode();
            var hashCode2 = username2.GetHashCode();
            
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
    }
}