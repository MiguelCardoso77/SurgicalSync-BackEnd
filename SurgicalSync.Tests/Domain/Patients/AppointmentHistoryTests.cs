using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class AppointmentHistoryTests
    {
        [Test]
        public void TestConstructor()
        {
            var appointmentHistory = new AppointmentHistory("Surgeon");
            Assert.AreEqual("Surgeon", appointmentHistory.AppointmentHistoryValue);
        }
        
        [Test]
        public void TestToString()
        {
            var appointmentHistory = new AppointmentHistory("Surgeon");
            Assert.AreEqual("Surgeon", appointmentHistory.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var appointmentHistory1 = new AppointmentHistory("Surgeon");
            var appointmentHistory2 = new AppointmentHistory("Surgeon");
            Assert.AreEqual(appointmentHistory1, appointmentHistory2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var appointmentHistory1 = new AppointmentHistory("Surgeon");
            var appointmentHistory2 = new AppointmentHistory("Surgeon");
            
            // Act
            var hashCode1 = appointmentHistory1.GetHashCode();
            var hashCode2 = appointmentHistory2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var appointmentHistory1 = new AppointmentHistory("Surgeon");
            var appointmentHistory2 = new AppointmentHistory("Nurse");

            // Act
            var hashCode1 = appointmentHistory1.GetHashCode();
            var hashCode2 = appointmentHistory2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var appointmentHistory = (AppointmentHistory)Activator.CreateInstance(typeof(AppointmentHistory), true);

            Assert.NotNull(appointmentHistory);
            Assert.IsNull(appointmentHistory.AppointmentHistoryValue);
        }
    }
}