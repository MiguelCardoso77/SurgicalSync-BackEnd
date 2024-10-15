using System.Collections.Generic;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;


namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    [TestFixture]
    public class StaffTests
    {
        
        [Test]
        public void TestConstructor()
        {

            List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList = new List<StaffAvaiabilitySlots>();
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 1: 2024-09-25:14h00-18h00"));
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 2: 2024-09-25:19h00/2024-09-26:02h00"));

            var staff = new Staff(
                new LicenseNumber("N202400001"),
                new StaffName("Raquel Gonçalves"),
                new StaffEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                staffAvaiabilitySlotsList,
                StaffType.Other);
            
            Assert.AreEqual("100", staff.Id);
            Assert.AreEqual("Raquel Gonçalves", staff.StaffName);
            Assert.AreEqual("raquelgoncalves@gmail.com" , staff.StaffEmail);
            Assert.AreEqual("962839401" , staff.StaffPhoneNumber);
            Assert.AreEqual(StaffSpecialization.Dermatology , staff.StaffSpecialization);
            Assert.AreEqual(2, staff.StaffAvaiabilitySlots.Count);
            Assert.AreEqual(StaffType.Other, staff.StaffType);
            
        }
        
        
       

        [Test]
        public void TestChangeStaffSpecialization()
        {
            List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList = new List<StaffAvaiabilitySlots>();
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 1: 2024-09-25:14h00-18h00"));
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 2: 2024-09-25:19h00/2024-09-26:02h00"));
            var staff = new Staff(
                new LicenseNumber("N202400001"),
                new StaffName("Raquel Gonçalves"),
                new StaffEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                staffAvaiabilitySlotsList,
                StaffType.Other);

            staff.ChangeStaffSpecialization(StaffSpecialization.Dermatology);
            Assert.AreEqual(StaffSpecialization.Dermatology, staff.StaffSpecialization);
        }

        [Test]
        public void TestActivateStaff()
        {
            List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList = new List<StaffAvaiabilitySlots>();
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 1: 2024-09-25:14h00-18h00"));
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 2: 2024-09-25:19h00/2024-09-26:02h00"));
            var staff = new Staff(
                new LicenseNumber("N202400001"),
                new StaffName("Raquel Gonçalves"),
                new StaffEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                staffAvaiabilitySlotsList,
            StaffType.Other);

            staff.ActivateStaff();
            Assert.IsTrue(staff.IsActive);
        }
        
        [Test]
        public void TestDeactivateStaff()
        {
            List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList = new List<StaffAvaiabilitySlots>();
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 1: 2024-09-25:14h00-18h00"));
            staffAvaiabilitySlotsList.Add(new StaffAvaiabilitySlots("slot 2: 2024-09-25:19h00/2024-09-26:02h00"));
            var staff = new Staff(
                new LicenseNumber("N202400001"),
                new StaffName("Raquel Gonçalves"),
                new StaffEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                staffAvaiabilitySlotsList,
                StaffType.Other);

            staff.DeactivateStaff();
            Assert.IsFalse(staff.IsActive);
        }

    }
}