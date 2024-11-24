using DDDNetCore.Domain.SurgeryRooms;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.SurgeryRooms;

[TestFixture]
public class MaintenanceSlotsTests
{
    [Test]
    public void TestConstructor()
    {
        var maintenanceSlots = new MaintenanceSlots("18:00,20:00");
        Assert.AreEqual("18:00,20:00", maintenanceSlots.Value);
    }
    
    [Test]
    public void TestEquals()
    {
        var maintenanceSlots1 = new MaintenanceSlots("18:00,20:00");
        var maintenanceSlots2 = new MaintenanceSlots("18:00,20:00");
        Assert.AreEqual(maintenanceSlots1, maintenanceSlots2);
    }
    
    [Test]
    public void TestNotEquals()
    {
        var maintenanceSlots1 = new MaintenanceSlots("18:00,20:00");
        var maintenanceSlots2 = new MaintenanceSlots("20:00,22:00");
        Assert.AreNotEqual(maintenanceSlots1, maintenanceSlots2);
    }
    
    [Test]
    public void TestEqualHashCodes()
    {
        // Arrange
        var maintenanceSlots1 = new MaintenanceSlots("18:00,20:00");
        var maintenanceSlots2 = new MaintenanceSlots("18:00,20:00");

        // Act
        var hashCode1 = maintenanceSlots1.GetHashCode();
        var hashCode2 = maintenanceSlots2.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
    }
    
    [Test]
    
    public void TestDifferentHashCodes()
    {
        // Arrange
        var maintenanceSlots1 = new MaintenanceSlots("18:00,20:00");
        var maintenanceSlots2 = new MaintenanceSlots("20:00,22:00");

        // Act
        var hashCode1 = maintenanceSlots1.GetHashCode();
        var hashCode2 = maintenanceSlots2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
    } 
}