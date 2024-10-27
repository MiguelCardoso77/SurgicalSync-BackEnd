using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Staffs;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Services
{
    [TestFixture]
    public class OperationRequestServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOperationRequestRepository> _repoMock;
        private Mock<IPatientRepository> _patientMock;
        private Mock<ILogger<OperationRequestService>> _loggerMock;
        private OperationRequestMapper _mapper;
        private PatientMapper _patientMapper;
        private OperationRequestService _operationRequestService;
        private PatientNameMicroService _patientNameMicroService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IOperationRequestRepository>();
            _patientMock = new Mock<IPatientRepository>();
            _loggerMock = new Mock<ILogger<OperationRequestService>>();
            _mapper = new OperationRequestMapper();
            _patientMapper = new PatientMapper();
            _patientNameMicroService = new PatientNameMicroService(
                _unitOfWorkMock.Object,
                _patientMock.Object,
                _repoMock.Object
            );

            _operationRequestService = new OperationRequestService(
                _unitOfWorkMock.Object,
                _repoMock.Object,
                _loggerMock.Object,
                _mapper,
                _patientNameMicroService);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsOperationRequestDto_WhenOperationRequestExists()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var priority = Priority.ElectiveSurgery;
            var deadlineDate = new DeadlineDate(new DateTime(2025, 10, 1));
            var operationTypeId = new OperationTypeId("2");
            var medicalRecordNumber = new MedicalRecordNumber("202411000001");
            var licenseNumber = new StaffId("D202400001");

            var operationRequest = new OperationRequest(
                operationRequestId,
                priority,
                deadlineDate,
                operationTypeId,
                medicalRecordNumber,
                licenseNumber
            );

            var expectedDto = new OperationRequestDto
            {
                OperationRequestId = operationRequestId.AsString(),
                DeadlineDate = deadlineDate.Date.ToString("yyyy-MM-dd"),
                StaffId = licenseNumber.AsString(),
                Priority = priority.ToString(),
                OperationTypeId = operationTypeId.AsString(),
                MedicalRecordNumber = medicalRecordNumber.AsString()
            };

            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId))
                     .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.GetByIdAsync(operationRequestId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto.OperationRequestId, result.OperationRequestId);
            Assert.AreEqual(expectedDto.MedicalRecordNumber, result.MedicalRecordNumber);
            Assert.AreEqual(expectedDto.StaffId, result.StaffId);
            Assert.AreEqual(expectedDto.OperationTypeId, result.OperationTypeId);
            Assert.AreEqual(expectedDto.Priority, result.Priority);
            Assert.AreEqual(expectedDto.DeadlineDate, result.DeadlineDate);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId)).ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.GetByIdAsync(operationRequestId);

            // Assert
            Assert.IsNull(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfOperationRequestDto_WhenRequestsExist()
        {   
            // Arrange
            var operationRequestId1 = new OperationRequestId("1");
            var operationRequestId2 = new OperationRequestId("2");
            var deadlineDate1 = new DeadlineDate(new DateTime(2025, 10, 1));
            var deadlineDate2 = new DeadlineDate(new DateTime(2025, 10, 2));
            var operationTypeId1 = new OperationTypeId("2");
            var operationTypeId2 = new OperationTypeId("3");
            var medicalRecordNumber1 = new MedicalRecordNumber("202411000001");
            var medicalRecordNumber2 = new MedicalRecordNumber("202411000002");
            var licenseNumber1 = new StaffId("D202400001");
            var licenseNumber2 = new StaffId("D202400002");

            var priority1 = Priority.ElectiveSurgery;
            var priority2 = Priority.UrgentSurgery;

            var operationRequest1 = new OperationRequest(
                operationRequestId1,
                priority1,
                deadlineDate1,
                operationTypeId1,
                medicalRecordNumber1,
                licenseNumber1
            );

            var operationRequest2 = new OperationRequest(
                operationRequestId2,
                priority2,
                deadlineDate2,
                operationTypeId2,
                medicalRecordNumber2,
                licenseNumber2
            );

            var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            var expectedDto = new List<OperationRequestDto>
            {
                new OperationRequestDto
                {
                    OperationRequestId = operationRequestId1.AsString(),
                    DeadlineDate = deadlineDate1.Date.ToString("yyyy-MM-dd"),
                    StaffId = licenseNumber1.AsString(),
                    Priority = priority1.ToString(),
                    OperationTypeId = operationTypeId1.AsString(),
                    MedicalRecordNumber = medicalRecordNumber1.AsString()
                },
                new OperationRequestDto
                {
                    OperationRequestId = operationRequestId2.AsString(),
                    DeadlineDate = deadlineDate2.Date.ToString("yyyy-MM-dd"),
                    StaffId = licenseNumber2.AsString(),
                    Priority = priority2.ToString(),
                    OperationTypeId = operationTypeId2.AsString(),
                    MedicalRecordNumber = medicalRecordNumber2.AsString()
                }
            };

            // Act
            var result = await _operationRequestService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto.Count, result.Count, "The number of elements in the result list does not match the expected list.");

            for (int i = 0; i < expectedDto.Count; i++)
            {
                Assert.AreEqual(expectedDto[i].OperationRequestId, result[i].OperationRequestId, $"Mismatch at index {i} for OperationRequestId.");
                Assert.AreEqual(expectedDto[i].MedicalRecordNumber, result[i].MedicalRecordNumber, $"Mismatch at index {i} for MedicalRecordNumber.");
                Assert.AreEqual(expectedDto[i].StaffId, result[i].StaffId, $"Mismatch at index {i} for LicenseNumber.");
                Assert.AreEqual(expectedDto[i].OperationTypeId, result[i].OperationTypeId, $"Mismatch at index {i} for OperationTypeId.");
                Assert.AreEqual(expectedDto[i].Priority, result[i].Priority, $"Mismatch at index {i} for Priority.");
                Assert.AreEqual(expectedDto[i].DeadlineDate, result[i].DeadlineDate, $"Mismatch at index {i} for DeadlineDate.");
            }

            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoRequestsExist()
        {
            // Arrange
            var emptyOperationRequests = new List<OperationRequest>();

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyOperationRequests);

            // Act
            var result = await _operationRequestService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "The result list is not empty as expected.");
            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task AddAsync_CreatesNewOperationRequest_WhenValidDtoIsProvided()
        {
            // Arrange
            var operationRequestDto = new OperationRequestDto
            {
                OperationRequestId = null,
                DeadlineDate = "2025-10-01",
                StaffId = "D202400001",
                Priority = Priority.ElectiveSurgery.ToString(),
                OperationTypeId = "2",
                MedicalRecordNumber = "202411000001"
            };

            var operationRequestId = new OperationRequestId(Guid.NewGuid().ToString());

            var domainOperationRequest = new OperationRequest(
                operationRequestId,
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                new OperationTypeId("2"),
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );

            // Act
            var result = await _operationRequestService.AddAsync(operationRequestDto);

            // Assert
            Assert.IsNotNull(result);
            _repoMock.Verify(repo => repo.AddAsync(It.IsAny<OperationRequest>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        [Test]
        public async Task UpdateAsync_UpdatesExistingOperationRequest_WhenValidDtoIsProvided()
        { 
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var operationRequestDto = new OperationRequestDto
            { 
                OperationRequestId = "1",
                DeadlineDate = "2025-10-01",
                StaffId = "D202400001",
                Priority = Priority.UrgentSurgery.ToString(),
                OperationTypeId = "2",
                MedicalRecordNumber = "202411000001"
            };

            var operationRequest = new OperationRequest(
                operationRequestId,
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                new OperationTypeId("2"),
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );

            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId))
             .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.UpdateAsync(operationRequestDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(operationRequestDto.Priority, result.Priority);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        { 
            // Arrange
            var operationRequestDto = new OperationRequestDto { OperationRequestId = "1" };
            _repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestId>())).ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.UpdateAsync(operationRequestDto);

            // Assert
            Assert.IsNull(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(It.IsAny<OperationRequestId>()), Times.Once);
        }

        [Test]
        public async Task InactivateAsync_ReturnsOperationRequestDto_WhenOperationRequestExists()
        { 
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var operationRequest = new OperationRequest(
                operationRequestId,
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                new OperationTypeId("2"),
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );
            
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId)).ReturnsAsync(operationRequest);
    
            // Act
            var result = await _operationRequestService.InactivateAsync(operationRequestId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(operationRequestId.AsString(), result.OperationRequestId);
            Assert.AreEqual(Priority.ElectiveSurgery.ToString(), result.Priority);
            Assert.AreEqual("2025-10-01", result.DeadlineDate);
            Assert.AreEqual("D202400001", result.StaffId);
            Assert.AreEqual("2", result.OperationTypeId);
            Assert.AreEqual("202411000001", result.MedicalRecordNumber);
    
            _repoMock.Verify(repo => repo.Remove(operationRequest), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }


        [Test]
        public async Task InactivateAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        {
        // Arrange
        var operationRequestId = new OperationRequestId("1");
        _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId)).ReturnsAsync((OperationRequest)null);

        // Act
        var result = await _operationRequestService.InactivateAsync(operationRequestId);

        // Assert
        Assert.IsNull(result);
        _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
        }

        [Test]
        public async Task GetAllByStatus_ReturnsFilteredRequests_WhenRequestsExist()
        {
            // Arrange
            var activeRequest = new OperationRequest(
                new OperationRequestId("1"),
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                new OperationTypeId("2"),
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );
            
            var inactiveRequest = new OperationRequest(
                new OperationRequestId("2"),
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 2)),
                new OperationTypeId("3"),
                new MedicalRecordNumber("202411000002"),
                new StaffId("D202400002")
            );

            inactiveRequest.IsActive = false;

            var operationRequests = new List<OperationRequest> { activeRequest, inactiveRequest };
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var activeResults = await _operationRequestService.GetAllByStatus(true);
            var inactiveResults = await _operationRequestService.GetAllByStatus(false);

            // Assert
            Assert.IsNotNull(activeResults);
            Assert.IsNotNull(inactiveResults);
    
            Assert.AreEqual(1, activeResults.Count, "Expected one active request to be returned.");
            Assert.AreEqual(activeRequest.Id.AsString(), activeResults.First().OperationRequestId, "Active request ID did not match.");

            Assert.AreEqual(1, inactiveResults.Count, "Expected one inactive request to be returned.");
            Assert.AreEqual(inactiveRequest.Id.AsString(), inactiveResults.First().OperationRequestId, "Inactive request ID did not match.");
        }

        [Test]
        public async Task GetAllInsideDateRange_ReturnsFilteredRequests()
        {
            // Arrange
            var start = "2025-09-01";
            var end = "2025-12-01";

            var operationRequest1 = new OperationRequest(
                new OperationRequestId("1"),
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                new OperationTypeId("2"),
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );

            var operationRequest2 = new OperationRequest(
                new OperationRequestId("2"),
                Priority.UrgentSurgery,
                new DeadlineDate(new DateTime(2025, 11, 15)),
                new OperationTypeId("3"),
                new MedicalRecordNumber("202411000002"),
                new StaffId("D202400002")
            );

            var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };
    
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetAllInsideDateRange(start, end);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            
            var request1 = result.First();
            Assert.AreEqual("1", request1.OperationRequestId);
            Assert.AreEqual(Priority.ElectiveSurgery.ToString(), request1.Priority);
            Assert.AreEqual("2025-10-01", request1.DeadlineDate);
            Assert.AreEqual("2", request1.OperationTypeId);
            Assert.AreEqual("202411000001", request1.MedicalRecordNumber);
            Assert.AreEqual("D202400001", request1.StaffId);

            var request2 = result.Last();
            Assert.AreEqual("2", request2.OperationRequestId);
            Assert.AreEqual(Priority.UrgentSurgery.ToString(), request2.Priority);
            Assert.AreEqual("2025-11-15", request2.DeadlineDate);
            Assert.AreEqual("3", request2.OperationTypeId);
            Assert.AreEqual("202411000002", request2.MedicalRecordNumber);
            Assert.AreEqual("D202400002", request2.StaffId);
        }
        [Test]
        public async Task GetAllInsideDateRange_ReturnsEmptyList_WhenDeadlineDateIsOutOfRange()
        {
            // Arrange
            var start = "2025-09-01";
            var end = "2025-09-30";

            var operationRequest1 = new OperationRequest(
                new OperationRequestId("1"),
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                new OperationTypeId("2"),
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );

            var operationRequest2 = new OperationRequest(
                new OperationRequestId("2"),
                Priority.UrgentSurgery,
                new DeadlineDate(new DateTime(2025, 11, 15)),
                new OperationTypeId("3"),
                new MedicalRecordNumber("202411000002"),
                new StaffId("D202400002")
            );

            var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };
    
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetAllInsideDateRange(start, end);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
        
        [Test] 
        public async Task GetAllByOperationType_ReturnsFilteredRequests_WhenOperationTypeIdExists()
        { 
            // Arrange
            var operationTypeId = new OperationTypeId("2");

            var operationRequest1 = new OperationRequest(
                new OperationRequestId("1"),
                Priority.ElectiveSurgery,
                new DeadlineDate(new DateTime(2025, 10, 1)),
                operationTypeId,
                new MedicalRecordNumber("202411000001"),
                new StaffId("D202400001")
            );

            var operationRequest2 = new OperationRequest(
                new OperationRequestId("2"),
                Priority.UrgentSurgery,
                new DeadlineDate(new DateTime(2025, 11, 15)),
                operationTypeId,
                new MedicalRecordNumber("202411000002"),
                new StaffId("D202400002")
            );

            var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };
            
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetAllByOperationType(operationTypeId.AsString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            // Validate details of the first request
            var request1 = result.First();
            Assert.AreEqual("1", request1.OperationRequestId);
            Assert.AreEqual(Priority.ElectiveSurgery.ToString(), request1.Priority);
            Assert.AreEqual("2025-10-01", request1.DeadlineDate.ToString());
            Assert.AreEqual("2", request1.OperationTypeId);
            Assert.AreEqual("202411000001", request1.MedicalRecordNumber); 
            Assert.AreEqual("D202400001", request1.StaffId);

            // Validate details of the second request
            var request2 = result.Last();
            Assert.AreEqual("2", request2.OperationRequestId); 
            Assert.AreEqual(Priority.UrgentSurgery.ToString(), request2.Priority);
            Assert.AreEqual("2025-11-15", request2.DeadlineDate.ToString());
            Assert.AreEqual("2", request2.OperationTypeId);
            Assert.AreEqual("202411000002", request2.MedicalRecordNumber);
            Assert.AreEqual("D202400002", request2.StaffId);
        }


        [Test]
        public async Task GetAllByOperationType_ReturnsEmptyList_WhenOperationTypeIdDoesNotExist()
        {
            // Arrange
            var nonExistentOperationTypeId = new OperationTypeId("999");
            var operationRequests = new List<OperationRequest>();

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetAllByOperationType(nonExistentOperationTypeId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
        
        [Test]
        public async Task GetAllByMedicalRecordNumber_ReturnsFilteredRequests_WhenMedicalRecordNumberExists()
        {
        // Arrange
        var medicalRecordNumber = new MedicalRecordNumber("202411000001");

        var operationRequest1 = new OperationRequest(
            new OperationRequestId("1"),
            Priority.ElectiveSurgery,
            new DeadlineDate(new DateTime(2025, 10, 1)),
            new OperationTypeId("2"),
            medicalRecordNumber,
            new StaffId("D202400001")
            );

        var operationRequest2 = new OperationRequest(
            new OperationRequestId("2"),
            Priority.UrgentSurgery,
            new DeadlineDate(new DateTime(2025, 11, 15)),
            new OperationTypeId("3"),
            new MedicalRecordNumber("202411000002"),
            new StaffId("D202400002")
        );

        var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };

        _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

        // Act
        var result = await _operationRequestService.GetAllByMedicalRecordNumber(medicalRecordNumber.ToString());

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(medicalRecordNumber.ToString(), result.First().MedicalRecordNumber); 
        }

        [Test]
        public async Task GetAllByMedicalRecordNumber_ReturnsEmptyList_WhenMedicalRecordNumberDoesNotExist()
        {
        // Arrange
        var medicalRecordNumber = new MedicalRecordNumber("202411000003");

        var operationRequest1 = new OperationRequest(
            new OperationRequestId("1"),
            Priority.ElectiveSurgery,
            new DeadlineDate(new DateTime(2025, 10, 1)),
            new OperationTypeId("2"),
            new MedicalRecordNumber("202411000001"),
            new StaffId("D202400001")
        );

        var operationRequest2 = new OperationRequest(
            new OperationRequestId("2"),
            Priority.UrgentSurgery,
            new DeadlineDate(new DateTime(2025, 11, 15)),
            new OperationTypeId("3"),
            new MedicalRecordNumber("202411000002"), 
            new StaffId("D202400002")
        );

        var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };

        _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

        // Act
        var result = await _operationRequestService.GetAllByMedicalRecordNumber(medicalRecordNumber.ToString());

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    }
}