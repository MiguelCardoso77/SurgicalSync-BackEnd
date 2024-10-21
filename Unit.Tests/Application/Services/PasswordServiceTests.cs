using System;
using System.Linq;
using DDDNetCore.Application.Services;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Services
{
    [TestFixture]
    public class PasswordServiceTests
    {
        [Test]
        public void TestGeneratePassword()
        {
            // Arrange
            const int passwordLength = 10;
            const string specialCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            
            // Act
            var password = PasswordService.GeneratePassword();
            Console.WriteLine(password);
            
            // Assert
            Assert.AreEqual(passwordLength, password.Length);
            Assert.IsTrue(password.Any(char.IsDigit), "Password should contain at least one digit.");
            Assert.IsTrue(password.Any(char.IsUpper), "Password should contain at least one uppercase letter.");
            Assert.IsTrue(password.Any(char.IsSymbol) || password.Any(c => specialCharacters.Contains(c)), "Password should contain at least one special character.");
        }
    }
}