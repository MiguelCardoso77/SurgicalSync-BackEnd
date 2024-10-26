using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Services
{
    [TestFixture]
    public class PatientNameMicroServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPatientRepository> _patientRepositoryMock;
        private Mock<IOperationRequestRepository> _operationRequestRepositoryMock;
        private PatientNameMicroService _patientNameMicroService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _operationRequestRepositoryMock = new Mock<IOperationRequestRepository>();

            _patientNameMicroService = new PatientNameMicroService(
                _unitOfWorkMock.Object,
                _patientRepositoryMock.Object,
                _operationRequestRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllOperationRequestsByPatientName_ReturnsOperationRequests_WhenPatientExists()
        {
            // Arrange
            var patientName = "John Doe";
            var medicalRecordNumber = new MedicalRecordNumber("202411000001");
            var birthDate = new BirthDate(DateTime.Now.AddYears(-10).ToString("yyyy/MM/dd"));
            var gender = new Gender("Male");
            var phoneNumber = new PhoneNumber("123456789");
            var userEmail = new UserEmail("johndoe@example.com");

            var patient = new Patient(
                new PatientName(patientName),
                birthDate,
                gender,
                medicalRecordNumber,
                phoneNumber,
                new List<MedicalConditions>(),
                new EmergencyContact("123456789"),
                new List<AppointmentHistory>(),
                userEmail
            );

            var patients = new List<Patient> { patient };
            
            var operationRequestId = new OperationRequestId("1");
            var priority = Priority.ElectiveSurgery;
            var deadlineDate = new DeadlineDate(new DateTime(2025, 10, 1));
            var operationTypeId = new OperationTypeId("2");
            var licenseNumber = new StaffId("D202400001");

            var operationRequest = new OperationRequest(
                operationRequestId,
                priority,
                deadlineDate,
                operationTypeId,
                medicalRecordNumber,
                licenseNumber
            );

            var operationRequests = new List<OperationRequest> { operationRequest };

            var expectedDtoList = new List<OperationRequestDto>
            {
                new OperationRequestDto
                {
                    OperationRequestId = "1",
                    MedicalRecordNumber = "202411000001",
                    Priority = priority.ToString(),
                    DeadlineDate = deadlineDate.Date.ToString("yyyy-MM-dd"),
                    OperationTypeId = "2",
                    StaffId = "D202400001"
                }
            };

            _patientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);
            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _patientNameMicroService.GetAllOperationRequestsByPatientName(patientName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDtoList.Count, result.Count);
            
            for (int i = 0; i < expectedDtoList.Count; i++)
            {
                Assert.AreEqual(expectedDtoList[i].OperationRequestId, result[i].OperationRequestId);
                Assert.AreEqual(expectedDtoList[i].MedicalRecordNumber, result[i].MedicalRecordNumber);
                Assert.AreEqual(expectedDtoList[i].Priority, result[i].Priority);
                Assert.AreEqual(expectedDtoList[i].DeadlineDate, result[i].DeadlineDate);
                Assert.AreEqual(expectedDtoList[i].OperationTypeId, result[i].OperationTypeId);
                Assert.AreEqual(expectedDtoList[i].StaffId, result[i].StaffId);
            }

            _patientRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _operationRequestRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllOperationRequestsByPatientName_ReturnsEmptyList_WhenPatientDoesNotExist()
        {
            // Arrange
            var patientName = "Nonexistent Patient";

            _patientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Patient>());
            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OperationRequest>());

            // Act
            var result = await _patientNameMicroService.GetAllOperationRequestsByPatientName(patientName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);

            _patientRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _operationRequestRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Never);
        }

        [Test]
        public async Task GetAllOperationRequestsByPatientName_ReturnsEmptyList_WhenPatientHasNoOperationRequests()
        {
            // Arrange
            var patientName = "John Doe";
            var medicalRecordNumber = new MedicalRecordNumber("202411000002");
            var birthDate = new BirthDate(DateTime.Now.AddYears(-10).ToString("yyyy/MM/dd"));
            var gender = new Gender("Male");
            var phoneNumber = new PhoneNumber("123456789");
            var userEmail = new UserEmail("johndoe@example.com");

            var patient = new Patient(
                new PatientName(patientName),
                birthDate,
                gender,
                medicalRecordNumber,
                phoneNumber,
                new List<MedicalConditions>(),
                new EmergencyContact("123456789"),
                new List<AppointmentHistory>(),
                userEmail
            );

            var patients = new List<Patient> { patient };

            _patientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);
            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OperationRequest>());

            // Act
            var result = await _patientNameMicroService.GetAllOperationRequestsByPatientName(patientName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);

            _patientRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _operationRequestRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
