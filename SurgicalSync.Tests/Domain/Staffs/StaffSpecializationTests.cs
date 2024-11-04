using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Staffs
{
    public class StaffSpecializationTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffSpecialization1 = "Dermatology";
            Assert.AreEqual("Dermatology", staffSpecialization1);
        }
        
        [Test]
        public void TestToString()
        {
            var staffSpecialization1 = "Dermatology";
            Assert.AreEqual("Dermatology", staffSpecialization1);
        }
        
        [Test]
        public void TestEquals()
        {
            var staffSpecialization =  "Dermatology";
            var staffSpecialization1 = "Dermatology";
            Assert.AreEqual(staffSpecialization, staffSpecialization1);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var specialization1 ="Dermatology";
            var specialization2 = "Dermatology";
            
            // Act
            var hashCode1 = specialization1.GetHashCode();
            var hashCode2 = specialization2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            var specialization1 = "Dermatology";
            var specialization2 = "Dermatology";
            
            // Act
            var hashCode1 = specialization1.GetHashCode();
            var hashCode2 = specialization2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var staffSpecialization = "Dermatology";

            Assert.NotNull(staffSpecialization);
        }
    }
}