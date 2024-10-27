using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Staffs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers
{
    [TestFixture]
    public class OperationRequestsControllerTests
    {
        private OperationRequestsController _controller;
        private OperationRequestService _operationRequestService;
        private PatientNameMicroService _patientNameMicroService;
        private Mock<OperationRequestMapper> _mockOperationRequestMapper;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOperationRequestRepository> _oRRepositoryMock;
        private Mock<ILogger<OperationRequestService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _oRRepositoryMock = new Mock<IOperationRequestRepository>();
            _mockOperationRequestMapper = new Mock<OperationRequestMapper>();
            _loggerMock = new Mock<ILogger<OperationRequestService>>();

            _operationRequestService = new OperationRequestService(
                _unitOfWorkMock.Object, 
                _oRRepositoryMock.Object, 
                _loggerMock.Object,
                _mockOperationRequestMapper.Object,
                _patientNameMicroService
                );

            _controller = new OperationRequestsController(_operationRequestService);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsOperationRequest()
        {
            var operationDomainList = new List<OperationRequest>()
            {
                new OperationRequest(new OperationRequestId("1"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01, 01)),
                    new OperationTypeId("5"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001")),

                new OperationRequest(new OperationRequestId("2"), Priority.ElectiveSurgery, new DeadlineDate(new DateTime(2025, 11, 10)),
                    new OperationTypeId("1"), new MedicalRecordNumber("202409000002"), new StaffId("N202400001")),

                new OperationRequest(new OperationRequestId("3"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2024, 12, 15)),
                    new OperationTypeId("2"), new MedicalRecordNumber("202409000003"), new StaffId("N202400002")),

                new OperationRequest(new OperationRequestId("4"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01, 20)),
                    new OperationTypeId("2"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001"))
            };

            var operationRequestDto = new OperationRequestDto()
            {
                OperationRequestId = "1",
                Priority = "UrgentSurgery",
                DeadlineDate = "2025-01-01",
                OperationTypeId = "5",
                MedicalRecordNumber = "202409000001",
                StaffId = "N202400001"
            };
            
            _oRRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestId>())).ReturnsAsync(operationDomainList[0]);
            
            var result = await _controller.GetById("1");
            
            Assert.IsNotNull(result);
            result.Value.Should().BeEquivalentTo(operationRequestDto);
        }

        [Test]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            _oRRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestId>())).ReturnsAsync((OperationRequest)null);
            
            var result = await _controller.GetById("invalid-id");
            
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
        
        [Test]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var dto = new OperationRequestDto
            {
                OperationRequestId = "5",
                Priority = "UrgentSurgery",
                DeadlineDate = "2025-01-01",
                OperationTypeId = "2",
                MedicalRecordNumber = "202409000001",
                StaffId = "N202400001"
            };

            var result = await _controller.Update("differentId", dto);

            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }
        
        [Test]
        public async Task Create_ReturnsCreatedResponse_WhenOperationRequestIsValid()
        {
            var dto = new OperationRequestDto
            {
                OperationRequestId = "1",
                Priority = "UrgentSurgery",
                DeadlineDate = "2025-01-07",
                OperationTypeId = "5",
                MedicalRecordNumber = "202409000001",
                StaffId = "N202400001"
            };

            var request = new OperationRequest(new OperationRequestId("1"), Priority.UrgentSurgery,
                new DeadlineDate(new DateTime(2025, 01, 07)),
                new OperationTypeId("5"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001"));
                
            _oRRepositoryMock.Setup(service => service.AddAsync(It.IsAny<OperationRequest>()))
                .ReturnsAsync(request);

            var result = await _controller.Create(dto);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.AreEqual(201, createdResult.StatusCode);
        }

        [Test]

        public async Task GetAllOperationRequests_ReturnsAllOperationRequests()
        {
            var operationDomainList = new List<OperationRequest>()
            {
                new OperationRequest(new OperationRequestId("1"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01, 07)),
                    new OperationTypeId("5"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001")),

                new OperationRequest(new OperationRequestId("2"), Priority.ElectiveSurgery, new DeadlineDate(new DateTime(2025, 11, 10)),
                    new OperationTypeId("1"), new MedicalRecordNumber("202409000002"), new StaffId("N202400001")),

                new OperationRequest(new OperationRequestId("3"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2024, 12, 15)),
                    new OperationTypeId("2"), new MedicalRecordNumber("202409000003"), new StaffId("N202400002")),

                new OperationRequest(new OperationRequestId("4"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01, 20)),
                    new OperationTypeId("2"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001"))
            };
            
            _oRRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationDomainList);
            
            var result = await _controller.GetAll();
            
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedOperationRequests = okResult.Value as IEnumerable<OperationRequestDto>;
            Assert.IsNotNull(returnedOperationRequests);
            Assert.AreEqual(operationDomainList.Count, returnedOperationRequests.Count());
            
            var operationRequestsArray = returnedOperationRequests.ToArray();
            for (int i = 0; i < operationDomainList.Count; i++)
            {
                Assert.AreEqual(operationDomainList[i].Id.AsString(), operationRequestsArray[i].OperationRequestId);
                Assert.AreEqual(operationDomainList[i].Priority.ToString(), operationRequestsArray[i].Priority);
                Assert.AreEqual(operationDomainList[i].DeadlineDate.ToString(), operationRequestsArray[i].DeadlineDate);
                Assert.AreEqual(operationDomainList[i].OperationTypeId.AsString(), operationRequestsArray[i].OperationTypeId);
                Assert.AreEqual(operationDomainList[i].MedicalRecordNumber.AsString(), operationRequestsArray[i].MedicalRecordNumber);
                Assert.AreEqual(operationDomainList[i].StaffId.AsString(), operationRequestsArray[i].StaffId);
            }
        }
        [Test]
        public async Task Delete_ReturnsOk_WhenOperationRequestIsSuccessfullyDeleted()
        {
            var request = new OperationRequest(new OperationRequestId("1"), Priority.UrgentSurgery,
                new DeadlineDate(new DateTime(2025, 01, 07)),
                new OperationTypeId("5"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001"));

            _oRRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestId>()))
                .ReturnsAsync(request);
            
            _oRRepositoryMock.Setup(repo => repo.Remove(It.IsAny<OperationRequest>()));

            var result = await _controller.Delete("1");
            
            Assert.IsNotNull(result);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Delete_ReturnsNotFound_WhenOperationRequestIsNotFound()
        {
            _oRRepositoryMock.Setup(repo => repo.Remove(It.IsAny<OperationRequest>()));
            
            var result = await _controller.Delete("invalid-id");
            
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
        
    }
}