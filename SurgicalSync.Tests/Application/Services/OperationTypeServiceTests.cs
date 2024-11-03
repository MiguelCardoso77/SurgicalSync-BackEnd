using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services
{
    [TestFixture]
    public class OperationTypeServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOperationTypeRepository> _repoMock;
        private Mock<ILogger<OperationTypeService>> _loggerMock;
        private OperationTypeMapper _mapper;
        private OperationTypeService _operationTypeService; 
        
        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IOperationTypeRepository>();
            _loggerMock = new Mock<ILogger<OperationTypeService>>();
            _mapper = new OperationTypeMapper();
            
            _operationTypeService = new OperationTypeService(
                _unitOfWorkMock.Object,
                _repoMock.Object,
                _loggerMock.Object,
                _mapper);
        }
        
        [Test]
        public async Task GetByIdAsync_ReturnsOperationTypeDto_WhenOperationTypeExists()
        {
            // Arrange
            var operationTypeId = new OperationTypeId("1");
            var requiredStaff = new RequiredStaff("Doctor");
            var estimatedDuration = new EstimatedDuration("120");
            var operationName = new OperationName("Surgery");

            var operationType = new OperationType(
                operationTypeId,
                operationName,
                requiredStaff,
                estimatedDuration
            );

            var expectedDto = new OperationTypeDto
            {
                Id = operationTypeId.AsString(),
                OperationName = operationName.ToString(),
                RequiredStaff = requiredStaff.ToString(),
                EstimatedDuration = estimatedDuration.ToString()

            };

            _repoMock.Setup(repo => repo.GetByIdAsync(operationTypeId)).ReturnsAsync(operationType);

            // Act
            var result = await _operationTypeService.GetByIdAsync(operationTypeId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto.Id, result.Id);
            Assert.AreEqual(expectedDto.OperationName, result.OperationName);
            CollectionAssert.AreEqual(expectedDto.RequiredStaff, result.RequiredStaff);
            CollectionAssert.AreEqual(expectedDto.EstimatedDuration, result.EstimatedDuration);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationTypeId), Times.Once);
        }
        
        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenOperationTypeDoesNotExist()
        {
            // Arrange
            var operationTypeId = new OperationTypeId("1");
            _repoMock.Setup(repo => repo.GetByIdAsync(operationTypeId)).ReturnsAsync((OperationType)null);

            // Act
            var result = await _operationTypeService.GetByIdAsync(operationTypeId);

            // Assert
            Assert.IsNull(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(operationTypeId), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfOperationTypeDto_WhenRequestsExist()
        {
            // Arrange
            var operationTypeId1 = new OperationTypeId("1");
            var operationTypeId2 = new OperationTypeId("2");

            var operationName1 = new OperationName("Surgery1");
            var operationName2 = new OperationName("Surgery2");

            var requiredStaffList1 = new RequiredStaff("Doctor");
            var requiredStaffList2 = new RequiredStaff("Nurse");

            var estimatedDurationList1 = new EstimatedDuration("20");
            var estimatedDurationList2 = new EstimatedDuration("30");

            var operationType1 = new OperationType(operationTypeId1, operationName1, requiredStaffList1, estimatedDurationList1);
            var operationType2 = new OperationType(operationTypeId2, operationName2, requiredStaffList2, estimatedDurationList2);

            var operationTypes = new List<OperationType> { operationType1, operationType2 };
            
            var expectedDtos = operationTypes.Select(op => new OperationTypeDto
            {
                Id = op.Id.AsString(),
                OperationName = op.Name.ToString(),
                RequiredStaff = op.RequiredStaff.ToString(),
                EstimatedDuration = op.EstimatedDuration.ToString()
            }).ToList();

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDtos.Count, result.Count);
            
            for (int i = 0; i < expectedDtos.Count; i++)
            {
                Assert.AreEqual(expectedDtos[i].Id, result[i].Id);
                Assert.AreEqual(expectedDtos[i].OperationName, result[i].OperationName);
                CollectionAssert.AreEqual(expectedDtos[i].RequiredStaff, result[i].RequiredStaff);
                CollectionAssert.AreEqual(expectedDtos[i].EstimatedDuration, result[i].EstimatedDuration);
            }

            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
        
        [Test]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoOperationTypesExist()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OperationType>());

            // Act
            var result = await _operationTypeService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Expected an empty list when no OperationTypes exist.");
            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
        
        [Test]
        public async Task AddAsync_CreatesNewOperationType_WhenValidDtoIsProvided()
        {
            // Arrange
            var operationTypeId = "1";
            var dto = new OperationTypeDto
            {
                Id = operationTypeId,
                OperationName = "Test Surgery",
                RequiredStaff = "Doctor",
                EstimatedDuration = "30",
            };

            // Act
            var result = await _operationTypeService.AddAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            _repoMock.Verify(repo => repo.AddAsync(It.IsAny<OperationType>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        [Test]
        public async Task UpdateAsync_UpdatesExistingOperationType_WhenValidDtoIsProvided()
        {
            // Arrange
            var operationTypeId = "1";
            var dto = new OperationTypeDto
            {
                Id = operationTypeId,
                OperationName = "Test Surgery",
                RequiredStaff = "Doctor",
                EstimatedDuration = "30"
            };

            var operationType = new OperationType(
                new OperationTypeId(operationTypeId),
                new OperationName("Old Surgery"),
                new RequiredStaff("Nurse"),
                new EstimatedDuration("60")
            );

            _repoMock.Setup(repo => repo.GetByIdAsync(new OperationTypeId(operationTypeId))).ReturnsAsync(operationType);

            // Act
            var result = await _operationTypeService.UpdateAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dto.OperationName, result.OperationName);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        [Test]
        public async Task UpdateAsync_ReturnsNull_WhenOperationTypeDoesNotExist()
        {
            // Arrange
            var dto = new OperationTypeDto { Id = "1" };
            _repoMock.Setup(repo => repo.GetByIdAsync(new OperationTypeId(dto.Id))).ReturnsAsync((OperationType)null);
            
            // Act
            var result = await _operationTypeService.UpdateAsync(dto);
            
            // Assert
            Assert.IsNull(result);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }
        
        [Test]
        public async Task GetAllByOperationName_ReturnsListOfOperationTypeDto_WhenOperationTypesExist()
        {
            // Arrange
            var operationName = new OperationName("Surgery");

            var oT = new OperationType(
                new OperationTypeId("1"),
                operationName,
                new RequiredStaff("Doctor"),
                new EstimatedDuration("120")
            );
            
            var oT2 = new OperationType(
                new OperationTypeId("2"),
                new OperationName("Not Surg"),
                new RequiredStaff("Nurse"),
                new EstimatedDuration("60")
            );
            
            var operationTypes = new List<OperationType> { oT, oT2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.GetAllByName("Surgery");
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(oT.Id.AsString(), result[0].Id);
        }
        
        [Test]
        public async Task GetAllByOperationName_ReturnsEmptyList_WhenNoOperationTypesExist()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OperationType>());

            // Act
            var result = await _operationTypeService.GetAllByName("Surgery");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Expected an empty list when no OperationTypes exist.");
        }
        
        [Test]
        public async Task GetAllByStatus_ReturnsListOfOperationTypeDto_WhenOperationTypesExist()
        {
            var oT = new OperationType(
                new OperationTypeId("1"),
                new OperationName("Lot of Surgery"),
                new RequiredStaff("Doctor"),
                new EstimatedDuration("120")
            );
            
            var oT2 = new OperationType(
                new OperationTypeId("2"),
                new OperationName("Not Surg"),
                new RequiredStaff("Nurse"),
                new EstimatedDuration("60")
            );
            
            var operationTypes = new List<OperationType> { oT, oT2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.GetAllByStatus(true);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
        
        [Test]
        public async Task GetAllByStatus_ReturnsEmptyList_WhenNoOperationTypesExist()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OperationType>());

            // Act
            var result = await _operationTypeService.GetAllByStatus(true);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Expected an empty list when no OperationTypes exist.");
        }
        
        [Test]
        public async Task GetAllBySpecialization_ReturnsListOfOperationTypeDto_WhenOperationTypesExist()
        {
            var oT = new OperationType(
                new OperationTypeId("1"),
                new OperationName("Lot of Surgery"),
                new RequiredStaff("Doctor"),
               new EstimatedDuration("120")
            );
            
            var oT2 = new OperationType(
                new OperationTypeId("2"),
                new OperationName("Not Surg"),
                new RequiredStaff("Nurse"),
               new EstimatedDuration("60")
            );
            
            var operationTypes = new List<OperationType> { oT, oT2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.GetAllBySpecialization("Arthroscopy");
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public async Task GetAllBySpecialization_ReturnsEmptyList_WhenNoOperationTypesExist()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OperationType>());

            // Act
            var result = await _operationTypeService.GetAllBySpecialization("Arthroscopy");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Expected an empty list when no OperationTypes exist.");
        }
    }
}