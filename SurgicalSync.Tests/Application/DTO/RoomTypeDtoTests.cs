using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class RoomTypeDtoTests
{
    [Test]
    public void TestCreateIncompletePlanningDto()
    {
        // Arrange
        var dto = new RoomTypeDto()
        {
            RoomTypeCode = "123--321",
            RoomTypeDescription = "Surgery"
        };
        
        // Assert
        Assert.AreEqual(dto.RoomTypeCode, "123--321");
        Assert.AreEqual(dto.RoomTypeDescription, "Surgery");
    }
    
    [Test]
    public void TestCreateCompletePlanningDto()
    {
        // Arrange
        var dto = new RoomTypeDto()
        {
            RoomTypeCode = "123--321",
            RoomTypeDesignation = "ICU",
            RoomTypeDescription = "Surgery"
        };
        
        // Assert
        Assert.AreEqual(dto.RoomTypeCode, "123--321");
        Assert.AreEqual(dto.RoomTypeDesignation, "ICU");
        Assert.AreEqual(dto.RoomTypeDescription, "Surgery");
    }
}