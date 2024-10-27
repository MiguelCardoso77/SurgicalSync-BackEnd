using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services
{
    public class StaffServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IStaffRepository> _repoMock;
        private StaffMapper _mapper;
        private StaffService _service;
        private static int _lastGeneratedNumber;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IStaffRepository>();
            _mapper = new StaffMapper();
            _lastGeneratedNumber = 00003;

            _service = new StaffService(
                _unitOfWorkMock.Object,
                _repoMock.Object,
                _mapper
            );
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfStaffDto_WhenRequestsExist()
        {
            var staff1 = new Staff
            (
                new StaffId("N202400002"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400001")
            );

            var staff2 = new Staff(
                new StaffId("N202400001"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400001")
            );

            var staffs = new List<Staff> { staff1, staff2 };

            var expectedDtos = staffs.Select(pt => new StaffDto()
            {
                Id = "N202400002",
                StaffName = "Raquel Gonçalves",
                UserEmail = "raquelgoncalves@gmail.com",
                StaffPhoneNumber = "962839401",
                StaffSpecialization = StaffSpecialization.Dermatology.ToString(),
                StaffAvaiabilitySlots = new List<string>()
                    { "slot 1: 2024-09-25:14h00-18h00", "slot 2: 2024-09-25:19h00/2024-09-26:02h00" },
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400001"
            }).ToList();

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            //Act
            var result = await _service.GetAllAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDtos.Count, result.Count);

            for (int i = 0; i < expectedDtos.Count; i++)
            {
                Assert.AreEqual(expectedDtos[i].StaffName, result[i].StaffName);
                Assert.AreEqual(expectedDtos[i].UserEmail, result[i].UserEmail);
                Assert.AreEqual(expectedDtos[i].StaffSpecialization, result[i].StaffSpecialization);
            }

            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoStaffProfileExist()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Staff>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result, "Expected an empty list when no Staff exist.");
            _repoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task AddAsync_CreatesNewStaffProfile_WhenValidDtoIsProvided()
        {
            var staff1 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Diana"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400001")
            );

            var staff2 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staffs = new List<Staff> { staff1, staff2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);
            
            // Arrange
            var id = "N202400082";

            var dto = new StaffDto()
            {
                Id = id,
                StaffName = "Miguel",
                UserEmail = "miguel@gmail.com",
                StaffPhoneNumber = "987654321",
                StaffSpecialization = StaffSpecialization.Dermatology.ToString(),
                StaffAvaiabilitySlots = new List<string>()
                    { "slot 1: 2024-09-25:14h00-18h00", "slot 2: 2024-09-25:19h00/2024-09-26:02h00" },
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400011"
            };

            var result = await _service.AddAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dto.UserEmail, result.UserEmail);
            Assert.AreEqual(dto.StaffLicenseNumber, result.StaffLicenseNumber);
            Assert.AreNotEqual(staff1.StaffPhoneNumber.ToString(), result.StaffPhoneNumber);
            Assert.AreNotEqual(staff1.UserEmail.ToString(), dto.UserEmail);
            Assert.AreNotEqual(staff2.StaffPhoneNumber.ToString(), result.StaffPhoneNumber);
            Assert.AreNotEqual(staff2.UserEmail.ToString(), dto.UserEmail);
            Assert.AreNotEqual(staff1.StaffLicenseNumber.ToString(), result.StaffLicenseNumber);
            Assert.AreNotEqual(staff2.StaffLicenseNumber.ToString(), result.StaffLicenseNumber);
            
            _repoMock.Verify(repo => repo.AddAsync(It.IsAny<Staff>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_UpdatesExistingStaffProfile_WhenValidDtoIsProvided()
        {
            // Arrange
            var id = "N202400002";

            var dto = new StaffDto()
            {
                Id = id,
                StaffName = "miguel",
                UserEmail = "1221194@isep.ipp.pt",
                StaffPhoneNumber = "987654321",
                StaffSpecialization = StaffSpecialization.Dermatology.ToString(),
                StaffAvaiabilitySlots = new List<string>()
                    { "slot 1: 2024-09-25:14h00-18h00", "slot 2: 2024-09-25:19h00/2024-09-26:02h00" },
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400002"
            };

            var staff = new Staff(
                new StaffId("N202400002"),
                new StaffName("miguel"),
                new UserEmail("1221194@isep.ipp.pt"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02hx00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );
            var staffs = new List<Staff> { staff };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            _repoMock.Setup(repo => repo.GetByIdAsync(new StaffId(id)))
                .ReturnsAsync(staff);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dto.StaffName, result.StaffName);
            Assert.AreEqual(dto.UserEmail, result.UserEmail);
            Assert.AreEqual(dto.StaffPhoneNumber, result.StaffPhoneNumber);
            Assert.AreEqual(dto.StaffType, result.StaffType);
            Assert.AreEqual(dto.StaffPhoneNumber, result.StaffPhoneNumber);

            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ReturnsNull_WhenStaffProfileDoesNotExist()
        {
            // Arrange
            var dto = new StaffDto()
            {
                Id = "N202400702",
                StaffName = "Raquel Gonçalves",
                UserEmail = "raquelgoncalves@gmail.com",
                StaffPhoneNumber = "962839401",
                StaffSpecialization = StaffSpecialization.Dermatology.ToString(),
                StaffAvaiabilitySlots = new List<string>()
                    { "slot 1: 2024-09-25:14h00-18h00", "slot 2: 2024-09-25:19h00/2024-09-26:02h00" },
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400001"
            };

            _repoMock.Setup(repo => repo.GetByIdAsync(new StaffId(dto.Id)))
                .ReturnsAsync((Staff)null);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            Assert.IsNull(result);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        [Test]
        public async Task GetAllByStaffNameAsync_ReturnsListOfStaffDtoList_WhenStaffProfileExist()
        {
            var staffName = new StaffName("Diana");

            var staff1 = new Staff(
                new StaffId("N202400002"),
                staffName,
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staff2 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staffs = new List<Staff> { staff1, staff2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            //Act
            var result = await _service.GetAllByName("Diana");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(staff1.StaffName.ToString(), result[0].StaffName);
        }

        [Test]
        public async Task GetAllBySpecializationAsync_ReturnsListOfStaffDtoList_WhenStaffProfileExist()
        {
            var specialization = StaffSpecialization.Anesthesiology;

            var staff1 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Raquel"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                specialization,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staff2 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staffs = new List<Staff> { staff1, staff2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            //Act
            var result = await _service.GetAllBySpecialization("Anesthesiology");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(staff1.StaffSpecialization.ToString(), result[0].StaffSpecialization);
        }

        [Test]
        public async Task GetAllByEmailAsync_ReturnsListOfStaffDtoList_WhenStaffProfileExist()
        {
            var email = new UserEmail("tomasgoncalves@gmail.com");

            var staff1 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Raquel"),
                email,
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staff2 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staffs = new List<Staff> { staff1, staff2 };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            //Act
            var result = await _service.GetAllByEmail("tomasgoncalves@gmail.com");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(staff1.UserEmail.ToString(), result[0].UserEmail);
        }

        [Test]
        public async Task GetByIdAsyncAsync_ReturnsListOfStaffDto_WhenRequestsExist()
        {
            var staffId = new StaffId("N202400002");

            _repoMock.Setup(repo => repo.GetByIdAsync(staffId)).ReturnsAsync((Staff)null);

            //Act
            var result = await _service.GetByIdAsync(staffId);

            //Assert
            Assert.IsNull(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(staffId), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenStaffProfileDoesNotExist()
        {
            // Arrange
            var staffId = new StaffId("N202400802");

            _repoMock.Setup(repo => repo.GetByIdAsync(staffId)).ReturnsAsync((Staff)null);

            //Act
            var result = await _service.GetByIdAsync(staffId);

            //Assert
            Assert.IsNull(result);
            _repoMock.Verify(repo => repo.GetByIdAsync(staffId), Times.Once);
        }

        [Test]
        public async Task GenerateLN_ReturnsStaffId_WhenValidDtoIsProvided()
        {
            var dto = new StaffDto()
            {
                StaffName = "Diana",
                UserEmail = "diananeves@gmail.com",
                StaffPhoneNumber = "938413938",
                StaffSpecialization = StaffSpecialization.Anesthesiology.ToString(),
                StaffAvaiabilitySlots = new List<string>()
                    { "slot 1: 2024-09-25:14h00-18h00", 
                        "slot 2: 2024-09-25:19h00/2024-09-26:02h00" },
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400112"
            };
            
            var staff1 = new Staff(
                new StaffId("N202400002"),
                new StaffName("Tomas"),
                new UserEmail("tomasgoncalves@gmail.com"),
                new StaffPhoneNumber("963456798"),
                StaffSpecialization.Anesthesiology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Nurse,
                true,
                new StaffLicenseNumber("N202400002")
            );

            var staff2 = new Staff(
                new StaffId("N202400003"),
                new StaffName("Raquel Gonçalves"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Other,
                true,
                new StaffLicenseNumber("N202400003")
            );

            var staffs = new List<Staff> { staff1, staff2 };
            
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            var staffType = StaffType.Doctor;
            
            var result = _service.GenerateLN(dto,staffType,staffs);
            
            Assert.IsNotNull(result);
        }
        
        [Test]
        public async Task DeactivateAsync_ReturnsListOfStaffDto_WhenValidStaffIdIsProvided()
        {
            var staffId = new StaffId("N202400022");

            var staff = new Staff(
                staffId,
                new StaffName("Diana"),
                new UserEmail("raquelgoncalves@gmail.com"),
                new StaffPhoneNumber("962839401"),
                StaffSpecialization.Dermatology,
                new List<StaffAvaiabilitySlots>
                {
                    new("slot 1: 2024-09-25:14h00-18h00"),
                    new("slot 2: 2024-09-25:19h00/2024-09-26:02h00")
                },
                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400002")
            );
            var staffs = new List<Staff> { staff};

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);
            
            //Act
            var result = await _service.DeactivateAsync(staffId);

            //Assert
            Assert.IsNull(result);
        }
    }
}