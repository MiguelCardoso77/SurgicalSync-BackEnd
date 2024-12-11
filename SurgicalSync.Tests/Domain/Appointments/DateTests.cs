using System;
using DDDNetCore.Domain.Appointments;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Appointments;

[TestFixture]
public class DateTests
{
    [Test]
    public void Constructor_WithFutureDate_ShouldSetDateCorrectly()
    {
        var futureDate = DateTime.Now.AddDays(1);
        var date = new Date(futureDate);
        Assert.AreEqual(futureDate.ToString("yyyyMMdd"), date.Value);
    }

    [Test]
    public void Constructor_WithPastDate_ShouldThrowArgumentException()
    {
        var pastDate = DateTime.Now.AddDays(-1);
        Assert.Throws<ArgumentException>(() => new Date(pastDate));
    }

    [Test]
    public void ToString_ShouldReturnFormattedDate()
    {
        var futureDate = new DateTime(2025, 12, 25);
        var date = new Date(futureDate);
        Assert.AreEqual("20251225", date.ToString());
    }

    [Test]
    public void Equals_WithSameDate_ShouldReturnTrue()
    {
        var date1 = new Date(new DateTime(2025, 12, 25));
        var date2 = new Date(new DateTime(2025, 12, 25));

        Assert.IsTrue(date1.Equals(date2));
    }

    [Test]
    public void Equals_WithDifferentDate_ShouldReturnFalse()
    {
        var date1 = new Date(DateTime.Now.AddDays(1));
        var date2 = new Date(DateTime.Now.AddDays(2));

        Assert.IsFalse(date1.Equals(date2));
    }
    
    [Test]
    public void GetHashCode_WithSameDate_ShouldReturnSameHashCode()
    {
        var date1 = new Date(new DateTime(2025, 12, 25));
        var date2 = new Date(new DateTime(2025, 12, 25));

        Assert.AreEqual(date1.GetHashCode(), date2.GetHashCode());
    }
    
    [Test]
    public void TestPrivateConstructor()
    {
        var date = (Date)Activator.CreateInstance(typeof(Date), true);

        Assert.NotNull(date);
    }
}
