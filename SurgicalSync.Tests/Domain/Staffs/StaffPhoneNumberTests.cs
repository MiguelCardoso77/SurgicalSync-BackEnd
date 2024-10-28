using System;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Staffs
{
    [TestFixture]
    public class StaffPhoneNumberTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffPhoneNumber = new StaffPhoneNumber("934568742");
            Assert.AreEqual("934568742", staffPhoneNumber.Value);
        }

        [Test]
        public void TestToString()
        {
            var staffPhoneNumber = new StaffPhoneNumber("934568742");
            Assert.AreEqual("934568742", staffPhoneNumber.ToString());
        }

        [Test]
        public void TestEquals()
        {
            var staffPhoneNumber1 = new StaffPhoneNumber("934568742");
            var staffPhoneNumber2 = new StaffPhoneNumber("934568742");
            Assert.AreEqual(staffPhoneNumber1, staffPhoneNumber2);
        }

        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var staffPhoneNumber1 = new StaffPhoneNumber("934568742");
            var staffPhoneNumber2 = new StaffPhoneNumber("934568742");

            // Act
            var hashCode1 = staffPhoneNumber1.GetHashCode();
            var hashCode2 = staffPhoneNumber2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }

        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var staffPhoneNumber1 = new StaffPhoneNumber("934568742");
            var staffPhoneNumber2 = new StaffPhoneNumber("931111111");

            // Act
            var hashCode1 = staffPhoneNumber1.GetHashCode();
            var hashCode2 = staffPhoneNumber2.GetHashCode();


            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var staffPhoneNumber = (StaffPhoneNumber)Activator.CreateInstance(typeof(StaffPhoneNumber), true);

            Assert.NotNull(staffPhoneNumber);
            Assert.IsNull(staffPhoneNumber.Value);
        }
    }
}