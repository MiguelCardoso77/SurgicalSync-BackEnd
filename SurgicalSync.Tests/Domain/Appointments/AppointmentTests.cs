using System;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.SurgeryRooms;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Appointments;

[TestFixture]
public class AppointmentTests
{
    private Mock<AppointmentId> _mockAppointmentId;
    private Mock<Date> _mockDate;
    private Mock<Time> _mockTime;
    private Mock<RoomNumber> _mockRoomNumber;
    private Mock<OperationRequestId> _mockOperationRequestId;
    private Mock<RequiredStaff> _mockRequiredStaff;

    [SetUp]
    public void SetUp()
    {
        _mockAppointmentId = new Mock<AppointmentId>("1");
        _mockDate = new Mock<Date>(new DateTime(2025, 12, 25));
        _mockTime = new Mock<Time>(360);
        _mockRoomNumber = new Mock<RoomNumber>("1");
        _mockOperationRequestId = new Mock<OperationRequestId>("1");
        _mockRequiredStaff = new Mock<RequiredStaff>("Nurse");
    }
    
    [Test]
    public void TestConstructor()
    {
        var appointment = new Appointment(
            _mockAppointmentId.Object,
            Status.Scheduled,
            _mockDate.Object,
            _mockTime.Object,
            _mockRoomNumber.Object,
            _mockOperationRequestId.Object,
            _mockRequiredStaff.Object
        );
            
        Assert.AreEqual(Status.Scheduled, appointment.Status);
        Assert.AreEqual("20251225", appointment.Date.Value);
        Assert.AreEqual(360, appointment.Time.Value);
        Assert.AreEqual(_mockRoomNumber.Object, appointment.RoomNumber);
        Assert.AreEqual(_mockOperationRequestId.Object, appointment.OperationRequestId);
        Assert.AreEqual("Nurse", appointment.RequiredStaff.Value);
    }

    [Test]
    public void TestChangeStatus()
    {
        var appointment = new Appointment(
            _mockAppointmentId.Object,
            Status.Scheduled,
            _mockDate.Object,
            _mockTime.Object,
            _mockRoomNumber.Object,
            _mockOperationRequestId.Object,
            _mockRequiredStaff.Object
        );
        
        appointment.ChangeStatus(Status.Completed);
        
        Assert.AreEqual(Status.Completed, appointment.Status);
    }

    [Test]
    public void TestChangeDate()
    {
        var appointment = new Appointment(
            _mockAppointmentId.Object,
            Status.Scheduled,
            _mockDate.Object,
            _mockTime.Object,
            _mockRoomNumber.Object,
            _mockOperationRequestId.Object,
            _mockRequiredStaff.Object
        );
        
        var newDate = new Mock<Date>(new DateTime(2025, 12, 26));
        appointment.ChangeDate(newDate.Object);
        
        Assert.AreEqual("20251226", appointment.Date.Value);
    }
    
    [Test]
    public void TestChangeTime()
    {
        var appointment = new Appointment(
            _mockAppointmentId.Object,
            Status.Scheduled,
            _mockDate.Object,
            _mockTime.Object,
            _mockRoomNumber.Object,
            _mockOperationRequestId.Object,
            _mockRequiredStaff.Object
        );
        
        var newTime = new Mock<Time>(480);
        appointment.ChangeTime(newTime.Object);
        
        Assert.AreEqual(480, appointment.Time.Value);
    }
    
    [Test]
    public void TestChangeRoomNumber()
    {
        var appointment = new Appointment(
            _mockAppointmentId.Object,
            Status.Scheduled,
            _mockDate.Object,
            _mockTime.Object,
            _mockRoomNumber.Object,
            _mockOperationRequestId.Object,
            _mockRequiredStaff.Object
        );
        
        var newRoomNumber = new Mock<RoomNumber>("2");
        appointment.ChangeRoomNumber(newRoomNumber.Object);
        
        Assert.AreEqual(newRoomNumber.Object, appointment.RoomNumber);
    }
    
    [Test]
    public void TestChangeRequiredStaff()
    {
        var appointment = new Appointment(
            _mockAppointmentId.Object,
            Status.Scheduled,
            _mockDate.Object,
            _mockTime.Object,
            _mockRoomNumber.Object,
            _mockOperationRequestId.Object,
            _mockRequiredStaff.Object
        );
        
        var newRequiredStaff = new Mock<RequiredStaff>("Doctor");
        appointment.ChangeRequiredStaff(newRequiredStaff.Object);
        
        Assert.AreEqual("Doctor", appointment.RequiredStaff.Value);
    }
}