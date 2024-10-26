using System;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.Staffs
{
    public class StaffLicenseNumberTests
    {
        
        [Test]
        public void TestConstructor()
        {
            var staffLicenseNumber = new StaffLicenseNumber("Raquel Gonçalves");
            Assert.AreEqual("Raquel Gonçalves", staffLicenseNumber.StaffLicenseNumberValue);
        }
        
        [Test]
        public void TestToString()
        {
            var staffLicenseNumber = new StaffLicenseNumber("Raquel Gonçalves");
            Assert.AreEqual("Raquel Gonçalves", staffLicenseNumber.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffLicenseNumber1 = new StaffLicenseNumber("Raquel Gonçalves");
            var staffLicenseNumber2 = new StaffLicenseNumber("Raquel Gonçalves");
            Assert.AreEqual(staffLicenseNumber1, staffLicenseNumber2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            var staffLicenseNumber1 = new StaffLicenseNumber("Raquel Gonçalves");
            var staffLicenseNumber2 = new StaffLicenseNumber("Raquel Gonçalves");
            // Act
            var hashCode1 = staffLicenseNumber1.GetHashCode();
            var hashCode2 = staffLicenseNumber2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            var staffLicenseNumber1 = new StaffLicenseNumber("Raquel Gonçalves");
            var staffLicenseNumber2 = new StaffLicenseNumber("Tomás Gonçalves");
            // Act
            var hashCode1 = staffLicenseNumber1.GetHashCode();
            var hashCode2 = staffLicenseNumber2.GetHashCode();


            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var staffLicenseNumber = (StaffLicenseNumber)Activator.CreateInstance(typeof(StaffLicenseNumber), true);

            Assert.NotNull(staffLicenseNumber);
            Assert.IsNull(staffLicenseNumber.StaffLicenseNumberValue);
        }
    }
}