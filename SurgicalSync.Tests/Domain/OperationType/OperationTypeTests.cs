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
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff,
                _mockEstimatedDuration
            );
            
            Assert.AreEqual("Surgery", operationType.Name.Value);
            //Assert.AreEqual(0, operationType.RequiredStaff.Count);
            //Assert.AreEqual(0, operationType.EstimatedDuration.Count);
        }

        [Test]
        public void TestChangeOperationTypeName()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff,
                _mockEstimatedDuration
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
                _mockRequiredStaff,
                _mockEstimatedDuration
            );
            
            var newRequiredStaff = new List<RequiredStaff>() { new RequiredStaff("Doctor") };
            operationType.ChangeRequiredStaff(newRequiredStaff);
            
            //Assert.AreEqual("Doctor", operationType.RequiredStaff[0].Value);
        }
        
        [Test]
        public void TestChangeEstimatedDuration()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
                _mockOperationTypeId.Object,
                _mockOperationName.Object,
                _mockRequiredStaff,
                _mockEstimatedDuration
            );
            
            var newEstimatedDuration = new List<EstimatedDuration>() { new EstimatedDuration("60") };
            operationType.ChangeEstimatedDuration(newEstimatedDuration);
            
            //Assert.AreEqual("60", operationType.EstimatedDuration[0].Value.ToString());
        }
        
        [Test]
        public void TestActivateOperationType()
        {
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
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
            var operationType = new DDDNetCore.Domain.OperationType.OperationType(
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
            var operationType = (DDDNetCore.Domain.OperationType.OperationType)Activator.CreateInstance(typeof(DDDNetCore.Domain.OperationType.OperationType), true);

            Assert.NotNull(operationType);
            Assert.IsNull(operationType.RequiredStaff);
        }
        
    }
}