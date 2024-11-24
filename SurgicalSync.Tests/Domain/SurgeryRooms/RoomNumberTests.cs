using DDDNetCore.Domain.SurgeryRooms;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.SurgeryRooms;

[TestFixture]
public class RoomNumberTests
{
    [Test]
    public void ShouldCreateRoomNumber()
    {
        // Arrange
        var roomNumber = "1";

        // Act
        var room = new RoomNumber(roomNumber);

        // Assert
        Assert.AreEqual(roomNumber, room.Value);
    }
    
    [Test]
    public void ShouldCreateNullRoomNumber()
    {
        // Arrange
        var roomNumber = "1";

        // Act
        var room = new RoomNumber(roomNumber);

        // Assert
        Assert.IsNotNull(room);
    }
    
    [Test]
    public void TestEquals()
    {
        var roomNumber1 = new RoomNumber("1");
        var roomNumber2 = new RoomNumber("1");
        Assert.AreEqual(roomNumber1, roomNumber2);
    }

    [Test]
    public void TestNotEquals()
    {
        var roomNumber1 = new RoomNumber("1");
        var roomNumber2 = new RoomNumber("2");
        Assert.AreNotEqual(roomNumber1, roomNumber2);
    }

    [Test]
    public void TestEqualHashCodes()
    {
        // Arrange
        var roomNumber1 = new RoomNumber("1");
        var roomNumber2 = new RoomNumber("1");

        // Act
        var hashCode1 = roomNumber1.GetHashCode();
        var hashCode2 = roomNumber2.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
    }
    
    [Test]
    public void TestDifferentHashCodes()
    {
        // Arrange
        var roomNumber1 = new RoomNumber("1");
        var roomNumber2 = new RoomNumber("2");

        // Act
        var hashCode1 = roomNumber1.GetHashCode();
        var hashCode2 = roomNumber2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
    }
}