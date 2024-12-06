using System;
using DDDNetCore.Domain.RoomTypes;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.RoomTypes;

[TestFixture]
public class RoomTypeTests
{
    private Mock<RoomTypeId> _mockRoomTypeId;
    private Mock<RoomTypeDesignation> _mockRoomTypeDesignation;
    private Mock<RoomTypeDescription> _mockRoomTypeDescription;
    
    [SetUp]
    public void SetUp()
    {
        _mockRoomTypeId = new Mock<RoomTypeId>("123--321");
        _mockRoomTypeDesignation = new Mock<RoomTypeDesignation>("Operating Room");
        _mockRoomTypeDescription = new Mock<RoomTypeDescription>("A room for surgeries");
    }
    
    [Test]
    public void Constructor_ShouldCreateRoomType_WhenValidParametersAreProvided()
    {
        // Act
        var roomType = new RoomType(
            _mockRoomTypeId.Object,
            _mockRoomTypeDesignation.Object,
            _mockRoomTypeDescription.Object);

        // Assert
        Assert.IsNotNull(roomType);
        Assert.AreEqual(_mockRoomTypeId.Object, roomType.Id);
        Assert.AreEqual(_mockRoomTypeDesignation.Object, roomType.Designation);
        Assert.AreEqual(_mockRoomTypeDescription.Object, roomType.Description);
    }

    [Test]
    public void Constructor_ShouldThrowException_WhenIdIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new RoomType(null, _mockRoomTypeDesignation.Object, _mockRoomTypeDescription.Object));
    }

    [Test]
    public void Constructor_ShouldThrowException_WhenDesignationIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new RoomType(_mockRoomTypeId.Object, null, _mockRoomTypeDescription.Object));
    }

    [Test]
    public void Constructor_ShouldAssignDefaultDescription_WhenDescriptionIsNull()
    {
        // Act
        var roomType = new RoomType(_mockRoomTypeId.Object, _mockRoomTypeDesignation.Object, null);

        // Assert
        Assert.IsNotNull(roomType);
        Assert.AreEqual("No description available.", roomType.Description.Value);
    }

    [Test]
    public void ChangeDesignation_ShouldUpdateDesignation()
    {
        // Arrange
        var roomType = new RoomType(
            _mockRoomTypeId.Object,
            _mockRoomTypeDesignation.Object,
            _mockRoomTypeDescription.Object);
        var newDesignation = new Mock<RoomTypeDesignation>("Recovery Room");

        // Act
        roomType.ChangeDesignation(newDesignation.Object);

        // Assert
        Assert.AreEqual(newDesignation.Object, roomType.Designation);
    }

    [Test]
    public void ChangeDescription_ShouldUpdateDescription()
    {
        // Arrange
        var roomType = new RoomType(
            _mockRoomTypeId.Object,
            _mockRoomTypeDesignation.Object,
            _mockRoomTypeDescription.Object);
        var newDescription = new Mock<RoomTypeDescription>("A room for recovery after surgery");

        // Act
        roomType.ChangeDescription(newDescription.Object);

        // Assert
        Assert.AreEqual(newDescription.Object, roomType.Description);
    }
    
    [Test]
    public void TestPrivateConstructor()
    {
        var roomType = (RoomType)Activator.CreateInstance(typeof(RoomType), true);

        Assert.NotNull(roomType);
    }
}