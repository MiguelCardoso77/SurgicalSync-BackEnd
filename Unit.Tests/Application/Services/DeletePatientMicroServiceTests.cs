using System.Threading.Tasks;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Services
{
    [TestFixture]
    public class DeletePatientMicroServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPatientRepository> _patientRepositoryMock;
        private Mock<IUserRepository> _repoMock;
        private UserMapper _mapper;
        private UserService _userService;
        private Mock<ILogger<DeletePatientMicroService>> _loggerMock;
        private PatientMapper _patientMapper;
        private DeletePatientMicroService _service;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _loggerMock = new Mock<ILogger<DeletePatientMicroService>>();
            _repoMock = new Mock<IUserRepository>();
            _mapper = new UserMapper();

            _userService = new UserService(
                _unitOfWorkMock.Object,
                _repoMock.Object,
                _mapper
            );

            _patientMapper = new PatientMapper();

            _service = new DeletePatientMicroService(
                _unitOfWorkMock.Object,
                _patientRepositoryMock.Object,
                _patientMapper,
                _userService,
                _loggerMock.Object
                );
        }

        [Test]
        public async Task DeletePatientDataByGDPRAndAccount_NoPatientFound_ReturnsNull()
        {
            // Arrange
            var medicalRecordNumber = new MedicalRecordNumber("123");
            _patientRepositoryMock.Setup(x => x.GetByIdAsync(medicalRecordNumber)).ReturnsAsync((Patient)null);

            // Act
            var result = await _service.DeletePatientDataByGDPRAndAccount(medicalRecordNumber);

            // Assert
            Assert.IsNull(result);
        }
    }
}
