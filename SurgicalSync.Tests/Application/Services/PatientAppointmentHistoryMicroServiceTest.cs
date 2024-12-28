using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.SurgeryRooms;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services;

[TestFixture]
public class PatientAppointmentHistoryMicroServiceTest
{
    private Mock<IOperationRequestRepository> _operationRequestRepository;
    private Mock<IAppointmentsRepository> _appointmentsRepository;
    private PatientAppointmentHistoryMicroService _patientAppointmentHistoryMicroService;

    [SetUp]
    public void Setup()
    {
        _operationRequestRepository = new Mock<IOperationRequestRepository>();
        _appointmentsRepository = new Mock<IAppointmentsRepository>();
        _patientAppointmentHistoryMicroService = new PatientAppointmentHistoryMicroService(
            _operationRequestRepository.Object,
            _appointmentsRepository.Object);
    }

    [Test]
    public async Task GetPatientOperationRequests_ReturnsOperationRequests()
    {
        var medicalRecordNumber = new MedicalRecordNumber("202411000002");
        
        // Arrange
        var operationRequestId1 = new OperationRequestId("1");
        var operationRequestId2 = new OperationRequestId("2");
        var deadlineDate1 = new DeadlineDate(new DateTime(2025, 10, 1));
        var deadlineDate2 = new DeadlineDate(new DateTime(2025, 10, 2));
        var operationTypeId1 = new OperationTypeId("2");
        var operationTypeId2 = new OperationTypeId("3");
        var licenseNumber1 = new StaffId("D202400001");
        var licenseNumber2 = new StaffId("D202400002");

        var priority1 = Priority.ElectiveSurgery;
        var priority2 = Priority.UrgentSurgery;

        var operationRequest1 = new OperationRequest(
            operationRequestId1,
            priority1,
            deadlineDate1,
            operationTypeId1,
            medicalRecordNumber,
            licenseNumber1
        );

        var operationRequest2 = new OperationRequest(
            operationRequestId2,
            priority2,
            deadlineDate2,
            operationTypeId2,
            medicalRecordNumber,
            licenseNumber2
        );
        
        var expectedDto = new List<OperationRequestDto>
        {
            new OperationRequestDto
            {
                OperationRequestId = operationRequestId1.AsString(),
                DeadlineDate = deadlineDate1.DateTime.ToString("yyyy-MM-dd"),
                StaffId = licenseNumber1.AsString(),
                Priority = priority1.ToString(),
                OperationTypeId = operationTypeId1.AsString(),
                MedicalRecordNumber = medicalRecordNumber.AsString()
            },
            new OperationRequestDto
            {
                OperationRequestId = operationRequestId2.AsString(),
                DeadlineDate = deadlineDate2.DateTime.ToString("yyyy-MM-dd"),
                StaffId = licenseNumber2.AsString(),
                Priority = priority2.ToString(),
                OperationTypeId = operationTypeId2.AsString(),
                MedicalRecordNumber = medicalRecordNumber.AsString()
            }
        };

        var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };

        _operationRequestRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

        var result = await _patientAppointmentHistoryMicroService.GetPatientOperationRequests(medicalRecordNumber);
        
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedDto.Count, result.Count, "The number of elements in the result list does not match the expected list.");

        for (int i = 0; i < expectedDto.Count; i++)
        {
            Assert.AreEqual(expectedDto[i].OperationRequestId, result[i].OperationRequestId, $"Mismatch at index {i} for OperationRequestId.");
            Assert.AreEqual(expectedDto[i].MedicalRecordNumber, result[i].MedicalRecordNumber, $"Mismatch at index {i} for MedicalRecordNumber.");
            Assert.AreEqual(expectedDto[i].StaffId, result[i].StaffId, $"Mismatch at index {i} for LicenseNumber.");
            Assert.AreEqual(expectedDto[i].OperationTypeId, result[i].OperationTypeId, $"Mismatch at index {i} for OperationTypeId.");
            Assert.AreEqual(expectedDto[i].Priority, result[i].Priority, $"Mismatch at index {i} for Priority.");
            Assert.AreEqual(expectedDto[i].DeadlineDate, result[i].DeadlineDate, $"Mismatch at index {i} for DeadlineDate.");
        }

        _operationRequestRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Test]
public async Task GetPatientAppointments_ReturnsAppointments()
{
    var medicalRecordNumber = new MedicalRecordNumber("202411000002");
    
    // Arrange
    var appointmentId1 = new AppointmentId("1");
    var appointmentId2 = new AppointmentId("2");
    var appointmentId3 = new AppointmentId("3");
    var status = Status.Scheduled;
    var date = new Date(new DateTime(2025, 10, 1));
    var date1 = new Date(new DateTime(2025, 10, 2));
    var date2 = new Date(new DateTime(2025, 10, 2));
    var time = new Time(100);
    var operationRequestId1 = new OperationRequestId("1");
    var operationRequestId2 = new OperationRequestId("2");
    var operationRequestId3 = new OperationRequestId("3");
    var roomNumber = new RoomNumber("1");
    var requiredStaff = new RequiredStaff("2 doctor");

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

    var appointment3 = new Appointment(
        appointmentId3,
        status,
        date2,
        time,
        roomNumber,
        operationRequestId3,
        requiredStaff
    );

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

    var appointments = new List<Appointment> { appointment1, appointment2, appointment3 };
    var operationRequests = new List<OperationRequest> { operationRequest1, operationRequest2 };
    var operationRequestDtos = new List<OperationRequestDto>
    {
        new OperationRequestDto
        {
            OperationRequestId = operationRequestId1.AsString(),
            MedicalRecordNumber = medicalRecordNumber.AsString(),
            Priority = Priority.ElectiveSurgery.ToString(),
            DeadlineDate = new DateTime(2025, 10, 1).ToString("yyyy-MM-dd"),
            OperationTypeId = "1",
            StaffId = "D202400001"
        },
        new OperationRequestDto
        {
            OperationRequestId = operationRequestId2.AsString(),
            MedicalRecordNumber = medicalRecordNumber.AsString(),
            Priority = Priority.UrgentSurgery.ToString(),
            DeadlineDate = new DateTime(2025, 10, 2).ToString("yyyy-MM-dd"),
            OperationTypeId = "2",
            StaffId = "D202400002"
        }
    };

    _appointmentsRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);
    _operationRequestRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

    // Act
    var result = await _patientAppointmentHistoryMicroService.GetAllPatientAppointmentHistoryAsync(medicalRecordNumber);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual(2, result.Count, "Should return only appointments related to the patient's operation requests");
    
    // Verify the appointments are correctly associated with the patient's operation requests
    Assert.IsTrue(result.Any(a => a.Id.Equals(appointmentId1)));
    Assert.IsTrue(result.Any(a => a.Id.Equals(appointmentId2)));
    Assert.IsFalse(result.Any(a => a.Id.Equals(appointmentId3)), "Should not include appointments for other patients");

    // Verify repository calls - now expecting GetAllAsync to be called twice
    _appointmentsRepository.Verify(repo => repo.GetAllAsync(), Times.Exactly(2));
    _operationRequestRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
}
}