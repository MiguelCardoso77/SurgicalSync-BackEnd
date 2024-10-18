using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using Xunit;

namespace DDDNetCore.Unit.Tests.Application.Services
{
    public class OperationRequestServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOperationRequestRepository> _repoMock;
        private readonly Mock<ILogger<OperationRequestService>> _loggerMock;
        private readonly OperationRequestMapper _mapper;
        private readonly OperationRequestService _operationRequestService;

        public OperationRequestServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IOperationRequestRepository>();
            _loggerMock = new Mock<ILogger<OperationRequestService>>();
            _mapper = new OperationRequestMapper();

            _operationRequestService = new OperationRequestService(
                _unitOfWorkMock.Object,
                _repoMock.Object,
                _loggerMock.Object,
                _mapper);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOperationRequestDto_WhenOperationRequestExists()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var priority = Priority.ElectiveSurgery;
            var deadlineDate = new DeadlineDate(new DateTime(2025, 10, 1));
            var operationTypeId = new OperationTypeId("2");
            var medicalRecordNumber = new MedicalRecordNumber("202411000001");
            var licenseNumber = new LicenseNumber("D202400001");

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
                LicenseNumber = licenseNumber.AsString(),
                Priority = priority.ToString(),
                OperationTypeId = operationTypeId.AsString(),
                MedicalRecordNumber = medicalRecordNumber.AsString()
            };

            // Setting up the repository to return the mock operation request
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId))
                     .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.GetByIdAsync(operationRequestId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.OperationRequestId, result.OperationRequestId);
            Assert.Equal(expectedDto.MedicalRecordNumber, result.MedicalRecordNumber);
            Assert.Equal(expectedDto.LicenseNumber, result.LicenseNumber);
            Assert.Equal(expectedDto.OperationTypeId, result.OperationTypeId);
            Assert.Equal(expectedDto.Priority, result.Priority);
            Assert.Equal(expectedDto.DeadlineDate, result.DeadlineDate);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
        }
        
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId)).ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.GetByIdAsync(operationRequestId);

            // Assert
            Assert.Null(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
        }
        
        [Fact]
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
            var licenseNumber1 = new LicenseNumber("D202400001");
            var licenseNumber2 = new LicenseNumber("D202400002");

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
                    LicenseNumber = licenseNumber1.AsString(),
                    Priority = priority1.ToString(),
                    OperationTypeId = operationTypeId1.AsString(),
                    MedicalRecordNumber = medicalRecordNumber1.AsString()
                },
                new OperationRequestDto
                {
                    OperationRequestId = operationRequestId2.AsString(),
                    DeadlineDate = deadlineDate2.Date.ToString("yyyy-MM-dd"),
                    LicenseNumber = licenseNumber2.AsString(),
                    Priority = priority2.ToString(),
                    OperationTypeId = operationTypeId2.AsString(),
                    MedicalRecordNumber = medicalRecordNumber2.AsString()
                }
            };

            // Act
            var result = await _operationRequestService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Count, result.Count);
            for (int i = 0; i < expectedDto.Count; i++)
            {
                Assert.Equal(expectedDto[i].OperationRequestId, result[i].OperationRequestId);
                Assert.Equal(expectedDto[i].MedicalRecordNumber, result[i].MedicalRecordNumber);
                Assert.Equal(expectedDto[i].LicenseNumber, result[i].LicenseNumber);
                Assert.Equal(expectedDto[i].OperationTypeId, result[i].OperationTypeId);
                Assert.Equal(expectedDto[i].Priority, result[i].Priority);
                Assert.Equal(expectedDto[i].DeadlineDate, result[i].DeadlineDate);
            }
            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoRequestsExist()
        {
            // Arrange
            var emptyOperationRequests = new List<OperationRequest>();
    
            // Setting up the repository to return an empty list
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyOperationRequests);

            // Act
            var result = await _operationRequestService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Ensure the result is an empty list
            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
        
        [Fact]
        public async Task AddAsync_CreatesNewOperationRequest_WhenValidDtoIsProvided()
        {
            // Arrange
            var operationRequestDto = new OperationRequestDto
            {
                OperationRequestId = null, // Simulate a new request (ID will be generated)
                DeadlineDate = "2025-10-01",
                LicenseNumber = "D202400001",
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
                new LicenseNumber("D202400001")
            );
            
            // Act
            await _operationRequestService.AddAsync(operationRequestDto);

            // Assert
            _repoMock.Verify(repo => repo.AddAsync(It.Is<OperationRequest>(op => 
                op.Priority == domainOperationRequest.Priority &&
                op.DeadlineDate.Date == domainOperationRequest.DeadlineDate.Date &&
                op.OperationTypeId.AsString() == domainOperationRequest.OperationTypeId.AsString() &&
                op.MedicalRecordNumber.AsString() == domainOperationRequest.MedicalRecordNumber.AsString() &&
                op.LicenseNumber.AsString() == domainOperationRequest.LicenseNumber.AsString()
            )), Times.Once);
            
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        [Fact]
        public async Task AddAsync_ThrowsArgumentNullException_WhenDtoIsNull()
        {
            // Arrange
            OperationRequestDto nullDto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _operationRequestService.AddAsync(nullDto));
    
            // Verify that neither AddAsync nor CommitAsync were called on the mocks
            _repoMock.Verify(repo => repo.AddAsync(It.IsAny<OperationRequest>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }
        
        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedOperationRequestDto_WhenOperationRequestExists()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var existingDeadlineDate = new DeadlineDate(new DateTime(2025, 10, 1));
            var updatedDeadlineDate = new DeadlineDate(new DateTime(2025, 12, 1));
            var priority = Priority.ElectiveSurgery;
            var operationTypeId = new OperationTypeId("2");
            var medicalRecordNumber = new MedicalRecordNumber("202411000001");
            var licenseNumber = new LicenseNumber("D202400001");

            var operationRequest = new OperationRequest(
                operationRequestId,
                priority,
                existingDeadlineDate,
                operationTypeId,
                medicalRecordNumber,
                licenseNumber
            );

            var operationRequestDto = new OperationRequestDto
            {
                OperationRequestId = operationRequestId.AsString(),
                DeadlineDate = updatedDeadlineDate.Date.ToString("yyyy-MM-dd"),
                LicenseNumber = licenseNumber.AsString(),
                Priority = Priority.UrgentSurgery.ToString(),
                OperationTypeId = operationTypeId.AsString(),
                MedicalRecordNumber = medicalRecordNumber.AsString()
            };

            // Setting up the repository to return the existing operation request
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId))
                .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.UpdateAsync(operationRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(operationRequestDto.OperationRequestId, result.OperationRequestId);
            Assert.Equal(updatedDeadlineDate.Date.ToString("yyyy-MM-dd"), result.DeadlineDate);
            Assert.Equal(operationRequestDto.Priority, result.Priority);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        
        [Fact]
        public async Task UpdateAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var operationRequestDto = new OperationRequestDto
            {
                OperationRequestId = operationRequestId.AsString(),
                DeadlineDate = "2025-12-01",
                LicenseNumber = "D202400001",
                Priority = Priority.UrgentSurgery.ToString(),
                OperationTypeId = "2",
                MedicalRecordNumber = "202411000001"
            };

            // Setting up the repository to return null when looking for the operation request
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId)).ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.UpdateAsync(operationRequestDto);

            // Assert
            Assert.Null(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }
        
        [Fact]
        public async Task InactivateAsync_ReturnsOperationRequestDto_WhenOperationRequestExists()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
            var priority = Priority.ElectiveSurgery;
            var existingDeadlineDate = new DeadlineDate(new DateTime(2025, 10, 1));
            var operationTypeId = new OperationTypeId("2");
            var medicalRecordNumber = new MedicalRecordNumber("202411000001");
            var licenseNumber = new LicenseNumber("D202400001");

            var operationRequest = new OperationRequest(
                operationRequestId,
                priority,
                existingDeadlineDate,
                operationTypeId,
                medicalRecordNumber,
                licenseNumber
            );

            // Setting up the repository to return the existing operation request
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId))
                .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.InactivateAsync(operationRequestId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(operationRequestId.AsString(), result.OperationRequestId);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
            _repoMock.Verify(repo => repo.Remove(operationRequest), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        [Fact]
        public async Task InactivateAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        {
            // Arrange
            var operationRequestId = new OperationRequestId("1");
    
            // Setting up the repository to return null for the non-existent operation request
            _repoMock.Setup(repo => repo.GetByIdAsync(operationRequestId))
                .ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.InactivateAsync(operationRequestId);

            // Assert
            Assert.Null(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationRequestId), Times.Once);
            _repoMock.Verify(repo => repo.Remove(It.IsAny<OperationRequest>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }

    }
}
