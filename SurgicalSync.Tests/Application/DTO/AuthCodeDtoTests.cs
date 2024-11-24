using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class AuthCodeDtoTests
{
    [Test]
    public void TestIncompleteDto()
    {
        var dto = new AuthCodeDto();
        Assert.IsNull(dto.AuthCode);
        Assert.IsNull(dto.Email);
    }

    [Test]
    public void TestCompleteDto()
    {
        var dto = new AuthCodeDto
        {
            AuthCode = "123456",
            Email = "1220772@isep.ipp.pt"
        };

        Assert.AreEqual("123456", dto.AuthCode);
        Assert.AreEqual("1220772@isep.ipp.pt", dto.Email);
    }

}