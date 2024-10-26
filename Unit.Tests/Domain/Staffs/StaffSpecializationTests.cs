using System;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.Staffs
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
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var specialization1 = StaffSpecialization.Dermatology;
            var specialization2 = StaffSpecialization.Dermatology;
            
            // Act
            var hashCode1 = specialization1.GetHashCode();
            var hashCode2 = specialization2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            var specialization1 = StaffSpecialization.Dermatology;
            var specialization2 = StaffSpecialization.Cardiology;
            
            // Act
            var hashCode1 = specialization1.GetHashCode();
            var hashCode2 = specialization2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var staffSpecialization = StaffSpecialization.None;

            Assert.NotNull(staffSpecialization);
        }
    }
}