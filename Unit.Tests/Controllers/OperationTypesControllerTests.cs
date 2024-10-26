using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.OperationType;
using DDDSample1.Domain.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Controllers
{
    [TestFixture]
    public class OperationTypesControllerTests
    {
        private OperationTypesController _controller;
        private OperationTypeService _oTServiceMock;
        private Mock<OperationTypeMapper> _oTMapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOperationTypeRepository> _oTRepoMock;
        private Mock<ILogger<OperationTypeService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _oTRepoMock = new Mock<IOperationTypeRepository>();
            _oTMapperMock = new Mock<OperationTypeMapper>();
            _loggerMock = new Mock<ILogger<OperationTypeService>>();

            _oTServiceMock = new OperationTypeService(_unitOfWorkMock.Object, _oTRepoMock.Object, _loggerMock.Object, _oTMapperMock.Object);

            _controller = new OperationTypesController(_oTServiceMock);
        }

        [Test]
        public async Task GetAll_ReturnsAllOperationTypes()
        {
            // Arrange
            var operationDomainList = new List<OperationType>
            {
                new (new OperationTypeId("1"), new OperationName("Operation1"), new List<RequiredStaff>{new RequiredStaff("Doctor")}, new List<EstimatedDuration>{new EstimatedDuration("10")}),
                new (new OperationTypeId("2"), new OperationName("Operation2"), new List<RequiredStaff>{new RequiredStaff("Nurse")}, new List<EstimatedDuration>{new EstimatedDuration("20")})
            };
            
            _oTRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationDomainList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsOperationType()
        {
            // Arrange
            var operationDomainList = new List<OperationType>
            {
                new OperationType(new OperationTypeId("1"), new OperationName("Operation1"), new List<RequiredStaff>{new RequiredStaff("Doctor")}, new List<EstimatedDuration>{new EstimatedDuration("10")}),
                new OperationType(new OperationTypeId("2"), new OperationName("Operation2"), new List<RequiredStaff>{new RequiredStaff("Nurse")}, new List<EstimatedDuration>{new EstimatedDuration("20")})
            };
            
            var operationTypeDto = new OperationTypeDto
            {
                Id = "1", OperationName = "Operation1", RequiredStaff = new List<string>{"Doctor"}, EstimatedDuration = new List<string>{"10"}
            };
            
            _oTRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationTypeId>())).ReturnsAsync(operationDomainList[0]);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            Assert.IsNotNull(result);
            result.Value.Should().BeEquivalentTo(operationTypeDto);
        }

        [Test]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _oTRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationTypeId>()))
                .ReturnsAsync((OperationType)null);

            // Act
            var result = await _controller.GetById("invalid-id");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task Create_ValidOperationType_ReturnsCreatedAtAction()
        {
            // Arrange
            var operationTypeDto = new OperationTypeDto
            {
                Id = "1", OperationName = "Operation1", RequiredStaff = new List<string>{"Doctor"}, EstimatedDuration = new List<string>{"10","20","40"}
            };
            
            _oTRepoMock.Setup(repo => repo.AddAsync(It.IsAny<OperationType>())).ReturnsAsync(new OperationType(
                new OperationTypeId("1"), 
                new OperationName("Operation1"), 
                new List<RequiredStaff> { new RequiredStaff("Doctor") }, 
                new List<EstimatedDuration> { new ("10"), new ("20"), new ("40") }));

            // Act
            var result = await _controller.Create(operationTypeDto);

            // Assert
            Assert.IsNotNull(result);
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public async Task Delete_ValidId_ReturnsOk()
        {
            // Arrange
            var operationTypeToRemove = new OperationType(new OperationTypeId("1"), new OperationName("Operation1"), new List<RequiredStaff> { new RequiredStaff("Doctor") }, new List<EstimatedDuration> { new EstimatedDuration("10") });

            _oTRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationTypeId>()))
                .ReturnsAsync(operationTypeToRemove);

            _oTRepoMock.Setup(repo => repo.Remove(It.IsAny<OperationType>()));

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsNotNull(result);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _oTRepoMock.Setup(repo => repo.Remove(It.IsAny<OperationType>()));

            // Act
            var result = await _controller.Delete("invalid-id");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
