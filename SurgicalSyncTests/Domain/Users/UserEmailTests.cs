using DDDNetCore.Domain.Users;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Users
{
    [TestFixture]
    public class UserEmailTests
    {
        [Test]
        public void TestConstructor()
        {
            var email = new UserEmail("email@email.com");
            Assert.AreEqual("email@email.com", email.UserEmailValue);
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

    }
}