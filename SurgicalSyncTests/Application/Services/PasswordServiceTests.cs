using System.Linq;
using DDDNetCore.Application.Services;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Application.Services
{
    [TestFixture]
    public class PasswordServiceTests
    {
        [Test]
        public void TestGeneratePassword()
        {
            // Arrange
            const int passwordLength = 10;
            
            // Act
            var password = PasswordService.GeneratePassword();
            
            // Assert
            Assert.AreEqual(passwordLength, password.Length);
            Assert.IsTrue(password.Any(char.IsDigit));
            Assert.IsTrue(password.Any(char.IsUpper));
            Assert.IsTrue(password.Any(char.IsSymbol));
        }
    }
}