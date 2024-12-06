using System;
using DDDNetCore.Domain.RoomTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.RoomTypes;

[TestFixture]
public class RoomTypeDesignationTests
{
    [Test]
    public void Constructor_ShouldSetDesignationValue_WhenInputIsValid()
    {
        // Arrange
        var designation = "Valid Room Type";

        // Act
        var roomTypeDesignation = new RoomTypeDesignation(designation);

        // Assert
        Assert.AreEqual(designation, roomTypeDesignation.Value);
    }

    [Test]
    public void Constructor_ShouldThrowFormatException_WhenDesignationIsNull()
    {
        // Arrange
        string designation = null;

        // Act & Assert
        Assert.Throws<FormatException>(() =>
            new RoomTypeDesignation(designation),
            "Room type designation must be a non-empty string with less than 100 characters."
        );
    }

    [Test]
    public void Constructor_ShouldThrowFormatException_WhenDesignationIsEmpty()
    {
        // Arrange
        var designation = "";

        // Act & Assert
        Assert.Throws<FormatException>(() =>
            new RoomTypeDesignation(designation),
            "Room type designation must be a non-empty string with less than 100 characters."
        );
    }

    [Test]
    public void Constructor_ShouldThrowFormatException_WhenDesignationExceedsMaxLength()
    {
        // Arrange
        var designation = new string('A', 100); // 100 characters, which is invalid

        // Act & Assert
        Assert.Throws<FormatException>(() =>
            new RoomTypeDesignation(designation),
            "Room type designation must be a non-empty string with less than 100 characters."
        );
    }

    [Test]
    public void ToString_ShouldReturnDesignationValue()
    {
        // Arrange
        var designation = "Valid Room Type";
        var roomTypeDesignation = new RoomTypeDesignation(designation);

        // Act
        var result = roomTypeDesignation.ToString();

        // Assert
        Assert.AreEqual(designation, result);
    }

    [Test]
    public void Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        var designation1 = new RoomTypeDesignation("Valid Room Type");
        var designation2 = new RoomTypeDesignation("Valid Room Type");

        // Act
        var isEqual = designation1.Equals(designation2);

        // Assert
        Assert.IsTrue(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        // Arrange
        var designation1 = new RoomTypeDesignation("Valid Room Type");
        var designation2 = new RoomTypeDesignation("Different Room Type");

        // Act
        var isEqual = designation1.Equals(designation2);

        // Assert
        Assert.IsFalse(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenComparingWithNull()
    {
        // Arrange
        var designation = new RoomTypeDesignation("Valid Room Type");

        // Act
        var isEqual = designation.Equals(null);

        // Assert
        Assert.IsFalse(isEqual);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenComparingWithDifferentObjectType()
    {
        // Arrange
        var designation = new RoomTypeDesignation("Valid Room Type");
        var differentTypeObject = new object();

        // Act
        var isEqual = designation.Equals(differentTypeObject);

        // Assert
        Assert.IsFalse(isEqual);
    }

    [Test]
    public void GetHashCode_ShouldReturnSameHashCode_ForEqualValues()
    {
        // Arrange
        var designation1 = new RoomTypeDesignation("Valid Room Type");
        var designation2 = new RoomTypeDesignation("Valid Room Type");

        // Act
        var hashCode1 = designation1.GetHashCode();
        var hashCode2 = designation2.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode2);
    }

    [Test]
    public void GetHashCode_ShouldReturnDifferentHashCodes_ForDifferentValues()
    {
        // Arrange
        var designation1 = new RoomTypeDesignation("Valid Room Type");
        var designation2 = new RoomTypeDesignation("Different Room Type");

        // Act
        var hashCode1 = designation1.GetHashCode();
        var hashCode2 = designation2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2);
    }
    
    [Test]
    public void TestPrivateConstructor()
    {
        var roomTypeDesignation = (RoomTypeDesignation)Activator.CreateInstance(typeof(RoomTypeDesignation), true);

        Assert.NotNull(roomTypeDesignation);
        Assert.IsNull(roomTypeDesignation.Value);
    }
}