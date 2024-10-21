using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.DTO
{
    [TestFixture]
    public class StaffDtoTests
    {
        [Test]
        public void TestCreateIncompleteStaffDto()
        {
            var dto = new StaffDto()
            {
                Id = "1",
                StaffName = "Tomás Gonçalves",
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.StaffName, "Tomás Gonçalves");
        }
        
        [Test]
        public void TestCreateCompleteStaffDto()
        {
            var dto = new StaffDto()
            {
                Id = "N202400001",
                StaffName = "Tomás Gonçalves",
                StaffEmail = "tomasgoncalves@gmail.com",
                StaffPhoneNumber = "931465819",
                StaffAvaiabilitySlots = new List<string>() {"2024-09-25:14h00-18h00", "2024-09-25:19h00"},
                StaffSpecialization = StaffSpecialization.Anesthesiology.ToString()
            };

            Assert.AreEqual(dto.Id, "N202400001");
            Assert.AreEqual(dto.StaffName, "Tomás Gonçalves");
            Assert.AreEqual(dto.StaffEmail, "tomasgoncalves@gmail.com");
            Assert.AreEqual(dto.StaffPhoneNumber, "931465819");
            Assert.AreEqual(dto.StaffAvaiabilitySlots,
                new List<string>() { "2024-09-25:14h00-18h00", "2024-09-25:19h00" }); 
            Assert.AreEqual(dto.StaffSpecialization, StaffSpecialization.Anesthesiology.ToString());
        }
    }
}

