using System;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class OperationRequestTest
    {
        private OperationRequestId _id;
        private Priority _priority;
        private DeadlineDate _deadlineDate;
        private OperationTypeId _operationTypeId;
        private MedicalRecordNumber _medicalRecordNumber;
        private LicenseNumber _licenseNumber;

        [SetUp]
        public void Setup()
        {
            _id = new OperationRequestId("1");
            _priority = Priority.UrgentSurgery;
            _deadlineDate = new DeadlineDate(DateTime.Now.AddDays(10));
            _operationTypeId = new OperationTypeId("123");
            _medicalRecordNumber = new MedicalRecordNumber("123");
            _licenseNumber = new LicenseNumber("N202400123");
        }
        [Test]
        public void Constructor_ValidParameters_ShouldCreateInstance()
        {
            var operationRequest = new OperationRequest(_id, _priority, _deadlineDate, _operationTypeId, _medicalRecordNumber, _licenseNumber);

            Assert.NotNull(operationRequest);
            Assert.AreEqual(_id, operationRequest.Id);
            Assert.AreEqual(_priority, operationRequest.Priority);
            Assert.AreEqual(_deadlineDate, operationRequest.DeadlineDate);
            Assert.AreEqual(_operationTypeId, operationRequest.OperationTypeId);
            Assert.AreEqual(_medicalRecordNumber, operationRequest.MedicalRecordNumber);
            Assert.AreEqual(_licenseNumber, operationRequest.LicenseNumber);
            Assert.IsTrue(operationRequest.IsActive);
        }
        
        [Test]
        public void ActivateOperationRequest_ShouldSetIsActiveToTrue()
        {
            var operationRequest = new OperationRequest(_id, _priority, _deadlineDate, _operationTypeId, _medicalRecordNumber, _licenseNumber);

            operationRequest.ActivateOperationRequest();

            Assert.IsTrue(operationRequest.IsActive);
        }
        
        [Test]
        public void DeactivateOperationRequest_ShouldSetIsActiveToFalse()
        {
            var operationRequest = new OperationRequest(_id, _priority, _deadlineDate, _operationTypeId, _medicalRecordNumber, _licenseNumber);

            operationRequest.DeactivateOperationRequest();

            Assert.IsFalse(operationRequest.IsActive);
        }

        [Test]
        public void Constructor_ShouldSetIsActiveToTrueByDefault()
        {
            var operationRequest = new OperationRequest(_id, _priority, _deadlineDate, _operationTypeId, _medicalRecordNumber, _licenseNumber);

            Assert.IsTrue(operationRequest.IsActive);
        }

        [Test]
        public void Properties_ShouldBeCorrectlySet()
        {
            var operationRequest = new OperationRequest(_id, _priority, _deadlineDate, _operationTypeId, _medicalRecordNumber, _licenseNumber);

            Assert.AreEqual(_id, operationRequest.Id);
            Assert.AreEqual(_priority, operationRequest.Priority);
            Assert.AreEqual(_deadlineDate, operationRequest.DeadlineDate);
            Assert.AreEqual(_operationTypeId, operationRequest.OperationTypeId);
            Assert.AreEqual(_medicalRecordNumber, operationRequest.MedicalRecordNumber);
            Assert.AreEqual(_licenseNumber, operationRequest.LicenseNumber);
        }

        [Test]
        public void Constructor_WithPrivateDefaultConstructor_ShouldSetDeadlineDateToNull()
        {
            var operationRequest = (OperationRequest)Activator.CreateInstance(typeof(OperationRequest), true);

            Assert.NotNull(operationRequest);
            Assert.IsNull(operationRequest.DeadlineDate);
        }
    }
}