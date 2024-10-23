using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.DTO
{
    public class LoginResponseTests
    {
        [Test]
        public void TestCreateIncompleteLoginResponse()
        {
            var dto = new LoginResponse()
            {
                Email = "1221194@isep.ipp.pt",
                Registered = true,
            };
            
            Assert.AreEqual(dto.Email, "1221194@isep.ipp.pt");
            Assert.AreEqual(dto.Registered, true);
        }
        
        [Test]
        public void TestCreateCompleteLoginResponse()
        {
            var dto = new LoginResponse()
            {
                Kind = "test",
                LocalId = "1",
                Email = "1221194@isep.ipp.pt",
                DisplayName = "test",
                IdToken = "2",
                Registered = true,
                RefreshToken = "3",
                ExpiresIn = "hoje"
            };

            Assert.AreEqual(dto.Email, "1221194@isep.ipp.pt");
            Assert.AreEqual(dto.Kind, "test");
            Assert.AreEqual(dto.LocalId, "1");
            Assert.AreEqual(dto.DisplayName, "test");
            Assert.AreEqual(dto.IdToken, "2");
            Assert.AreEqual(dto.Registered, true);
            Assert.AreEqual(dto.RefreshToken, "3");
            Assert.AreEqual(dto.ExpiresIn, "hoje");
        }
    }
}