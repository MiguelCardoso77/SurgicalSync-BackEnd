using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffIdTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffId = new StaffId("N202400001");
            Assert.AreEqual("N202400001", staffId.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var staffId = new StaffId("N202400001");
            Assert.AreEqual("N202400001", staffId.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffId1 = new StaffId("N202400001");
            var staffId2 = new StaffId("N202400001");
            Assert.AreEqual(staffId1, staffId2);
        }
        
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var staffId1 = new StaffId("N202400001");
            var staffId2 = new StaffId("N202400002");

            // Act
            var hashCode1 = staffId1.GetHashCode();
            var hashCode2 = staffId2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

    }
}