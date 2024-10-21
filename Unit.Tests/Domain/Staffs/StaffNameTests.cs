using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffNameTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffName = new StaffName("Raquel Gonçalves");
            Assert.AreEqual("Raquel Gonçalves", staffName.StaffNameValue);
        }
        
        [Test]
        public void TestToString()
        {
            var staffName = new StaffName("Raquel Gonçalves");
            Assert.AreEqual("Raquel Gonçalves", staffName.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffName1 = new StaffName("Raquel Gonçalves");
            var staffName2 = new StaffName("Raquel Gonçalves");
            Assert.AreEqual(staffName1, staffName2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            var staffName1 = new StaffName("Raquel Gonçalves");
            var staffName2 = new StaffName("Raquel Gonçalves");
            // Act
            var hashCode1 = staffName1.GetHashCode();
            var hashCode2 = staffName2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            var staffName1 = new StaffName("Raquel Gonçalves");
            var staffName2 = new StaffName("Tomás Gonçalves");
            // Act
            var hashCode1 = staffName1.GetHashCode();
            var hashCode2 = staffName2.GetHashCode();


            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

    }
}