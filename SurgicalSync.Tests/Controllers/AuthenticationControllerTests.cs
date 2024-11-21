using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private AuthenticationController _controller;
        private AuthenticationService _service;
        private PatientService _patientService;
        private PatientMapper _patientMapper;
        private DeletePatientMicroService _deletePatientMicroService;
        private UserEmailMicroService _userEmailMicroService;
        private UserService _userService;
        private UserMapper _userMapper;
        private PatientMicroService _patientMicroService;

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
            _mockIUserRepository = new Mock<IUserRepository>();
            
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

            _patientService = new PatientService(
                _mockUnitOfWork.Object,
                _mockIPatientRepository.Object,
                _patientMapper,
                _userEmailMicroService,
                _deletePatientMicroService,
                _loggerMockPatient.Object
            );

            _patientMicroService = new PatientMicroService(
                _patientService
            );

            if (FirebaseApp.DefaultInstance == null)
            {
                // Configuração do Firebase Admin SDK
                FirebaseApp.Create(new AppOptions()
                {
                    Credential =
                        GoogleCredential.FromFile("../../../surgicalsync-d5bd5-firebase-adminsdk-7v461-12fb9fe637.json")
                });
            }

            _service = new AuthenticationService(
                _patientMicroService
            );
            
            _controller = new AuthenticationController(
                _service
            );
        }
        
        [Test]
        public async Task LoginWithEmailPasswordAsync_ReturnsString_WhenValidDtoIsProvided()
        {
            var dto = new LoginDto
            {
                Email = "1220917@isep.ipp.pt",
                Password = "57+&Kt9X(J",
                ReturnSecureToken = "true"
            };
            
            //Act
            var result = await _controller.LoginWithEmailPassword(dto);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}