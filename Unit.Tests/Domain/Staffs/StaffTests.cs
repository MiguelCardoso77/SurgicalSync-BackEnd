using System.Collections.Generic;
using DDDNetCore.Domain.Staffs;
using Moq;
using NUnit.Framework;


namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    [TestFixture]
    public class StaffTests
    {
        private Mock<LicenseNumber> _mockLicenseNumber;
        private Mock<StaffName> _mockStaffName;
        private Mock<StaffEmail> _mockEmail;
        private Mock<StaffPhoneNumber> _mockPhoneNumber;
        private List<StaffAvaiabilitySlots> _mockAvaiabilitySlots;

        [SetUp]
        public void SetUp()
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
        public void TestConstructor()
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
            
            Assert.AreEqual("N202400001", staff.Id.ToString());
            Assert.AreEqual("Raquel Gonçalves", staff.StaffName.ToString());
            Assert.AreEqual("raquelgoncalves@gmail.com" , staff.StaffEmail.ToString());
            Assert.AreEqual("962839401" , staff.StaffPhoneNumber.ToString());
            Assert.AreEqual(StaffSpecialization.Dermatology.ToString() , staff.StaffSpecialization.ToString());
            Assert.AreEqual(2.ToString(), staff.StaffAvaiabilitySlots.Count.ToString());
            //Assert.AreEqual(StaffType.Other.ToString(), staff.StaffType.ToString());
            
        }
        
        
       

        [Test]
        public void TestChangeStaffSpecialization()
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
            staff.ChangeStaffSpecialization(StaffSpecialization.Cardiology);
            Assert.AreEqual(StaffSpecialization.Dermatology.ToString(), staff.StaffSpecialization.ToString());
        }

        [Test]
        public void TestActivateStaff()
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
            staff.ActivateStaff();
            Assert.IsTrue(staff.IsActive);
        }
        
        [Test]
        public void TestDeactivateStaff()
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
            staff.DeactivateStaff();
            Assert.IsFalse(staff.IsActive);
        }

    }
}