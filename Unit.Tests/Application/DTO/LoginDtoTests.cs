using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.DTO
{
    public class LoginDtoTests
    {
        [Test]
        public void TestCreateIncompleteLoginDto()
        {
            var dto = new LoginDto()
            {
                Email = "1221194@isep.ipp.pt",
                Password = "Isep-2022"
            };
            
            Assert.AreEqual(dto.Email, "1221194@isep.ipp.pt");
            Assert.AreEqual(dto.Password, "Isep-2022");
        }
        
        [Test]
        public void TestCreateCompleteLoginDto()
        {
            var dto = new LoginDto()
            {
                Email = "1221194@isep.ipp.pt",
                Password = "Isep-2022",
                ReturnSecureToken = "true"
            };

            Assert.AreEqual(dto.Email, "1221194@isep.ipp.pt");
            Assert.AreEqual(dto.Password, "Isep-2022");
            Assert.AreEqual(dto.ReturnSecureToken, "true");
        }
    }
}