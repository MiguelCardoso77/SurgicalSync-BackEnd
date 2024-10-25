using System;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Mappers
{
    [TestFixture]
    public class OperationRequestMapperTests
    {
        private OperationRequestMapper _mapper;
        private Mock<OperationRequestId> _mockOperationRequestId;
        private Mock<DeadlineDate> _mockDeadlineDate;
        private Mock<OperationTypeId> _mockOperationTypeId;
        private Mock<StaffId> _mockStaffId;
        private Mock<MedicalRecordNumber> _mockMedicalRecordNumber;

        private Priority _mockPriority;

        [SetUp]
        public void Setup()
        {
            _mapper = new OperationRequestMapper();
            _mockOperationRequestId = new Mock<OperationRequestId>("1");
            _mockDeadlineDate = new Mock<DeadlineDate>(new DateTime(2025, 10, 1));
            _mockOperationTypeId = new Mock<OperationTypeId>("2");
            _mockStaffId = new Mock<StaffId>("D202400001");
            _mockMedicalRecordNumber = new Mock<MedicalRecordNumber>("123456");

            _mockPriority = Priority.ElectiveSurgery;
            _mockDeadlineDate.Setup(m => m.ToString()).Returns("2025-10-01");
        }

        [Test]
        public void ToDto_ValidDomain_ReturnsCorrectDto()
        {
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockStaffId.Object
            );

            var dto = _mapper.ToDto(operationRequest);

            Assert.AreEqual(_mockOperationRequestId.Object.AsString(), dto.OperationRequestId);
            Assert.AreEqual("2025-10-01", dto.DeadlineDate);
            Assert.AreEqual(_mockStaffId.Object.AsString(), dto.StaffId);
            Assert.AreEqual(_mockPriority.ToString(), dto.Priority);
            Assert.AreEqual(_mockOperationTypeId.Object.AsString(), dto.OperationTypeId);
            Assert.AreEqual(_mockMedicalRecordNumber.Object.AsString(), dto.MedicalRecordNumber);
        }

        [Test]
        public void ToDomain_ValidDto_ReturnsCorrectDomain()
        {
            var dto = new OperationRequestDto()
            {
                OperationRequestId = "1",
                DeadlineDate = "2025-10-01",
                StaffId = "D202400001",
                Priority = _mockPriority.ToString(),
                OperationTypeId = "2",
                MedicalRecordNumber = "123456"
            };

            var operationRequestId = new OperationRequestId("1");
            var operationRequest = _mapper.ToDomain(dto, operationRequestId);
            
            Assert.AreEqual(operationRequestId, operationRequest.Id);
            Assert.AreEqual(_mockPriority, operationRequest.Priority); 
            Assert.AreEqual(new DeadlineDate(DateTime.Parse(dto.DeadlineDate)), operationRequest.DeadlineDate);
            Assert.AreEqual(new OperationTypeId(dto.OperationTypeId), operationRequest.OperationTypeId);
            Assert.AreEqual(new MedicalRecordNumber(dto.MedicalRecordNumber), operationRequest.MedicalRecordNumber);
            Assert.AreEqual(new StaffId(dto.StaffId), operationRequest.StaffId);
        }
    }
}
