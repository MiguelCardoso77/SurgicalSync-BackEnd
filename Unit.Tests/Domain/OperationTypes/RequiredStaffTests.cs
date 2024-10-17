using System;
using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class RequiredStaffTests
    {
        [Test]
        public void TestConstructor()
        {
            var requiredStaff = new RequiredStaff("Surgeon");
            Assert.AreEqual("Surgeon", requiredStaff.RequiredStaffValue);
        }
        
        [Test]
        public void TestToString()
        {
            var requiredStaff = new RequiredStaff("Surgeon");
            Assert.AreEqual("Surgeon", requiredStaff.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var requiredStaff1 = new RequiredStaff("Surgeon");
            var requiredStaff2 = new RequiredStaff("Surgeon");
            Assert.AreEqual(requiredStaff1, requiredStaff2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var requiredStaff1 = new RequiredStaff("Surgeon");
            var requiredStaff2 = new RequiredStaff("Surgeon");
            
            // Act
            var hashCode1 = requiredStaff1.GetHashCode();
            var hashCode2 = requiredStaff2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var requiredStaff1 = new RequiredStaff("Surgeon");
            var requiredStaff2 = new RequiredStaff("Nurse");

            // Act
            var hashCode1 = requiredStaff1.GetHashCode();
            var hashCode2 = requiredStaff2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var requiredStaff = (RequiredStaff)Activator.CreateInstance(typeof(RequiredStaff), true);

            Assert.NotNull(requiredStaff);
            Assert.IsNull(requiredStaff.RequiredStaffValue);
        }
        
    }
}