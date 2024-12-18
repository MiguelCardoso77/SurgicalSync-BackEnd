using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class ResetPasswordDtoTests
{
    [Test]
    public void TestCreateIncompleteResetPasswordDto()
    {
        // Arrange
        var dto = new ResetPasswordDto()
        {
            UserEmail = "1220772@isep.ipp.pt",
            NewPassword = "123456"
        };
        
        // Assert
        Assert.AreEqual(dto.UserEmail, "1220772@isep.ipp.pt");
        Assert.AreEqual(dto.NewPassword, "123456");
    }
    
    [Test]
    public void TestCreateCompleteForgotPasswordDto()
    {
        // Arrange
        var dto = new ForgotPasswordDto()
        {
            UserEmail = "1220772@isep.ipp.pt"
        };
        
        // Assert
        Assert.AreEqual(dto.UserEmail, "1220772@isep.ipp.pt");
    }
    
}