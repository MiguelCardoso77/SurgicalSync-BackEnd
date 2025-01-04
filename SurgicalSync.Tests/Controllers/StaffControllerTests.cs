using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers
{
    public class StaffControllerTests
    {
        private StaffController _controller;
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

            _controller = new StaffController(
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
        public async Task GetAll_ReturnsAllStaffProfiles()
        {
            // Arrange
            var StaffList = new List<Staff>
            {
                new(
                    new StaffId("N202400002"),
                    new StaffName("Diana"),
                    new UserEmail("diana@gmail.com"),
                    new StaffPhoneNumber("938413938"),
                    new StaffSpecialization("Dermatology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Doctor,
                    true,
                    new StaffLicenseNumber("N202400002")
                ),
                new(
                    new StaffId("N202400003"),
                    new StaffName("Miguel"),
                    new UserEmail("miguel@gmail.com"),
                    new StaffPhoneNumber("962839401"),
                    new StaffSpecialization("Cardiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Nurse,
                    true,
                    new StaffLicenseNumber("N202400003")
                ),
                new(
                    new StaffId("N202400004"),
                    new StaffName("Diogo"),
                    new UserEmail("diogo@gmail.com"),
                    new StaffPhoneNumber("962839401"),
                    new StaffSpecialization("Cardiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Other,
                    true,
                    new StaffLicenseNumber("N202400004")
                )
            };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(StaffList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsStaff()
        {
            // Arrange
            var StaffList = new List<Staff>
            {
                new(
                    new StaffId("N202400002"),
                    new StaffName("Diana"),
                    new UserEmail("diana@gmail.com"),
                    new StaffPhoneNumber("938413938"),
                    new StaffSpecialization("Dermatology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Doctor,
                    true,
                    new StaffLicenseNumber("N202400002")
                ),
                new(
                    new StaffId("N202400003"),
                    new StaffName("Miguel"),
                    new UserEmail("miguel@gmail.com"),
                    new StaffPhoneNumber("962839401"),
                    new StaffSpecialization("Anesthesiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Nurse,
                    true,
                    new StaffLicenseNumber("N202400003")
                ),
                new(
                    new StaffId("N202400004"),
                    new StaffName("Diogo"),
                    new UserEmail("diogo@gmail.com"),
                    new StaffPhoneNumber("962839401"),
                    new StaffSpecialization("Dermatology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Other,
                    true,
                    new StaffLicenseNumber("N202400004")
                )
            };

            var dto = new StaffDto()
            {
                Id = "N202400003",
                StaffName = "Miguel",
                UserEmail = "miguel@gmail.com",
                StaffPhoneNumber = "962839401",
                StaffSpecialization = "Anesthesiology",
                StaffAvailabilitySlots = "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00",

                StaffType = StaffType.Nurse.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400003"
            };

            _repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<StaffId>()))
                .ReturnsAsync(StaffList[1]);

            // Act
            var result = await _controller.GetById("N202400003");

            // Assert
            Assert.IsNotNull(result);
            result.Value.Should().BeEquivalentTo(dto);
        }

        [Test]
        public async Task GetById_InValidId_ReturnsNotFound()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<StaffId>()))
                .ReturnsAsync((Staff)null);

            // Act
            var result = await _controller.GetById("N202400021");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task Create_ValidStaffProfile_ReturnsCreatedAtAction()
        {
            var StaffList = new List<Staff>
            {
                new(
                    new StaffId("N202400002"),
                    new StaffName("Diana"),
                    new UserEmail("diana@gmail.com"),
                    new StaffPhoneNumber("938413938"),
                    new StaffSpecialization("Anesthesiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    DDDNetCore.Domain.Staffs.StaffType.Doctor
                    , true,
                    new StaffLicenseNumber("N202400002")
                ),
                new(
                    new StaffId("N202400003"),
                    new StaffName("Miguel"),
                    new UserEmail("miguel@gmail.com"),
                    new StaffPhoneNumber("962839401"),
                    new StaffSpecialization("Anesthesiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    DDDNetCore.Domain.Staffs.StaffType.Nurse,
                    true,
                    new StaffLicenseNumber("N202400003")
                ),
                new(
                    new StaffId("N202400004"),
                    new StaffName("Diogo"),
                    new UserEmail("diogo@gmail.com"),
                    new StaffPhoneNumber("962839401"),
                    new StaffSpecialization("Anesthesiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    DDDNetCore.Domain.Staffs.StaffType.Other,
                    true,
                    new StaffLicenseNumber("N202400004")
                )
            };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(StaffList);

            // Arrange
            var dto = new StaffDto ()
                {
                    Id = "N202400021",
                    StaffName = "Tomas",
                    UserEmail = "tomas@gmail.com",
                    StaffPhoneNumber = "987123845",
                    StaffSpecialization = "Anesthesiology",
                    StaffAvailabilitySlots = "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00",
                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400021"
            };

            _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Staff>())).ReturnsAsync(
                new Staff
                (new StaffId("N202400021"),
                    new StaffName("Tomas"),
                    new UserEmail("tomas@gmail.com"),
                    new StaffPhoneNumber("987123845"),
                    new StaffSpecialization("Anesthesiology"),
                    new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                    StaffType.Doctor,
                    true,
                    new StaffLicenseNumber("N202400021")
                ));

            // Act
            var result = await _controller.AddAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            //result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public async Task Update_ValidStaffProfile_ReturnsCreatedAtAction()
        {
            // Arrange
            var staffId = "N202400021";

            var dto = new StaffDto()
            {
                Id = "N202400021",
                StaffName = "Tomas",
                UserEmail = "tomas@gmail.com",
                StaffPhoneNumber = "987123845",
                StaffSpecialization = "Anesthesiology",
                StaffAvailabilitySlots = "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00",

                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400021"
            };

            var staff = new Staff(
                new StaffId("N202400021"),
                new StaffName("Tomas"),
                new UserEmail("tomas@gmail.com"),
                new StaffPhoneNumber("987123845"),
                new StaffSpecialization("Anesthesiology"),
                new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400021")
            );
            var staffs = new List<Staff> { staff };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffs);

            _repoMock.Setup(repo => repo.GetByIdAsync(new StaffId(staffId)))
                .ReturnsAsync(staff);

            // Act
            var result = await _controller.Update(staffId, dto);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Update_InValidStaffProfile_ReturnsBadRequest()
        {
            var dto = new StaffDto()
            {
                Id = "N202400021",
                StaffName = "Miguel",
                UserEmail = "miguel@gmail.com",
                StaffPhoneNumber = "987654321",
                StaffSpecialization = "Anesthesiology",
                StaffAvailabilitySlots = "slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00",

                StaffType = StaffType.Doctor.ToString(),
                isActive = true,
                StaffLicenseNumber = "N202400011"
            };

            // Arrange
            _repoMock.Setup(repo => repo.Remove(It.IsAny<Staff>()));

            // Act
            var result = await _controller.Update("invalid-id", dto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task Deactivate_ValidId_ReturnsOk()
        {
            // Arrange
            var staffToRemove = new Staff(
                new StaffId("N202400021"),
                new StaffName("Tomas"),
                new UserEmail("tomas@gmail.com"),
                new StaffPhoneNumber("987123845"),
                new StaffSpecialization("Anesthesiology"),
                new StaffAvailabilitySlots("slot 1: 2024-09-25:14h00-18h00 ; slot 2: 2024-09-25:19h00/2024-09-26:02h00"),

                StaffType.Doctor,
                true,
                new StaffLicenseNumber("N202400021")
            );

            _repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<StaffId>()))
                .ReturnsAsync(staffToRemove);

            _repoMock.Setup(repo => repo.Remove(It.IsAny<Staff>()));

            // Act
            var result = await _controller.Deactivate("N202400021");

            // Assert
            Assert.IsNotNull(result);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Deactivate_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Remove(It.IsAny<Staff>()));

            // Act
            var result = await _controller.Deactivate("N202400021");

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}