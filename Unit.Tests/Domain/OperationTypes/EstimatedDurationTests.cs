using System;
using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class EstimatedDurationTests
    {
        [Test]
        public void TestConstructor()
        {
            var estimatedDuration = new EstimatedDuration("50");
            Assert.AreEqual("50", estimatedDuration.EstimatedDurationValue);
        }
        
        [Test]
        public void TestToString()
        {
            var estimatedDuration = new EstimatedDuration("1:30");
            Assert.AreEqual("1:30", estimatedDuration.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var estimatedDuration1 = new EstimatedDuration("1:30");
            var estimatedDuration2 = new EstimatedDuration("1:30");
            Assert.AreEqual(estimatedDuration1, estimatedDuration2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var estimatedDuration1 = new EstimatedDuration("1:30");
            var estimatedDuration2 = new EstimatedDuration("1:30");
            
            // Act
            var hashCode1 = estimatedDuration1.GetHashCode();
            var hashCode2 = estimatedDuration2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var estimatedDuration1 = new EstimatedDuration("1:30");
            var estimatedDuration2 = new EstimatedDuration("2:00");

            // Act
            var hashCode1 = estimatedDuration1.GetHashCode();
            var hashCode2 = estimatedDuration2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var estimatedDuration = (EstimatedDuration)Activator.CreateInstance(typeof(EstimatedDuration), true);

            Assert.NotNull(estimatedDuration);
            Assert.IsNull(estimatedDuration.EstimatedDurationValue);
        }
        
    }
}