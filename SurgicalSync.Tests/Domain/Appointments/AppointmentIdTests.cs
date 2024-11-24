using DDDNetCore.Domain.Appointments;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Appointments;

[TestFixture]
public class AppointmentIdTests
{
    [Test]
    public void TestConstructor()
    {
        var id = new AppointmentId("1");
        Assert.AreEqual("1", id.Value);
    }
    
    [Test]
    public void TestAsString()
    {
        var id = new AppointmentId("1");
        Assert.AreEqual("1", id.AsString());
    }
    
    [Test]
    public void TestEquals()
    {
        var id1 = new AppointmentId("1");
        var id2 = new AppointmentId("1");
        Assert.AreEqual(id1, id2);
    }
    
    [Test]
    public void TestNotEquals()
    {
        var id1 = new AppointmentId("1");
        var id2 = new AppointmentId("2");
        Assert.AreNotEqual(id1, id2);
    }
}