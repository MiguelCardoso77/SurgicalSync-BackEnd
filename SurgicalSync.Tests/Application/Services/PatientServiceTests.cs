using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services
{
    [TestFixture]
    public class PatientServiceTests
    {
        private PatientService _service;
        private PatientMapper _patientMapper;
        private DeletePatientMicroService _deletePatientMicroService;
        private UserEmailMicroService _userEmailMicroService;
        private UserService _userService;
        private UserMapper _userMapper;

        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPatientRepository> _mockIPatientRepository;
        private Mock<IUserRepository> _mockIUserRepository;
        private Mock<ILogger<DeletePatientMicroService>> _loggerMock;
        private Mock<ILogger<PatientService>> _loggerMockPatient;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockIPatientRepository = new Mock<IPatientRepository>();
            _loggerMock = new Mock<ILogger<DeletePatientMicroService>>();
            _loggerMockPatient = new Mock<ILogger<PatientService>>();
            _mockIUserRepository = new Mock<IUserRepository>();

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

            _service = new PatientService(
                _mockUnitOfWork.Object,
                _mockIPatientRepository.Object,
                _patientMapper,
                _userEmailMicroService,
                _deletePatientMicroService,
                _loggerMockPatient.Object
            );
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfPatientDto_WhenRequestsExist()
        {
            var patient1 = new Patient
            (
                new PatientName("Diana"),
                new BirthDate("30 de Junho de 2004"),
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            var expectedDtos = patients.Select(pt => new PatientDto
            {
                PatientName = pt.PatientName.ToString(),
                BirthDate = pt.BirthDate.ToString(),
                Gender = pt.Gender.ToString(),
                MedicalRecordNumber = pt.Id.AsString(),
                PhoneNumber = pt.PhoneNumber.ToString(),
                MedicalConditions = pt.MedicalConditions.ToString(),
                EmergencyContact = pt.EmergencyContact.ToString(),
                AppointmentHistory = pt.AppointmentHistory.ToString(),
                Email = pt.UserEmail.ToString()
            }).ToList();

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            //Act
            var result = await _service.GetAllAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDtos.Count, result.Count);

            for (int i = 0; i < expectedDtos.Count; i++)
            {
                Assert.AreEqual(expectedDtos[i].MedicalRecordNumber, result[i].MedicalRecordNumber);
                Assert.AreEqual(expectedDtos[i].PatientName, result[i].PatientName);
                Assert.AreEqual(expectedDtos[i].BirthDate, result[i].BirthDate);
                Assert.AreEqual(expectedDtos[i].Gender, result[i].Gender);
                Assert.AreEqual(expectedDtos[i].PhoneNumber, result[i].PhoneNumber);
                Assert.AreEqual(expectedDtos[i].EmergencyContact, result[i].EmergencyContact);
                Assert.AreEqual(expectedDtos[i].Email, result[i].Email);
                Assert.AreEqual(expectedDtos[i].MedicalConditions, result[i].MedicalConditions);
                Assert.AreEqual(expectedDtos[i].AppointmentHistory, result[i].AppointmentHistory);
            }

            _mockIPatientRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoPatientProfileExist()
        {
            // Arrange
            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Patient>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Expected an empty list when no OperationTypes exist.");
            _mockIPatientRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
        
        [Test]
        public async Task AddAsync_CreatesNewPatientProfile_WhenValidDtoIsProvided()
        {
            var user = new User(
                new UserId("1"),
                new Username("Diogo"),
                new UserEmail("1220812.isep.ipp.pt"),
                UserRole.Technician
                );
            
            var users = new List<User> { user };
            _mockIUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var patient1 = new Patient
            (
                new PatientName("Diana"),
                new BirthDate("30 de Junho de 2004"),
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1220917@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            // Arrange
            var medicalRecordNumber = "202410000001";
            
            var dto = new PatientDto
            {
                PatientName = "Miguel",
                BirthDate = "04 de Julho de 2004",
                Gender = "Masculino",
                MedicalRecordNumber = medicalRecordNumber,
                PhoneNumber = "935678124",
                MedicalConditions = "Alergia ao pó",
                EmergencyContact = "933264402",
                AppointmentHistory = "Alergia ao pó",
                Email = "1221144@isep.ipp.pt"
            };

            var result = await _service.AddAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dto.Email , result.Email);
            Assert.AreNotEqual(user.UserEmail.ToString(), result.Email);
            _mockIPatientRepository.Verify(repo => repo.AddAsync(It.IsAny<Patient>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }
        
        [Test]
        public async Task UpdateAsync_UpdatesExistingPatientProfile_WhenValidDtoIsProvided()
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
                MedicalConditions = "Alergia ao pó",
                EmergencyContact = "933264402",
                AppointmentHistory = "Alergia ao pó",
                Email = "1221194@isep.ipp.pt"
            };

            var patient = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber(medicalRecordNumber),
                new PhoneNumber("934260705"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("938413938"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1220917@isep.ipp.pt")
            );

            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(new MedicalRecordNumber(medicalRecordNumber)))
                .ReturnsAsync(patient);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dto.PatientName, result.PatientName);
            Assert.AreEqual(dto.BirthDate, result.BirthDate);
            Assert.AreEqual(dto.Gender, result.Gender);
            Assert.AreEqual(dto.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(dto.EmergencyContact, result.EmergencyContact);
            Assert.AreEqual(dto.MedicalConditions, result.MedicalConditions);
            Assert.AreEqual(dto.AppointmentHistory, result.AppointmentHistory);
            
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ReturnsNull_WhenPatientProfileDoesNotExist()
        {
            // Arrange
            var dto = new PatientDto
            {
                PatientName = "Paula",
                PhoneNumber = "934260705",
                MedicalRecordNumber = "1",
                EmergencyContact = "933264402",
                Email = "1221194@isep.ipp.pt"
            };
            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(new MedicalRecordNumber(dto.MedicalRecordNumber)))
                .ReturnsAsync((Patient)null);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            Assert.IsNull(result);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        [Test]
        public async Task GetAllByPatientNameAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist()
        {
            var patientName = new PatientName("Diana");

            var patient1 = new Patient
            (
                patientName,
                new BirthDate("30 de Junho de 2004"),
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            //Act
            var result = await _service.GetAllByPatientNameAsync("Diana");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(patient1.PatientName.ToString(), result[0].PatientName);
        }

        [Test]
        public async Task GetAllByBirthDateAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist()
        {
            var birthDate = new BirthDate("30 de Junho de 2004");

            var patient1 = new Patient
            (
                new PatientName("Diana"),
                birthDate,
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            //Act
            var result = await _service.GetAllByBirthDateAsync("30 de Junho de 2004");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(patient1.BirthDate.ToString(), result[0].BirthDate);
        }

        [Test]
        public async Task GetAllByEmailAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist()
        {
            var userEmail = new UserEmail("1221194@isep.ipp.pt");

            var patient1 = new Patient
            (
                new PatientName("Diana"),
                new BirthDate("30 de Junho de 2004"),
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                userEmail
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1220772@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            //Act
            var result = await _service.GetAllByEmailAsync("1221194@isep.ipp.pt");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(patient1.UserEmail.ToString(), result[0].Email);
        }

        [Test]
        public async Task GetAllByPhoneNumberAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist()
        {
            var phoneNumber = new PhoneNumber("933264402");

            var patient1 = new Patient
            (
                new PatientName("Diana"),
                new BirthDate("30 de Junho de 2004"),
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                phoneNumber,
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            //Act
            var result = await _service.GetAllByPhoneNumberAsync("933264402");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(patient1.PhoneNumber.ToString(), "933264402");
        }

        [Test]
        public async Task GetAllByGenderAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist()
        {
            var gender = new Gender("Female");

            var patient1 = new Patient
            (
                new PatientName("Diana"),
                new BirthDate("30 de Junho de 2004"),
                gender,
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patient2 = new Patient(
                new PatientName("Tomás"),
                new BirthDate("12 de Novembro de 2004"),
                new Gender("Male"),
                new MedicalRecordNumber("202410000090"),
                new PhoneNumber("938413938"),
                new MedicalConditions("Alergia ao pó"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("Alergia ao pó"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1, patient2 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            //Act
            var result = await _service.GetAllByGenderAsync("Female");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(patient1.Gender.ToString(), "Female");
        }

        [Test]
        public async Task GetByIdAsyncAsync_ReturnsListOfPatientDto_WhenRequestsExist()
        {
            var medicalRecordNumber = new MedicalRecordNumber("202410000112");

            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(medicalRecordNumber)).ReturnsAsync((Patient)null);

            //Act
            var result = await _service.GetByIdAsync(medicalRecordNumber);

            //Assert
            Assert.IsNull(result);
            _mockIPatientRepository.Verify(repo => repo.GetByIdAsync(medicalRecordNumber), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenPatientProfileDoesNotExist()
        {
            // Arrange
            var medicalRecordNumber = new MedicalRecordNumber("1");
            _mockIPatientRepository.Setup(repo => repo.GetByIdAsync(medicalRecordNumber)).ReturnsAsync((Patient)null);

            // Act
            var result = await _service.GetByIdAsync(medicalRecordNumber);

            // Assert
            Assert.IsNull(result);
            _mockIPatientRepository.Verify(repo => repo.GetByIdAsync(medicalRecordNumber), Times.Once);
        }
        
        [Test]
        public async Task DeleteAsync_ReturnsListOfPatientDto_WhenValidMedicalRecordNumberIsProvided()
        {
            var patient1 = new Patient
            (
                new PatientName("Diana"),
                new BirthDate("30 de Junho de 2004"),
                new Gender("Female"),
                new MedicalRecordNumber("202410000112"),
                new PhoneNumber("933264402"),
                new MedicalConditions("Asma"),
                new EmergencyContact("934260705"),
                new AppointmentHistory("2 de novembro de 2023"),
                new UserEmail("1221194@isep.ipp.pt")
            );

            var patients = new List<Patient> { patient1 };

            _mockIPatientRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

            var medicalRecordNumber = new MedicalRecordNumber("202410000112");
            
            //Act
            var result = await _service.DeleteAsync(medicalRecordNumber);

            //Assert
            Assert.IsNull(result);
        }
    }
}