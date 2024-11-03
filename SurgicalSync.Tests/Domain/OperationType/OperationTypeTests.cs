using System;
using System.Collections.Generic;
using DDDNetCore.Domain.OperationType;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.OperationType
{
    [TestFixture]
    public class OperationTypeTests
    {
        private Mock<OperationTypeId> _mockOperationTypeId;
        private Mock<OperationName> _mockOperationName;
        private Mock<RequiredStaff> _mockRequiredStaff;
        private Mock<EstimatedDuration> _mockEstimatedDuration;
        
        [SetUp]
        public void SetUp()
        {
            _mockOperationTypeId = new Mock<OperationTypeId>("1");
            _mockOperationName = new Mock<OperationName>("Surgery");
            
            _mockRequiredStaff = new Mock<RequiredStaff>("Nurse");
            _mockEstimatedDuration = new Mock<EstimatedDuration>("20");
        }
        
        [Test]
        public void TestConstructor()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff.Object,
                _mockEstimatedDuration.Object
            );
            
            Assert.AreEqual("Surgery", operationType.Name.Value);
            Assert.AreEqual("Nurse", operationType.RequiredStaff.Value);
            Assert.AreEqual("20", operationType.EstimatedDuration.Value);
        }

        [Test]
        public void TestChangeOperationTypeName()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff.Object,
                _mockEstimatedDuration.Object
            );
            
            var newOperationName = new Mock<OperationName>("New Surgery");
            operationType.ChangeOperationTypeName(newOperationName.Object);
            
            Assert.AreEqual("New Surgery", operationType.Name.Value);
        }
        
        [Test]
        public void TestChangeRequiredStaff()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff.Object,
                _mockEstimatedDuration.Object
            );
            
            var newRequiredStaff = new Mock<RequiredStaff>("Doctor");
            operationType.ChangeRequiredStaff(newRequiredStaff.Object);
            
            Assert.AreEqual("Doctor", operationType.RequiredStaff.Value);
        }
        
        [Test]
        public void TestChangeEstimatedDuration()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff.Object,
                _mockEstimatedDuration.Object
            );
            
            var newEstimatedDuration = new Mock<EstimatedDuration>("60");
            operationType.ChangeEstimatedDuration(newEstimatedDuration.Object);
            
            Assert.AreEqual("60", operationType.EstimatedDuration.Value.ToString());
        }
        
        [Test]
        public void TestActivateOperationType()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff.Object,
                _mockEstimatedDuration.Object
            );
            
            operationType.ActivateOperationType();
            
            Assert.IsTrue(operationType.IsActive);
        }
        
        [Test]
        public void TestDeactivateOperationType()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff.Object,
                _mockEstimatedDuration.Object
            );
            
            operationType.DeactivateOperationType();
            
            Assert.IsFalse(operationType.IsActive);
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var operationType = (DDDNetCore.Domain.OperationType.OperationType)Activator.CreateInstance(typeof(DDDNetCore.Domain.OperationType.OperationType), true);

            Assert.NotNull(operationType);
            Assert.IsNull(operationType.RequiredStaff);
        }
        
    }
}