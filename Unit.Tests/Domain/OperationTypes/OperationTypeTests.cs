using System;
using System.Collections.Generic;
using DDDNetCore.Domain.OperationTypes;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class OperationTypeTests
    {
        private Mock<OperationTypeId> _mockOperationTypeId;
        private Mock<OperationName> _mockOperationName;
        private List<RequiredStaff> _mockRequiredStaff;
        private List<EstimatedDuration> _mockEstimatedDuration;
        
        [SetUp]
        public void SetUp()
        {
            _mockOperationTypeId = new Mock<OperationTypeId>("1");
            _mockOperationName = new Mock<OperationName>("Surgery");
            
            _mockRequiredStaff = new List<RequiredStaff>();
            _mockEstimatedDuration = new List<EstimatedDuration>();
        }
        
        [Test]
        public void TestConstructor()
        {
            var operationType = new OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff,
                _mockEstimatedDuration
            );
            
            Assert.AreEqual("Surgery", operationType.Name.OperationNameValue);
            Assert.AreEqual(0, operationType.RequiredStaff.Count);
            Assert.AreEqual(0, operationType.EstimatedDuration.Count);
        }
        
        [Test]
        public void TestActivateOperationType()
        {
            var operationType = new OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff,
                _mockEstimatedDuration
            );
            
            operationType.ActivateOperationType();
            
            Assert.IsTrue(operationType.IsActive);
        }
        
        [Test]
        public void TestDeactivateOperationType()
        {
            var operationType = new OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff,
                _mockEstimatedDuration
            );
            
            operationType.DeactivateOperationType();
            
            Assert.IsFalse(operationType.IsActive);
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var operationType = (OperationType)Activator.CreateInstance(typeof(OperationType), true);

            Assert.NotNull(operationType);
            Assert.IsNull(operationType.RequiredStaff);
        }
        
    }
}