using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using Moq;
using NUnit.Framework;
using System;
using DDDNetCore.Domain.OperationType;

namespace DDDNetCore.Unit.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class OperationRequestTest
    {
        private Mock<OperationRequestId> _mockOperationRequestId;
        private Mock<DeadlineDate> _mockDeadlineDate;
        private Mock<OperationTypeId> _mockOperationTypeId;
        private Mock<StaffId> _mockStaffId;
        private Mock<MedicalRecordNumber> _mockMedicalRecordNumber;
        private Mock<DeadlineDate> _mockNewDeadlineDate;

        private Priority _mockPriority;
        private Priority _mockNewPriority;

        [SetUp]
        public void Setup()
        {
            _mockOperationRequestId = new Mock<OperationRequestId>("1");
            _mockDeadlineDate = new Mock<DeadlineDate>(new DateTime(2025, 10, 1));
            _mockNewDeadlineDate = new Mock<DeadlineDate>(new DateTime(2025, 11, 1));
            _mockOperationTypeId = new Mock<OperationTypeId>("2");
            _mockStaffId = new Mock<StaffId>("D202400001");
            _mockMedicalRecordNumber = new Mock<MedicalRecordNumber>("202511000001");

            _mockPriority = Priority.ElectiveSurgery;
            _mockNewPriority = Priority.UrgentSurgery;
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateInstance()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            Assert.NotNull(operationRequest);
            Assert.AreEqual(_mockOperationRequestId.Object, operationRequest.Id);
            Assert.AreEqual(_mockPriority, operationRequest.Priority);
            Assert.AreEqual(_mockDeadlineDate.Object, operationRequest.DeadlineDate);
            Assert.AreEqual(_mockOperationTypeId.Object, operationRequest.OperationTypeId);
            Assert.AreEqual(_mockMedicalRecordNumber.Object, operationRequest.MedicalRecordNumber);
            Assert.AreEqual(_mockStaffId.Object, operationRequest.StaffId);
            Assert.IsTrue(operationRequest.IsActive);
        }

        [Test]
        public void ActivateOperationRequest_ShouldSetIsActiveToTrue()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            operationRequest.ActivateOperationRequest();

            Assert.IsTrue(operationRequest.IsActive);
        }

        [Test]
        public void DeactivateOperationRequest_ShouldSetIsActiveToFalse()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            operationRequest.DeactivateOperationRequest();

            Assert.IsFalse(operationRequest.IsActive);
        }

        [Test]
        public void Constructor_ShouldSetIsActiveToTrueByDefault()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            Assert.IsTrue(operationRequest.IsActive);
        }

        [Test]
        public void Properties_ShouldBeCorrectlySet()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            Assert.AreEqual(_mockOperationRequestId.Object, operationRequest.Id);
            Assert.AreEqual(_mockPriority, operationRequest.Priority);
            Assert.AreEqual(_mockDeadlineDate.Object, operationRequest.DeadlineDate);
            Assert.AreEqual(_mockOperationTypeId.Object, operationRequest.OperationTypeId);
            Assert.AreEqual(_mockMedicalRecordNumber.Object, operationRequest.MedicalRecordNumber);
            Assert.AreEqual(_mockStaffId.Object, operationRequest.StaffId);
        }

        [Test]
        public void Constructor_WithPrivateDefaultConstructor_ShouldSetDeadlineDateToNull()
        {
            var operationRequest = (OperationRequest)Activator.CreateInstance(typeof(OperationRequest), true);

            Assert.NotNull(operationRequest);
            Assert.IsNull(operationRequest.DeadlineDate);
        }

        [Test]
        public void ChangeDeadlineDate_ValidNewDate_ShouldUpdateDeadlineDate()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            operationRequest.ChangeDeadlineDate(_mockNewDeadlineDate.Object);

            Assert.AreEqual(_mockNewDeadlineDate.Object, operationRequest.DeadlineDate);
        }

        [Test]
        public void ChangePriority_ValidNewPriority_ShouldUpdatePriority()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            operationRequest.ChangePriority(_mockNewPriority);

            Assert.AreEqual(_mockNewPriority, operationRequest.Priority);
        }
    }
}
