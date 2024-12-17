using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class AvailableMaterialsDtoTests
{
    [Test]
    public void TestIncompleteDto()
    {
        var dto = new AvailableMaterialsDTO();
        Assert.IsNull(dto.Staff);
        Assert.IsNull(dto.Rooms);
    }

    [Test]
    public void TestCompleteDto()
    {
        var dto = new AvailableMaterialsDTO
        {
            Staff = "Nurses",
            Rooms = "1,2"
        };

        Assert.AreEqual("Nurses", dto.Staff);
        Assert.AreEqual("1,2", dto.Rooms);
    }
}