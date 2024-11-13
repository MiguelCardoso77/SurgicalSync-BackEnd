using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO
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
                UserEmail = "tomasgoncalves@gmail.com",
                StaffPhoneNumber = "931465819",
                StaffSpecialization = "Anesthesiology",
                StaffAvailabilitySlots = "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00",
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400001"
            };

            Assert.AreEqual(dto.Id, "N202400001");
            Assert.AreEqual(dto.StaffName, "Tomás Gonçalves");
            Assert.AreEqual(dto.UserEmail, "tomasgoncalves@gmail.com");
            Assert.AreEqual(dto.StaffPhoneNumber, "931465819");
            Assert.AreEqual(dto.StaffAvailabilitySlots,
                "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00");
            Assert.AreEqual(dto.StaffSpecialization, "Anesthesiology");
            Assert.AreEqual(dto.StaffType, StaffType.Doctor.ToString());
            Assert.AreEqual(dto.isActive, true);
            Assert.AreEqual(dto.StaffLicenseNumber, "N202400001");
        }
        
        [Test]
        public void TestCreateIncompleteStaffDto2()
        {
            var dto = new StaffDtoList()
            {
                Id = "1",
                StaffName = "Tomás Gonçalves",
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.StaffName, "Tomás Gonçalves");
        }
        
        [Test]
        public void TestCreateCompleteStaffDto2()
        {
            var dto = new StaffDtoList()
            {
                Id = "N202400001",
                StaffName = "Tomás Gonçalves",
                UserEmail = "tomasgoncalves@gmail.com",
                StaffSpecialization = "Anesthesiology"
            };

            Assert.AreEqual(dto.Id, "N202400001");
            Assert.AreEqual(dto.StaffName, "Tomás Gonçalves");
            Assert.AreEqual(dto.UserEmail, "tomasgoncalves@gmail.com");
            Assert.AreEqual(dto.StaffSpecialization, "Anesthesiology");
        }
    }
}

