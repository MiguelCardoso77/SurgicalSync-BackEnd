using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffSpecializationTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffSpecialization1 = StaffSpecialization.Dermatology;
            Assert.AreEqual(StaffSpecialization.Dermatology, staffSpecialization1);
        }
        
        [Test]
        public void TestToString()
        {
            var staffSpecialization1 = StaffSpecialization.Dermatology;
            Assert.AreEqual(StaffSpecialization.Dermatology.ToString(), staffSpecialization1.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffSpecialization =  StaffSpecialization.Dermatology;
            var staffSpecialization1 = StaffSpecialization.Dermatology;
            Assert.AreEqual(staffSpecialization, staffSpecialization1);
        }
    }
}