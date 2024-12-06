using System;
using DDDNetCore.Domain.RoomTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.RoomTypes;

[TestFixture]
public class RoomTypeDescriptionTests
{
    [Test]
    public void Constructor_ShouldSetDescriptionValue()
    {
        // Arrange
        var description = "Test Room Type";

        // Act
        var roomTypeDescription = new RoomTypeDescription(description);

        // Assert
        Assert.AreEqual(description, roomTypeDescription.Value);
    }
    
    [Test]
    public void ToString_ShouldReturnDescriptionValue()
    {
        // Arrange
        var description = "Test Room Type";
        var roomTypeDescription = new RoomTypeDescription(description);

        // Act
        var result = roomTypeDescription.ToString();

        // Assert
        Assert.AreEqual(description, result);
    }

    [Test]
    public void Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        var description1 = new RoomTypeDescription("Test Room Type");
        var description2 = new RoomTypeDescription("Test Room Type");

        // Act
        var isEqual = description1.Equals(description2);

        // Assert
        Assert.True(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        // Arrange
        var description1 = new RoomTypeDescription("Test Room Type");
        var description2 = new RoomTypeDescription("Another Room Type");

        // Act
        var isEqual = description1.Equals(description2);

        // Assert
        Assert.False(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenComparingWithNull()
    {
        // Arrange
        var description = new RoomTypeDescription("Test Room Type");

        // Act
        var isEqual = description.Equals(null);

        // Assert
        Assert.False(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenComparingWithDifferentObjectType()
    {
        // Arrange
        var description = new RoomTypeDescription("Test Room Type");
        var differentTypeObject = new object();

        // Act
        var isEqual = description.Equals(differentTypeObject);

        // Assert
        Assert.False(isEqual);
    }

    [Test]
    public void GetHashCode_ShouldReturnSameHashCode_ForEqualValues()
    {
        // Arrange
        var description1 = new RoomTypeDescription("Test Room Type");
        var description2 = new RoomTypeDescription("Test Room Type");

        // Act
        var hashCode1 = description1.GetHashCode();
        var hashCode2 = description2.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode2);
    }

    [Test]
    public void GetHashCode_ShouldReturnDifferentHashCodes_ForDifferentValues()
    {
        // Arrange
        var description1 = new RoomTypeDescription("Test Room Type");
        var description2 = new RoomTypeDescription("Another Room Type");

        // Act
        var hashCode1 = description1.GetHashCode();
        var hashCode2 = description2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2);
    }
    
    [Test]
    public void TestPrivateConstructor()
    {
        var roomTypeDescription = (RoomTypeDescription)Activator.CreateInstance(typeof(RoomTypeDescription), true);

        Assert.NotNull(roomTypeDescription);
        Assert.IsNull(roomTypeDescription.Value);
    }
    
}