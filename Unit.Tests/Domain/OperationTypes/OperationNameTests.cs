using System;
using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class OperationNameTests
    {
        [Test]
        public void TestConstructor()
        {
            var operationName = new OperationName("Ankle Surgery");
            Assert.AreEqual("Ankle Surgery", operationName.OperationNameValue);
        }
        
        [Test]
        public void TestToString()
        {
            var operationName = new OperationName("Ankle Surgery");
            Assert.AreEqual("Ankle Surgery", operationName.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var operationName1 = new OperationName("Ankle Surgery");
            var operationName2 = new OperationName("Ankle Surgery");
            Assert.AreEqual(operationName1, operationName2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var operationName1 = new OperationName("Ankle Surgery");
            var operationName2 = new OperationName("Ankle Surgery");
            
            // Act
            var hashCode1 = operationName1.GetHashCode();
            var hashCode2 = operationName2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var operationName1 = new OperationName("Ankle Surgery");
            var operationName2 = new OperationName("Knee Surgery");

            // Act
            var hashCode1 = operationName1.GetHashCode();
            var hashCode2 = operationName2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var operationName = (OperationName)Activator.CreateInstance(typeof(OperationName), true);

            Assert.NotNull(operationName);
            Assert.IsNull(operationName.OperationNameValue);
        }
        
    }
}