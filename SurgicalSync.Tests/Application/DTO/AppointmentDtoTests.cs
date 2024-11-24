using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class AppointmentDtoTests
{
    [Test]
    public void TestIncompleteAppointmentDto()
    {
        var appointmentDto = new AppointmentDto
        {
            Id = "1",
            Status = "Scheduled",
        };

        Assert.That(appointmentDto.Id, Is.EqualTo("1"));
        Assert.That(appointmentDto.Status, Is.EqualTo("Scheduled"));
    }
    
    [Test]
    public void TestCompleteAppointmentDto()
    {
        var appointmentDto = new AppointmentDto
        {
            Id = "1",
            Status = "Scheduled",
            Date = "2021-01-01",
            Time = "09:00",
            RoomNumber = "101",
        };

        Assert.That(appointmentDto.Id, Is.EqualTo("1"));
        Assert.That(appointmentDto.Status, Is.EqualTo("Scheduled"));
        Assert.That(appointmentDto.Date, Is.EqualTo("2021-01-01"));
        Assert.That(appointmentDto.Time, Is.EqualTo("09:00"));
        Assert.That(appointmentDto.RoomNumber, Is.EqualTo("101"));
    }
    
}