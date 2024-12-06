using System;
using DDDNetCore.Domain.RoomTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.RoomTypes;

[TestFixture]
public class RoomTypeIdTests
{
    [Test]
    public void Constructor_ShouldSetIdValue_WhenValidIdIsProvided()
    {
        // Arrange
        var validId = "123-abc";

        // Act
        var roomTypeId = new RoomTypeId(validId);

        // Assert
        Assert.AreEqual(validId, roomTypeId.Value);
    }

    [Test]
    public void AsString_ShouldReturnCorrectValue()
    {
        // Arrange
        var id = "room-type-123";
        var roomTypeId = new RoomTypeId(id);

        // Act
        var result = roomTypeId.AsString();

        // Assert
        Assert.AreEqual(id, result);
    }

    [Test]
    public void createFromString_ShouldReturnSameValue()
    {
        // Arrange
        var id = "room-type-123";
        var roomTypeId = new RoomTypeId(id);

        // Act
        var result = roomTypeId.AsString();

        // Assert
        Assert.AreEqual(id, result);
    }

    [Test]
    public void Equals_ShouldReturnTrue_WhenIdsAreEqual()
    {
        // Arrange
        var id1 = new RoomTypeId("room-type-123");
        var id2 = new RoomTypeId("room-type-123");

        // Act
        var isEqual = id1.Equals(id2);

        // Assert
        Assert.IsTrue(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenIdsAreNotEqual()
    {
        // Arrange
        var id1 = new RoomTypeId("room-type-123");
        var id2 = new RoomTypeId("room-type-456");

        // Act
        var isEqual = id1.Equals(id2);

        // Assert
        Assert.IsFalse(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenComparedWithNull()
    {
        // Arrange
        var id1 = new RoomTypeId("room-type-123");

        // Act
        var isEqual = id1.Equals(null);

        // Assert
        Assert.IsFalse(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenComparedWithDifferentObjectType()
    {
        // Arrange
        var id1 = new RoomTypeId("room-type-123");
        var differentTypeObject = new object();

        // Act
        var isEqual = id1.Equals(differentTypeObject);

        // Assert
        Assert.IsFalse(isEqual);
    }

    [Test]
    public void GetHashCode_ShouldReturnSameHashCode_ForEqualIds()
    {
        // Arrange
        var id1 = new RoomTypeId("room-type-123");
        var id2 = new RoomTypeId("room-type-123");

        // Act
        var hashCode1 = id1.GetHashCode();
        var hashCode2 = id2.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode2);
    }

    [Test]
    public void GetHashCode_ShouldReturnDifferentHashCodes_ForDifferentIds()
    {
        // Arrange
        var id1 = new RoomTypeId("room-type-123");
        var id2 = new RoomTypeId("room-type-456");

        // Act
        var hashCode1 = id1.GetHashCode();
        var hashCode2 = id2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2);
    }
}