using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class SurgeryRoomDtoTests
{
    [Test]
    public void TestCreateIncompleteSurgeryRoomDto()
    {
        var dto = new SurgeryRoomDto()
        {
            RoomNumber = "1",
            MaintenanceSlots = "Test",
        };

        Assert.AreEqual(dto.RoomNumber, "1");
        Assert.AreEqual(dto.MaintenanceSlots, "Test");
    }
    
    [Test]
    public void TestCreateCompleteSurgeryRoomDto()
    {
        var dto = new SurgeryRoomDto()
        {
            RoomNumber = "1",
            MaintenanceSlots = "Test",
            CurrentStatus = "Available",
            AssignedEquipment = "Scalpel, Forceps",
            Capacity = "10",
            Type = "Operating"
        };

        Assert.AreEqual(dto.RoomNumber, "1");
        Assert.AreEqual(dto.MaintenanceSlots, "Test");
        Assert.AreEqual(dto.CurrentStatus, "Available");
        Assert.AreEqual(dto.AssignedEquipment, "Scalpel, Forceps");
        Assert.AreEqual(dto.Capacity, "10");
        Assert.AreEqual(dto.Type, "Operating");
    }
}