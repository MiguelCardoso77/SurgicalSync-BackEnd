using System;
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
        private Mock<OperationRequestId> _mockOperationRequestId;
        private Mock<DeadlineDate> _mockDeadlineDate;
        private Mock<OperationTypeId> _mockOperationTypeId;
        private Mock<LicenseNumber> _mockLicenseNumber;
        private Mock<MedicalRecordNumber> _mockMedicalRecordNumber;

        private Priority _mockPriority;

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

            Setup();
        }

        public void Setup()
        {
            _mockOperationRequestId = new Mock<OperationRequestId>("1");
            _mockDeadlineDate = new Mock<DeadlineDate>(new DateTime(2025, 10, 1));
            _mockOperationTypeId = new Mock<OperationTypeId>("2");
            _mockLicenseNumber = new Mock<LicenseNumber>("D202400001");
            _mockMedicalRecordNumber = new Mock<MedicalRecordNumber>("202411000001");

            _mockPriority = Priority.ElectiveSurgery;

            _mockOperationRequestId.Setup(x => x.AsString()).Returns("1");
            _mockOperationTypeId.Setup(x => x.AsString()).Returns("2");
            _mockLicenseNumber.Setup(x => x.AsString()).Returns("D202400001");
            _mockMedicalRecordNumber.Setup(x => x.AsString()).Returns("202411000001");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOperationRequestDto_WhenOperationRequestExists()
        {
            // Arrange
            var operationRequest = new OperationRequest(
                _mockOperationRequestId.Object,
                _mockPriority,
                _mockDeadlineDate.Object,
                _mockOperationTypeId.Object,
                _mockMedicalRecordNumber.Object,
                _mockLicenseNumber.Object
            );

            var expectedDto = new OperationRequestDto()
            {
                OperationRequestId = "1",
                DeadlineDate = "2025-10-01",
                LicenseNumber = "D202400001",
                Priority = _mockPriority.ToString(),
                OperationTypeId = "2",
                MedicalRecordNumber = "202411000001"
            };

            // Setting up the repository to return the mock operation request
            _repoMock.Setup(repo => repo.GetByIdAsync(_mockOperationRequestId.Object))
                     .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.GetByIdAsync(_mockOperationRequestId.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.OperationRequestId, result.OperationRequestId);
            Assert.Equal(expectedDto.MedicalRecordNumber, result.MedicalRecordNumber);
            Assert.Equal(expectedDto.LicenseNumber, result.LicenseNumber);
            Assert.Equal(expectedDto.OperationTypeId, result.OperationTypeId);
            Assert.Equal(expectedDto.Priority, result.Priority);
            Assert.Equal(expectedDto.DeadlineDate, result.DeadlineDate);
            _repoMock.Verify(repo => repo.GetByIdAsync(_mockOperationRequestId.Object), Times.Once);
        }
        
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenOperationRequestDoesNotExist()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetByIdAsync(_mockOperationRequestId.Object)).ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.GetByIdAsync(_mockOperationRequestId.Object);

            // Assert
            Assert.Null(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(_mockOperationRequestId.Object), Times.Once);
        }
    }
}
