using System;
using DDDNetCore.Domain.Appointments;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Appointments;

[TestFixture]
public class TimeTests
{
    [Test]
    public void TestConstructor()
    {
        // Arrange
        var time = new Time(130);

        // Act
        var result = time.Value;
        
        // Assert
        Assert.AreEqual(130, result);
    }
    
    [Test]
    public void TestConstructorWithInvalidMinutes()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Time(0, 60));
    }
    
    [Test]
    public void TestConstructorWithInvalidHours()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Time(24, 0));
    }
    
    [Test]
    public void TestConstructorWithInvalidMinutesAndHours()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Time(24, 60));
    }
    
    [Test]
    public void TestToString()
    {
        // Arrange
        var time = new Time(130);

        // Act
        var result = time.ToString();
        
        // Assert
        Assert.AreEqual("02:10", result);
    }
    
    [Test]
    public void TestEquals()
    {
        // Arrange
        var time1 = new Time(130);
        var time2 = new Time(130);

        // Act
        var result = time1.Equals(time2);
        
        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestEqualsWithDifferentTime()
    {
        // Arrange
        var time1 = new Time(130);
        var time2 = new Time(140);

        // Act
        var result = time1.Equals(time2);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void TestEqualsWithDifferentType()
    {
        // Arrange
        var time = new Time(130);

        // Act
        var result = time.Equals("test");
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void TestGetHashCode()
    {
        // Arrange
        var time = new Time(130);

        // Act
        var result = time.GetHashCode();
        
        // Assert
        Assert.AreEqual(130, result);
    }
    
    [Test]
    public void TestCompareTo()
    {
        // Arrange
        var time1 = new Time(130);
        var time2 = new Time(140);

        // Act
        var result = time1.CompareTo(time2);
        
        // Assert
        Assert.AreEqual(-1, result);
    }
    
    [Test]
    public void TestCompareToWithEqualTime()
    {
        // Arrange
        var time1 = new Time(130);
        var time2 = new Time(130);

        // Act
        var result = time1.CompareTo(time2);
        
        // Assert
        Assert.AreEqual(0, result);
    }
}