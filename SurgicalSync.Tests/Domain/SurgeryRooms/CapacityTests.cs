using DDDNetCore.Domain.SurgeryRooms;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.SurgeryRooms;

[TestFixture]
public class CapacityTests
{
    [Test]
    public void TestConstructor()
    {
        var capacity = new Capacity("6,2");
        Assert.AreEqual(8, capacity.Value);
    }

    [Test]
    public void TestToString()
    {
        var capacity = new Capacity("6,2");
        Assert.AreEqual("Max Patients: 6, Max Staff: 2", capacity.ToString());
    }

    [Test]
    public void TestEquals()
    {
        var capacity1 = new Capacity("6,2");
        var capacity2 = new Capacity("6,2");
        Assert.AreEqual(capacity1, capacity2);
    }

    [Test]
    public void TestNotEquals()
    {
        var capacity1 = new Capacity("6,2");
        var capacity2 = new Capacity("2,2");
        Assert.AreNotEqual(capacity1, capacity2);
    }

    [Test]
    public void TestEqualHashCodes()
    {
        // Arrange
        var capacity1 = new Capacity("6,2");
        var capacity2 = new Capacity("6,2");

        // Act
        var hashCode1 = capacity1.GetHashCode();
        var hashCode2 = capacity2.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
    }

    [Test]
    public void TestDifferentHashCodes()
    {
        // Arrange
        var capacity1 = new Capacity("6,2");
        var capacity2 = new Capacity("4,2");

        // Act
        var hashCode1 = capacity1.GetHashCode();
        var hashCode2 = capacity2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
    }
}