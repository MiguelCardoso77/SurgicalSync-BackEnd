using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffSpecializationTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffSpecialization1 = new StaffSpecialization() == StaffSpecialization.Dermatology;
            Assert.AreEqual(StaffSpecialization.Dermatology, staffSpecialization1);
        }
        
        [Test]
        public void TestToString()
        {
            var staffSpecialization1 = new StaffSpecialization() == StaffSpecialization.Dermatology;
            Assert.AreEqual(StaffSpecialization.Dermatology, staffSpecialization1.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffSpecialization2 = new StaffSpecialization() == StaffSpecialization.Dermatology;
            var staffSpecialization3 = new StaffSpecialization() == StaffSpecialization.Dermatology;
            Assert.AreEqual(staffSpecialization2, staffSpecialization3);
        }
    }
}