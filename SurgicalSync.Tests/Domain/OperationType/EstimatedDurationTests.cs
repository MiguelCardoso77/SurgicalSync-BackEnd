using System;
using DDDNetCore.Domain.OperationType;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.OperationType
{
    [TestFixture]
    public class EstimatedDurationTests
    {
        [Test]
        public void TestConstructor()
        {
            var estimatedDuration = new EstimatedDuration("50");
            Assert.AreEqual(50, estimatedDuration.Value);
        }
        
        [Test]
        public void TestConstructorWithNonIntegerThrowsException()
        {
            const string invalidDuration = "A40";

            var ex = Assert.Throws<FormatException>(() => new EstimatedDuration(invalidDuration));

            Assert.That(ex.Message, Is.EqualTo("Estimated duration must be a positive integer."));
        }
        
        [Test]
        public void TestToString()
        {
            var estimatedDuration = new EstimatedDuration("130");
            Assert.AreEqual("130", estimatedDuration.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var estimatedDuration1 = new EstimatedDuration("130");
            var estimatedDuration2 = new EstimatedDuration("130");
            Assert.AreEqual(estimatedDuration1, estimatedDuration2);
        }
        
        [Test]
        public void TestNotEquals()
        {
            var estimatedDuration1 = new EstimatedDuration("130");
            var estimatedDuration2 = new EstimatedDuration("200");
            Assert.AreNotEqual(estimatedDuration1, estimatedDuration2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var estimatedDuration1 = new EstimatedDuration("130");
            var estimatedDuration2 = new EstimatedDuration("130");
            
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
            var estimatedDuration1 = new EstimatedDuration("130");
            var estimatedDuration2 = new EstimatedDuration("200");

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
            Assert.AreEqual(0, estimatedDuration.Value);
        }
        
    }
}