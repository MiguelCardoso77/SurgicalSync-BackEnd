using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;


namespace DDDNetCore.Unit.Tests.Domain.Staffs
{
    [TestFixture]
    public class StaffTests
    {
        private Mock<StaffId> _mockStaffId;
        private Mock<StaffLicenseNumber> _mockLicenseNumber;
        private Mock<StaffName> _mockStaffName;
        private Mock<UserEmail> _mockEmail;
        private Mock<StaffPhoneNumber> _mockPhoneNumber;
        private List<StaffAvaiabilitySlots> _mockAvailabilitySlots;
        private StaffSpecialization _mockSpecialization;
        private StaffType _mockType;
        private bool _mockIsActive;


        [SetUp]
        public void SetUp()
        {
            _mockStaffId = new Mock<StaffId>("N202400002");
            _mockStaffName = new Mock<StaffName>("Raquel Gonçalves");
            _mockEmail = new Mock<UserEmail>("raquelgoncalves@gmail.com");
            _mockPhoneNumber = new Mock<StaffPhoneNumber>("962839401");
            _mockSpecialization = StaffSpecialization.Dermatology;
            _mockAvailabilitySlots = new List<StaffAvaiabilitySlots>
            {
                new("slot 1: 2024-09-25:14h00-18h00"),
                new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
            };
            _mockType = StaffType.Doctor;
            _mockIsActive = true;
            _mockLicenseNumber = new Mock<StaffLicenseNumber>("N202400001");
        }


        [Test]
        public void TestConstructor()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );

            Assert.AreEqual("Raquel Gonçalves", staff.StaffName.StaffNameValue);
            Assert.AreEqual("raquelgoncalves@gmail.com", staff.UserEmail.UserEmailValue);
            Assert.AreEqual("962839401", staff.StaffPhoneNumber.StaffPhoneNumberValue);
            Assert.AreEqual(StaffSpecialization.Dermatology.ToString(), staff.StaffSpecialization.ToString());
            Assert.AreEqual(2, staff.StaffAvaiabilitySlots.Count);
            Assert.AreEqual(StaffType.Doctor.ToString(), staff.StaffType.ToString());
            Assert.AreEqual(true, staff.IsActive);
            Assert.AreEqual("N202400001", staff.StaffLicenseNumber.StaffLicenseNumberValue);
        }


        [Test]
        public void TestChangeStaffSpecialization()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
            staff.ChangeStaffSpecialization(StaffSpecialization.Cardiology);
            Assert.AreEqual(StaffSpecialization.Cardiology.ToString(), staff.StaffSpecialization.ToString());
        }
        
        [Test]
        public void TestChangeStaffPhoneNumber()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
            
            Mock<StaffPhoneNumber> mockStaffPhoneNumber = new Mock<StaffPhoneNumber>("938413938");

            staff.ChangeStaffPhoneNumber(mockStaffPhoneNumber.Object);
        }
        
        [Test]
        public void TestChangeUserEmail()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
            
            Mock<UserEmail> mockUserEmail = new Mock<UserEmail>("1221194@isep.ipp.pt");

            staff.ChangeUserEmail(mockUserEmail.Object);
        }

        [Test]
        public void TestChangeStaffAvaiabilitySlots()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
            
            List<StaffAvaiabilitySlots> mockStaffAvaiabilitySlots = new List<StaffAvaiabilitySlots>();
            
            Mock<StaffAvaiabilitySlots> mockStaffAvaiabilitySlot1 = new Mock<StaffAvaiabilitySlots>("slot 1: 2024-09-25:14h00-18h00");
            Mock<StaffAvaiabilitySlots> mockStaffAvaiabilitySlot2 = new Mock<StaffAvaiabilitySlots>("slot 2: 2024-09-25:19h00/2024-09-26:02h00");
            Mock<StaffAvaiabilitySlots> mockStaffAvaiabilitySlot3 = new Mock<StaffAvaiabilitySlots>("slot 3: 2024-10-06:17h00/2024-10-06:23h00");
            
            mockStaffAvaiabilitySlots.Add(mockStaffAvaiabilitySlot1.Object);
            mockStaffAvaiabilitySlots.Add(mockStaffAvaiabilitySlot2.Object);
            mockStaffAvaiabilitySlots.Add(mockStaffAvaiabilitySlot3.Object);

            staff.ChangeStaffAvaiabilitySlots(mockStaffAvaiabilitySlots);
        }
        
        [Test]
        public void TestActivateStaff()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );
            staff.ActivateStaff();
            Assert.IsTrue(staff.IsActive);
        }

        [Test]
        public void TestDeactivateStaff()
        {
            var staff = new Staff(
                _mockStaffId.Object,
                _mockStaffName.Object,
                _mockEmail.Object,
                _mockPhoneNumber.Object,
                _mockSpecialization,
                _mockAvailabilitySlots,
                _mockType,
                _mockIsActive,
                _mockLicenseNumber.Object
            );

            staff.DeactivateStaff();
            Assert.IsFalse(staff.IsActive);
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var staff = (Staff)Activator.CreateInstance(typeof(Staff), true);

            Assert.NotNull(staff);
            Assert.IsNull(staff.Id);
            Assert.IsNull(staff.StaffName);
        }
    }
}