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
        
    }
}