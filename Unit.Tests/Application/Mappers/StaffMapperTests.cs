using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Mappers
{
    [TestFixture]
    public class StaffMapperTests
    {
        private StaffMapper _mapper;
        private Mock<LicenseNumber> _mockLicenseNumber;
        private Mock<StaffName> _mockStaffName;
        private Mock<StaffEmail> _mockEmail;
        private Mock<StaffPhoneNumber> _mockPhoneNumber;
        private List<StaffAvaiabilitySlots> _mockAvaiabilitySlots;
        
        [SetUp]
        public void Setup()
        {
            _mockLicenseNumber = new Mock<LicenseNumber>("N202400001");
            _mockStaffName = new Mock<StaffName>("Raquel Gonçalves");
            _mockEmail = new Mock<StaffEmail>("raquelgoncalves@gmail.com");
            _mockPhoneNumber = new Mock<StaffPhoneNumber>("962839401");
            var _mockSpecialization = StaffSpecialization.Dermatology ;
            _mockAvaiabilitySlots = new List<StaffAvaiabilitySlots>();
            _mockAvaiabilitySlots.Add(new StaffAvaiabilitySlots("slot 1: 2024-09-25:14h00-18h00"));
            _mockAvaiabilitySlots.Add(new StaffAvaiabilitySlots("slot 2: 2024-09-25:19h00/2024-09-26:02h00"));

        }
        
        [Test]
        public void TestToDomain()
        {
            var dto = new StaffDto()
            {
                Id = "N202400001",
                StaffName = "Tomás Gonçalves",
                StaffSpecialization = StaffSpecialization.Gastroenterology.ToString(),
                StaffEmail = "tomasgoncalves@gmail.com",
                StaffPhoneNumber = "982740283", 
                StaffAvaiabilitySlots = new List<string>() { "2024-09-25:14h00-18h00", "2024-09-25:19h00/2024-09-26:02h00"},

            };
            
            var licenseNumber = new LicenseNumber(dto.Id);
            var staffAvaiabilitySlots = dto.StaffAvaiabilitySlots.Select(rs => new StaffAvaiabilitySlots(rs)).ToList();
           
            var staff = _mapper.ToDomain(dto, licenseNumber, staffAvaiabilitySlots );
            
            Assert.AreEqual(dto.Id, licenseNumber.AsString());
            Assert.AreEqual(dto.StaffAvaiabilitySlots, staffAvaiabilitySlots.ToString());
        }

        [Test]
        public void TestToDto()
        {
            var staff = new Staff(
                _mockLicenseNumber.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                StaffSpecialization.Dermatology,
                _mockAvaiabilitySlots
                //, StaffType.Other
            );
            var dto = _mapper.ToDto(staff);
            
            Assert.AreEqual(_mockLicenseNumber.Object.AsString(), dto.Id);
            Assert.AreEqual(_mockStaffName.Object, dto.StaffName);
            Assert.AreEqual(_mockEmail.Object, dto.StaffEmail);
            Assert.AreEqual(_mockPhoneNumber.Object, dto.StaffPhoneNumber);
            Assert.AreEqual(StaffSpecialization.Dermatology, dto.StaffSpecialization);
            Assert.AreEqual(_mockAvaiabilitySlots , dto.StaffAvaiabilitySlots.First());

        }
    }
}