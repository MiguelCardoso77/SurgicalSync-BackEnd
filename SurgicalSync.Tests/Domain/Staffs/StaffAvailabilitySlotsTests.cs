using System;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Staffs
{
    [TestFixture]
    public class StaffAvailabilitySlotsTests
    {
        [Test]
        public void TestConstructor()
        {
            var avaiabilitySlots = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");
            Assert.AreEqual("2024-09-25:14h00-18h00", avaiabilitySlots.Value);
        }

        [Test]
        public void TestToString()
        {
            var avaiabilitySlots = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");
            Assert.AreEqual("2024-09-25:14h00-18h00", avaiabilitySlots.ToString());
        }

        [Test]
        public void TestEquals()
        {
            var avaiabilitySlots1 = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");
            var avaiabilitySlots2 = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");
            Assert.AreEqual(avaiabilitySlots1, avaiabilitySlots2);
        }

        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var avaiabilitySlots1 = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");
            var avaiabilitySlots2 = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");

            // Act
            var hashCode1 = avaiabilitySlots1.GetHashCode();
            var hashCode2 = avaiabilitySlots2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }

        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var avaiabilitySlots1 = new StaffAvailabilitySlots("2024-09-25:14h00-18h00");
            var avaiabilitySlots2 = new StaffAvailabilitySlots("2024-09-26:15h00-18h00");

            // Act
            var hashCode1 = avaiabilitySlots1.GetHashCode();
            var hashCode2 = avaiabilitySlots2.GetHashCode();


            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var avaiabilitySlots = (StaffAvailabilitySlots)Activator.CreateInstance(typeof(StaffAvailabilitySlots), true);

            Assert.NotNull(avaiabilitySlots);
            Assert.IsNull(avaiabilitySlots.Value);
        }
    }
}