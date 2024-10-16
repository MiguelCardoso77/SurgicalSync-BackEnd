using DDDNetCore.Domain;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain
{
    [TestFixture]
    public class EmailTests
    {
        [Test]
        public void TestConstructor()
        {
            var email = new Email("content", "destination", "subject");
            Assert.AreEqual("content", email.EmailContent);
            Assert.AreEqual("destination", email.Destination);
            Assert.AreEqual("subject", email.Subject);
        }
        
        [Test]
        public void TestConstructorWithNullEmailContent()
        {
            Assert.That(() => new Email(null, "destination", "subject"), Throws.ArgumentNullException);
        }
        
        [Test]
        public void TestConstructorWithNullDestination()
        {
            Assert.That(() => new Email("content", null, "subject"), Throws.ArgumentNullException);
        }
        
        [Test]
        public void TestConstructorWithNullSubject()
        {
            Assert.That(() => new Email("content", "destination", null), Throws.ArgumentNullException);
        }
        
    }
}