using DDDNetCore.Application.Services;
using DDDNetCore.Domain;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services
{
    public class EmailServiceTests
    {
        private EmailService _emailService;

        [SetUp]
        public void Setup()
        {
            _emailService = new EmailService();
        }
            
        [Test]
        public void TestEmailNotSentDoesNotThrow()
        {
            // Arrange
            var emailObj = new Mock<Email>("Test Email", "TestEmail@isep.ipp.pt", "This is a test email.");

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _emailService.SendEmailAsync(emailObj.Object));
        }
    }
}