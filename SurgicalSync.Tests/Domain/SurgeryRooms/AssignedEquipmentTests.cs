using DDDNetCore.Domain.SurgeryRooms;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.SurgeryRooms;

[TestFixture]
public class AssignedEquipmentTests
{
    [Test]
    public void TestConstructor()
    {
        var equipment = new AssignedEquipment("X-Ray Machine");
        Assert.AreEqual("X-Ray Machine", equipment.Value);
    }
    
    [Test]
    public void TestToString()
    {
        var equipment = new AssignedEquipment("X-Ray Machine");
        Assert.AreEqual("X-Ray Machine", equipment.ToString());
    }
    
    [Test]
    public void TestEquals()
    {
        var equipment1 = new AssignedEquipment("X-Ray Machine");
        var equipment2 = new AssignedEquipment("X-Ray Machine");
        Assert.AreEqual(equipment1, equipment2);
    }
    
    [Test]
    public void TestNotEquals()
    {
        var equipment1 = new AssignedEquipment("X-Ray Machine");
        var equipment2 = new AssignedEquipment("MRI Machine");
        Assert.AreNotEqual(equipment1, equipment2);
    }
    
    [Test]
    public void TestEqualHashCodes()
    {
        // Arrange
        var equipment1 = new AssignedEquipment("X-Ray Machine");
        var equipment2 = new AssignedEquipment("X-Ray Machine");
        
        // Act
        var hashCode1 = equipment1.GetHashCode();
        var hashCode2 = equipment2.GetHashCode();
        
        // Assert
        Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
    }
    
    [Test]
    public void TestDifferentHashCodes()
    {
        // Arrange
        var equipment1 = new AssignedEquipment("X-Ray Machine");
        var equipment2 = new AssignedEquipment("MRI Machine");
        
        // Act
        var hashCode1 = equipment1.GetHashCode();
        var hashCode2 = equipment2.GetHashCode();
        
        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
    }
    
    [Test]
    public void TestNull()
    {
        var equipment = new AssignedEquipment(null);
        Assert.AreEqual(null, equipment.Value);
    }
    
    [Test]
    public void TestEmpty()
    {
        var equipment = new AssignedEquipment(string.Empty);
        Assert.AreEqual(string.Empty, equipment.Value);
    }
    
    [Test]
    public void TestWhitespace()
    {
        var equipment = new AssignedEquipment(" ");
        Assert.AreEqual(" ", equipment.Value);
    }
}