using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.SurgeryRooms;
using DDDNetCore.Domain.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers
{
    public class PatientsControllerTests
    {
        private PatientsController _controller;
        private PatientService _service;
        private PatientMapper _patientMapper;
        private DeletePatientMicroService _deletePatientMicroService;
        private UserEmailMicroService _userEmailMicroService;
        private UserService _userService;
        private UserMapper _userMapper;
        private PatientAppointmentHistoryMicroService _patientAppointmentHistoryMicroService;

        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPatientRepository> _mockIPatientRepository;
        private Mock<IUserRepository> _mockIUserRepository;
        private Mock<ILogger<DeletePatientMicroService>> _loggerMock;
        private Mock<ILogger<PatientService>> _loggerMockPatient;
        private Mock<IOperationRequestRepository> _mockIOperationRequestRepository;
        private Mock<IAppointmentsRepository> _mockIAppointmentsRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockIPatientRepository = new Mock<IPatientRepository>();
            _loggerMock = new Mock<ILogger<DeletePatientMicroService>>();
            _mockIUserRepository = new Mock<IUserRepository>();
            _mockIAppointmentsRepository = new Mock<IAppointmentsRepository>();
            _mockIOperationRequestRepository = new Mock<IOperationRequestRepository>();
            
            _loggerMockPatient = new Mock<ILogger<PatientService>>();

            _patientMapper = new PatientMapper();
            _userMapper = new UserMapper();

            _userEmailMicroService = new UserEmailMicroService(
                _mockIUserRepository.Object
            );

            _userService = new UserService(
                _mockUnitOfWork.Object,
                _mockIUserRepository.Object,
                _userMapper
            );

            _deletePatientMicroService = new DeletePatientMicroService(
                _mockUnitOfWork.Object,
                _mockIPatientRepository.Object,
                _patientMapper,
                _userService,
                _loggerMock.Object
            );

            _patientAppointmentHistoryMicroService = new PatientAppointmentHistoryMicroService(
                _mockIOperationRequestRepository.Object,
                _mockIAppointmentsRepository.Object
            );

            _service = new PatientService(
                _mockUnitOfWork.Object,
                _mockIPatientRepository.Object,
                _patientMapper,
                _deletePatientMicroService,
                _loggerMockPatient.Object,
                _patientAppointmentHistoryMicroService
            );

            _controller = new PatientsController(
                _service
            );
            
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "ICcTTh51IzOiBKmftT1SnrBH5d42";
            
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext
            };
        }

        [Test]
        public async Task GetAll_ReturnsAllPatientProfiles()
        {
            // Arrange
            var patientList = new List<Patient>
            {
                new(new PatientName("Diana"), new BirthDate("30 de Junho de 2004"), new Gender("Feminino"),
                    new MedicalRecordNumber("202409000001"), new PhoneNumber("938413938"),
                    new EmergencyContact("933264402"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1221195@isep.ipp.pt")
                ),

                new(new PatientName("Miguel"), new BirthDate("4 de Julho de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000002"), new PhoneNumber("938745060"),
                    new EmergencyContact("930923458"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1220772@isep.ipp.pt")
                ),

                new(new PatientName("Diogo"), new BirthDate("8 de Janeiro de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000003"), new PhoneNumber("938745065"),
                    new EmergencyContact("930923459"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1220812@isep.ipp.pt")
                )
            };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patientList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsPatient()
        {
            // Arrange
            var patientList = new List<Patient>
            {
                new(new PatientName("Diana"), new BirthDate("30 de Junho de 2004"), new Gender("Feminino"),
                    new MedicalRecordNumber("202409000001"), new PhoneNumber("938413938"),
                    new EmergencyContact("933264402"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1221194@isep.ipp.pt")
                ),

                new(new PatientName("Miguel"), new BirthDate("4 de Julho de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000002"), new PhoneNumber("938745060"),
                    new EmergencyContact("930923458"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1220772@isep.ipp.pt")
                ),

                new(new PatientName("Diogo"), new BirthDate("8 de Janeiro de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000003"), new PhoneNumber("938745065"),
                    new EmergencyContact("930923459"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1220812@isep.ipp.pt")
                )
            };

            var dto = new PatientDto
            {
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Gender = "Feminino",
                MedicalRecordNumber = "202409000001",
                PhoneNumber = "938413938",
                EmergencyContact = "933264402",
                AppointmentHistory = "02/04/2024",
                Email = "1221194@isep.ipp.pt"
            };

            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<MedicalRecordNumber>()))
                .ReturnsAsync(patientList[0]);

            // Act
            var result = await _controller.GetById("202409000001");

            // Assert
            Assert.IsNotNull(result);
            result.Value.Should().BeEquivalentTo(dto);
        }

        [Test]
        public async Task GetById_InValidId_ReturnsNotFound()
        {
            // Arrange
            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<MedicalRecordNumber>()))
                .ReturnsAsync((Patient)null);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task Create_ValidPatientProfile_ReturnsCreatedAtAction()
        {
            var user = new User(
                new UserId("1"),
                new Username("Diogo"),
                new UserEmail("1220812.isep.ipp.pt"),
                UserRole.Technician
            );
            
            var users = new List<User> { user };
            _mockIUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var patientList = new List<Patient>
            {
                new(new PatientName("Miguel"), new BirthDate("4 de Julho de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000002"), new PhoneNumber("938745060"),
                    new EmergencyContact("930923458"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1220772@isep.ipp.pt")
                ),

                new(new PatientName("Diogo"), new BirthDate("8 de Janeiro de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000003"), new PhoneNumber("938745065"),
                    new EmergencyContact("930923459"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1220812@isep.ipp.pt")
                )
            };
            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patientList);

            // Arrange
            var dto = new PatientDto
            {
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Gender = "Feminino",
                MedicalRecordNumber = "202409000001",
                PhoneNumber = "938413938",
                EmergencyContact = "933264402",
                AppointmentHistory = "02/04/2024",
                Email = "1221194@isep.ipp.pt"
            };

            _mockIPatientRepository.Setup(repo => repo.AddAsync(It.IsAny<Patient>())).ReturnsAsync(
                new Patient(
                    new PatientName("Diana"),
                    new BirthDate("30 de Junho de 2004"),
                    new Gender("Feminino"),
                    new MedicalRecordNumber("202409000001"),
                    new PhoneNumber("938413938"),
                    new EmergencyContact("933264402"),
                    new AppointmentHistory("02/04/2024"),
                    new UserEmail("1221194@isep.ipp.pt")
                ));

            // Act
            var result = await _controller.Create(dto);

            // Assert
            Assert.IsNotNull(result);
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }
        
        [Test]
        public async Task Update_ValidPatientProfile_ReturnsCreatedAtAction()
        {
            // Arrange
            var medicalRecordNumber = "1";
            
            var dto = new PatientDto
            {
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Gender = "Feminino",
                MedicalRecordNumber = medicalRecordNumber,
                PhoneNumber = "938413938",
                EmergencyContact = "933264402",
                AppointmentHistory = "02/04/2024",
                Email = "1221194@isep.ipp.pt"
            };

            var patient = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber(medicalRecordNumber),
                new PhoneNumber("934260705"),
                new EmergencyContact("938413938"),
                new AppointmentHistory("02/04/2024"),
                new UserEmail("1220917@isep.ipp.pt")
            );

            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(new MedicalRecordNumber(medicalRecordNumber)))
                .ReturnsAsync(patient);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            Assert.IsNotNull(result);
        }
        
        [Test]
        public async Task Update_InValidPatientProfile_ReturnsBadRequest()
        {
            var dto = new PatientDto
            {
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Gender = "Feminino",
                MedicalRecordNumber = "202410000001",
                PhoneNumber = "938413938",
                EmergencyContact = "933264402",
                AppointmentHistory = "02/04/2024",
                Email = "1221194@isep.ipp.pt"
            };
            // Arrange
            _mockIPatientRepository.Setup(repo => repo.Remove(It.IsAny<Patient>()));

            // Act
            var result = await _controller.Update("invalid-id", dto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }
        
        [Test]
        public async Task Delete_ValidId_ReturnsOk()
        {
            // Arrange
            var patientToRemove = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000001"),
                new PhoneNumber("934260705"),
                new EmergencyContact("938413938"),
                new AppointmentHistory("02/04/2024"),
                new UserEmail("1220917@isep.ipp.pt")
            );
            
            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<MedicalRecordNumber>()))
                .ReturnsAsync(patientToRemove);

            _mockIPatientRepository.Setup(repo => repo.Remove(It.IsAny<Patient>()));

            // Act
            var result = await _controller.Delete("202410000001");

            // Assert
            Assert.IsNotNull(result);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        

        [Test]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockIPatientRepository.Setup(repo => repo.Remove(It.IsAny<Patient>()));

            // Act
            var result = await _controller.Delete("invalid-id");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task TestGetAppointmentHistory()
        {
            // Arrange
                var patientEmail = new UserEmail("patient@example.com");
                var medicalRecordNumber = new MedicalRecordNumber("202411000002");
            
                // Setup appointments similar to existing test
                var appointmentId1 = new AppointmentId("1");
                var appointmentId2 = new AppointmentId("2");
                var status = Status.Scheduled;
                var date = new Date(new DateTime(2025, 10, 1));
                var date1 = new Date(new DateTime(2025, 10, 2));
                var time = new Time(100);
                var operationRequestId1 = new OperationRequestId("1");
                var operationRequestId2 = new OperationRequestId("2");
                var roomNumber = new RoomNumber("1");
                var requiredStaff = new RequiredStaff("2 doctor");
            
                var operationRequest1 = new OperationRequest(
                    operationRequestId1,
                    Priority.ElectiveSurgery,
                    new DeadlineDate(new DateTime(2025, 10, 1)),
                    new OperationTypeId("1"),
                    medicalRecordNumber,
                    new StaffId("D202400001")
                );
            
                var operationRequest2 = new OperationRequest(
                    operationRequestId2,
                    Priority.UrgentSurgery,
                    new DeadlineDate(new DateTime(2025, 10, 2)),
                    new OperationTypeId("2"),
                    medicalRecordNumber,
                    new StaffId("D202400002")
                );
            
                var appointment1 = new Appointment(
                    appointmentId1,
                    status,
                    date,
                    time,
                    roomNumber,
                    operationRequestId1,
                    requiredStaff
                );
            
                var appointment2 = new Appointment(
                    appointmentId2,
                    status,
                    date1,
                    time,
                    roomNumber,
                    operationRequestId2,
                    requiredStaff
                );
            
                var appointments = new List<Appointment> { appointment1, appointment2 };
            
                var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };
            
                // Mock repository GetByIdAsync
                var patient = new Patient(
                    new PatientName("Test Patient"),
                    new BirthDate("2000-01-01"),
                    new Gender("Male"),
                    medicalRecordNumber,
                    new PhoneNumber("123456789"),
                    new EmergencyContact("987654321"),
                    new AppointmentHistory(""),
                    patientEmail
                );
            
                _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(medicalRecordNumber))
                    .ReturnsAsync(patient);
                _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Patient> { patient });
                _mockIAppointmentsRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);
                _mockIOperationRequestRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

                var result = await _controller.AppointmentHistory(patientEmail.ToString());

                // Assert
                Assert.IsNotNull(result, "Result should not be null");

                var okResult = result.Result as OkObjectResult;
                Assert.IsNotNull(okResult, "Expected OkObjectResult");

                var appointmentHistory = okResult.Value as AppointmentHistory;
                Assert.IsNotNull(appointmentHistory, "Expected AppointmentHistory");

                var expectedHistoryString = $"{date.DateTime.ToString("yyyy/MM/dd")}, {time.ToString()}, {status.ToString()}; " +
                                            $"{date1.DateTime.ToString("yyyy/MM/dd")}, {time.ToString()}, {status.ToString()}";

                Assert.AreEqual(expectedHistoryString, appointmentHistory.ToString(), "Appointment history string does not match expected format");
        }
    }
}