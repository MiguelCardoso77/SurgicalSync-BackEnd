using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Specializations;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers
{
    [TestFixture]
    public class SpecializationControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ISpecializationRepository> _oRRepositoryMock;
        private Mock<SpecializationMapper> _mockSpecializationMapper;
        private Mock<ILogger<SpecializationService>> _loggerMock;
        private SpecializationService _specializationService;
        private SpecializationController _controller;

        [SetUp]
        public void Setup()
        {
            // Mock das dependências
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _oRRepositoryMock = new Mock<ISpecializationRepository>();
            _mockSpecializationMapper = new Mock<SpecializationMapper>();
            _loggerMock = new Mock<ILogger<SpecializationService>>();

            // Serviço de Especializações
            _specializationService = new SpecializationService(
                _unitOfWorkMock.Object,
                _oRRepositoryMock.Object,
                _loggerMock.Object,
                _mockSpecializationMapper.Object
            );

            // Controlador
            _controller = new SpecializationController(_specializationService);
        }

 
        [Test]
        public async Task GetAll_WithoutDesignation_ReturnsAllSpecializations()
        {
            // Arrange
            var specializationList = new List<SpecializationDto>
            {
                new SpecializationDto { code = "1", designation = "Cardiology", description = "Description" },
                new SpecializationDto { code = "2", designation = "Orthopedist", description = "Description2" }
            };

            _oRRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(specializationList.Select(s => new Specialization(new SpecializationCode(s.code), new SpecializationDesignation(s.designation), new SpecializationDescription(s.description))).ToList());
    
            var httpContextMock = new Mock<HttpContext>();
            var headers = new HeaderDictionary
            {
                { "Authorization", "ICcTTh51IzOiBKmftT1SnrBH5d42" } // Token válido
            };
            httpContextMock.SetupGet(c => c.Request.Headers).Returns(headers);
    
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedSpecializations = okResult.Value as IEnumerable<SpecializationDto>;
            Assert.IsNotNull(returnedSpecializations);
            Assert.AreEqual(specializationList.Count, returnedSpecializations.Count());
        }

        [Test]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            var specializationDomainList = new List<Specialization>()
            {
                new Specialization(new SpecializationCode("1"), new SpecializationDesignation("Updated Cardiology"),
                    new SpecializationDescription("Updated description"))
            };
            
            var updatedDto = new SpecializationDto
            {
                code = "1",
                designation = "Updated Cardiology",
                description = "Updated description"
            };

            _oRRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<SpecializationCode>())).ReturnsAsync(specializationDomainList[0]);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            Assert.IsNotNull(result);
            result.Value.Should().BeEquivalentTo(updatedDto);        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var updatedDto = new SpecializationDto
            {
                code = "1",
                designation = "Updated Cardiology",
                description = "Updated description"
            };

            // Mock do contexto HTTP e cabeçalhos
            var httpContextMock = new Mock<HttpContext>();
            var headers = new HeaderDictionary
            {
                { "Authorization", "ICcTTh51IzOiBKmftT1SnrBH5d42" } // Token válido
            };
            httpContextMock.SetupGet(c => c.Request.Headers).Returns(headers);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var result = await _controller.UpdateAsync("invalid", updatedDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }      
[Test]
public async Task Delete_ValidId_ReturnsOk()
{
    var specialization = new Specialization(new SpecializationCode("1"), new SpecializationDesignation("Cardiology"), new SpecializationDescription("Description"));

    _oRRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<SpecializationCode>())).ReturnsAsync(specialization);
    //_unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

    var result = await _controller.DeleteAsync("1");

    var okResult = result.Result as OkObjectResult;
    Assert.IsNotNull(okResult);
    Assert.AreEqual(200, okResult.StatusCode);

    var returnedSpecialization = okResult.Value as SpecializationDto;
    Assert.IsNotNull(returnedSpecialization);
    returnedSpecialization.Should().BeEquivalentTo(_mockSpecializationMapper.Object.ToDto(specialization));
}


        [Test]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            _oRRepositoryMock.Setup(s => s.Remove(It.IsAny<Specialization>()));

            var result = await _controller.DeleteAsync("invalid-id");

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
