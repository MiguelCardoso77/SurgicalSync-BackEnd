using System;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.SurgeryRooms;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Mappers;

[TestFixture]
public class AppointmentMapperTest
{
    private AppointmentMapper _mapper;
    private Mock<AppointmentId> _mockAppointmentId;
    private Mock<Date> _mockDate;
    private Mock<Time> _mockTime;
    private Mock<OperationRequestId> _mockOperationRequestId;
    private Mock<RoomNumber> _mockRoomNumber;
    private Mock<RequiredStaff> _mockRequiredStaff;
    
    [SetUp]
    public void Setup()
    {
        _mapper = new AppointmentMapper();
        _mockAppointmentId = new Mock<AppointmentId>("1");
        _mockDate = new Mock<Date>(new DateTime(2024, 12, 31));
        _mockTime = new Mock<Time>(300);
        _mockOperationRequestId = new Mock<OperationRequestId>("1");
        _mockRoomNumber = new Mock<RoomNumber>("1");
        _mockRequiredStaff = new Mock<RequiredStaff>("Nurses");
    }
    
    [Test]
    public void TestToDto()
    {
        var appointment = new Appointment(_mockAppointmentId.Object, Status.Scheduled, _mockDate.Object, _mockTime.Object,
        _mockRoomNumber.Object, _mockOperationRequestId.Object, _mockRequiredStaff.Object);
        var appointmentDto = _mapper.ToDto(appointment);
        
        Assert.AreEqual(appointment.Id.AsString(), appointmentDto.Id);
        Assert.AreEqual(appointment.Status.ToString(), appointmentDto.Status);
        Assert.AreEqual(appointment.Date.Value, appointmentDto.Date);
        Assert.AreEqual(appointment.OperationRequestId.AsString(), appointmentDto.OperationRequestId);
        Assert.AreEqual(appointment.RoomNumber.AsString(), appointmentDto.RoomNumber);
    }
    
    [Test]
    public void TestToDomain()
    {
        var appointmentDto = new AppointmentDto
        {
            Id = "1",
            Status = "Scheduled",
            Date = "2024/12/31",
            Time = "340",
            OperationRequestId = "1",
            RoomNumber = "1",
            RequiredStaff = "Nurses"
        };
        var appointment = _mapper.ToDomain(appointmentDto, _mockAppointmentId.Object);
        
        Assert.AreEqual(appointmentDto.Status, appointment.Status.ToString());
        Assert.AreEqual("20241231", appointment.Date.Value);
        Assert.AreEqual(appointmentDto.Time, appointment.Time.Value.ToString());
        Assert.AreEqual(appointmentDto.OperationRequestId, appointment.OperationRequestId.AsString());
        Assert.AreEqual(appointmentDto.RoomNumber, appointment.RoomNumber.AsString());
        Assert.AreEqual(appointmentDto.RequiredStaff, appointment.RequiredStaff.Value);
    }
    
}