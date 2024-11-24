using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class PlanningDtoTests
{
    [Test]
    public void TestCreateIncompletePlanningDto()
    {
        // Arrange
        var dto = new PlanningDto()
        {
            RoomNumber = "1",
            Heuristic = "Test",
        };
        
        // Assert
        Assert.AreEqual(dto.RoomNumber, "1");
        Assert.AreEqual(dto.Heuristic, "Test");
    }
    
    [Test]
    public void TestCreateCompletePlanningDto()
    {
        // Arrange
        var dto = new PlanningDto()
        {
            RoomNumber = "1",
            Heuristic = "Test",
            Date = "2024-11-24",
            OperationRequests = "2,3,4"
        };
        
        // Assert
        Assert.AreEqual(dto.RoomNumber, "1");
        Assert.AreEqual(dto.Heuristic, "Test");
        Assert.AreEqual(dto.Date, "2024-11-24");
        Assert.AreEqual(dto.OperationRequests, "2,3,4");
    }
    
}