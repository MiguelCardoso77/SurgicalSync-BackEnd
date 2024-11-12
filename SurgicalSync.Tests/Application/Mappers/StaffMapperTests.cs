using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Mappers
{
    [TestFixture]
    public class StaffMapperTests
    {
        
        private StaffMapper _mapper;
        private Mock<StaffId> _mockStaffId;
        private Mock<StaffLicenseNumber> _mockLicenseNumber;
        private Mock<StaffName> _mockStaffName;
        private Mock<UserEmail> _mockEmail;
        private Mock<StaffPhoneNumber> _mockPhoneNumber;
        private Mock<StaffAvailabilitySlots> _mockAvailabilitySlots;
        private Mock<StaffSpecialization> _mockSpecialization;
        private StaffType _mockType;
        private bool _mockIsActive;


        [SetUp]
        public void SetUp()
        {
            _mapper = new StaffMapper();
            _mockStaffId = new Mock<StaffId>("N202400002");
            _mockStaffName = new Mock<StaffName>("Raquel Gonçalves");
            _mockEmail = new Mock<UserEmail>("raquelgoncalves@gmail.com");
            _mockPhoneNumber = new Mock<StaffPhoneNumber>("962839401");
            _mockSpecialization = new Mock<StaffSpecialization>("Dermatology");
            _mockAvailabilitySlots = new Mock<StaffAvailabilitySlots>("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00");
            _mockType = StaffType.Doctor;
            _mockIsActive = true;
            _mockLicenseNumber = new Mock<StaffLicenseNumber>("N202400001");
        }
        
        [Test]
        public void TestToDomain()
        {
            var dto = new StaffDto()
            {
                Id = "N202400002",
                StaffName = "Raquel Gonçalves",
                UserEmail = "raquelgoncalves@gmail.com",
                StaffPhoneNumber = "962839401", 
                StaffSpecialization = "Dermatology",
                StaffAvaiabilitySlots = "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00",
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400001"
            };
            
            var id = new StaffId(dto.Id);
            var staffAvaiabilitySlots =new StaffAvailabilitySlots(dto.StaffAvaiabilitySlots);
           
            var staff = _mapper.ToDomain(dto, id, staffAvaiabilitySlots);
            
            Assert.AreEqual(staff.Id.AsString(), dto.Id);
            Assert.AreEqual(staff.StaffName.ToString(), dto.StaffName);
            Assert.AreEqual(staff.UserEmail.ToString(), dto.UserEmail);
            Assert.AreEqual(staff.StaffPhoneNumber.ToString(), dto.StaffPhoneNumber);
            Assert.AreEqual(staff.StaffSpecialization.ToString(), dto.StaffSpecialization);
            Assert.AreEqual(dto.StaffAvaiabilitySlots, dto.StaffAvaiabilitySlots);
            Assert.AreEqual(staff.StaffType.ToString(), dto.StaffType);
            Assert.AreEqual(staff.IsActive,dto.isActive);
            Assert.AreEqual(staff.StaffLicenseNumber.ToString(), dto.StaffLicenseNumber);
        }

        
        [Test]
        public void TestToDto()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization.Object,
                _mockAvailabilitySlots.Object, 
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
    
            Assert.IsNotNull(staff.StaffAvailabilitySlots, "Staff availability slots should not be null.");
    
            var dto = _mapper.ToDto(staff);
    
            Assert.AreEqual(_mockStaffId.Object.AsString(), dto.Id);
            Assert.AreEqual(_mockStaffName.Object.ToString(), dto.StaffName);
            Assert.AreEqual(_mockEmail.Object.ToString(), dto.UserEmail);
            Assert.AreEqual(_mockPhoneNumber.Object.ToString(), dto.StaffPhoneNumber);
            Assert.AreEqual(_mockSpecialization.Object.ToString(), dto.StaffSpecialization);
            Assert.AreEqual(_mockAvailabilitySlots.Object.ToString(), dto.StaffAvaiabilitySlots);
            Assert.AreEqual(_mockType.ToString(), dto.StaffType);
            Assert.AreEqual(_mockIsActive, dto.isActive);
            Assert.AreEqual(_mockLicenseNumber.Object.ToString(), dto.StaffLicenseNumber);
        }

        
        [Test]
        public void TestToDtoList()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization.Object,
                _mockAvailabilitySlots.Object,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
           
            var dto = _mapper.ToDtoList(staff);
            
            Assert.AreEqual(staff.Id.AsString(), dto.Id);
            Assert.AreEqual(staff.StaffName.ToString(), dto.StaffName);
            Assert.AreEqual(staff.UserEmail.ToString(), dto.UserEmail);
            Assert.AreEqual(staff.StaffSpecialization.ToString(), dto.StaffSpecialization);
        }

        [Test]
        public void TestToListDto()
        {
            var staff1 = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization.Object,
                _mockAvailabilitySlots.Object,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
            
            var staff2 = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization.Object,
                _mockAvailabilitySlots.Object,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );

            var staffList = new List<Staff>();
            staffList.Add(staff1);
            staffList.Add(staff2);

            var dto = _mapper.ToListDto(staffList);
            
            Assert.AreEqual(2, dto.Count);
        }
    }
}