using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffEmailTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffEmail = new StaffEmail("raquelgoncalves@gmail.com");
            Assert.AreEqual("raquelgoncalves@gmail.com", staffEmail.StaffEmailValue);
        }
        
        [Test]
        public void TestToString()
        {
            var staffEmail = new StaffEmail("raquelgoncalves@gmail.com");
            Assert.AreEqual("raquelgoncalves@gmail.com", staffEmail.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffEmail1 = new StaffEmail("raquelgoncalves@gmail.com");
            var staffEmail2 = new StaffEmail("raquelgoncalves@gmail.com");
            Assert.AreEqual(staffEmail1, staffEmail2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var staffEmail1 = new StaffEmail("raquelgoncalves@gmail.com");
            var staffEmail2 = new StaffEmail("raquelgoncalves@gmail.com");
            
            // Act
            var hashCode1 = staffEmail1.GetHashCode();
            var hashCode2 = staffEmail2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var staffEmail1 = new StaffEmail("raquelgoncalves@gmail.com");
            var staffEmail2 = new StaffEmail("tomasgoncalves@gmail.com");
            
            // Act
            var hashCode1 = staffEmail1.GetHashCode();
            var hashCode2 = staffEmail2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

    }
}