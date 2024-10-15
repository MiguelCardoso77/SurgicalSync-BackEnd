using NUnit.Framework;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using System;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.SurgicalSyncTests.Application.Mappers
{
    public class OperationRequestMapperTests
    {
        private OperationRequest _request;
        private OperationRequestDto _dto;

        [SetUp]
        public void Setup()
        {
            var operationRequestId = new OperationRequestId("123");
            var priority = Priority.EmergencySurgery;
            var deadlineDate = new DeadlineDate(DateTime.Now.AddDays(10));
            var operationTypeId = new OperationTypeId("456");
            var medicalRecordNumber = new MedicalRecordNumber("123");
            var licenseNumber = new LicenseNumber("N202400001");

            _request = new OperationRequest(operationRequestId, priority, deadlineDate, operationTypeId, medicalRecordNumber, licenseNumber);

            _dto = new OperationRequestDto
            {
                OperationRequestId = "123",
                DeadlineDate = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd"),
                Priority = "EmergencySurgery",
                OperationTypeId = "456",
                MedicalRecordNumber = "123",
                LicenseNumber = "N202400001"
            };
        }

        [Test]
        public void ToDto_ValidDomainModel_ShouldReturnCorrectDto()
        {
            var dtoResult = OperationRequestMapper.ToDto(_request);

            Assert.NotNull(dtoResult);
            Assert.AreEqual(_request.Id.AsString(), dtoResult.OperationRequestId);
            Assert.AreEqual(_request.DeadlineDate.ToString(), dtoResult.DeadlineDate);
            Assert.AreEqual(_request.Priority.ToString(), dtoResult.Priority);
            Assert.AreEqual(_request.OperationTypeId.AsString(), dtoResult.OperationTypeId);
            Assert.AreEqual(_request.MedicalRecordNumber.AsString(), dtoResult.MedicalRecordNumber);
            Assert.AreEqual(_request.LicenseNumber.AsString(), dtoResult.LicenseNumber);
        }

        [Test]
        public void ToDomain_ValidDto_ShouldReturnCorrectDomainModel()
        {
            var operationRequestId = new OperationRequestId(_dto.OperationRequestId);
            var deadlineDate = new DeadlineDate(DateTime.Parse(_dto.DeadlineDate));
            var priority = Enum.Parse<Priority>(_dto.Priority);
            var operationTypeId = new OperationTypeId(_dto.OperationTypeId);
            var medicalRecordNumber = new MedicalRecordNumber(_dto.MedicalRecordNumber);
            var licenseNumber = new LicenseNumber(_dto.LicenseNumber);

            var requestResult = OperationRequestMapper.ToDomain(_dto, operationRequestId, deadlineDate, priority, operationTypeId, medicalRecordNumber, licenseNumber);

            Assert.NotNull(requestResult);
            Assert.AreEqual(_dto.OperationRequestId, requestResult.Id.AsString());
            Assert.AreEqual(_dto.DeadlineDate, requestResult.DeadlineDate.ToString());
            Assert.AreEqual(_dto.Priority, requestResult.Priority.ToString());
            Assert.AreEqual(_dto.OperationTypeId, requestResult.OperationTypeId.AsString());
            Assert.AreEqual(_dto.MedicalRecordNumber, requestResult.MedicalRecordNumber.AsString());
            Assert.AreEqual(_dto.LicenseNumber, requestResult.LicenseNumber.AsString());
        }
    }
}
