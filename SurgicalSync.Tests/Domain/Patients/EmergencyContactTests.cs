using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class EmergencyContactTests
    {
        [Test]
        public void TestConstructor()
        {
            var emergencyContact = new EmergencyContact("934118398");
            Assert.AreEqual("934118398", emergencyContact.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var emergencyContact = new EmergencyContact("934118398");
            Assert.AreEqual("934118398", emergencyContact.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var emergencyContact1 = new EmergencyContact("934118398");
            var emergencyContact2 = new EmergencyContact("934118398");
            Assert.AreEqual(emergencyContact1, emergencyContact2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var emergencyContact1 = new EmergencyContact("30 de Junho de 2004");
            var emergencyContact2 = new EmergencyContact("30 de Junho de 2004");
            
            // Act
            var hashCode1 = emergencyContact1.GetHashCode();
            var hashCode2 = emergencyContact2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var emergencyContact1 = new EmergencyContact("30 de Junho de 2004");
            var emergencyContact2 = new EmergencyContact("12 de Novembro de 2004");

            // Act
            var hashCode1 = emergencyContact1.GetHashCode();
            var hashCode2 = emergencyContact2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var emergencyContact = (EmergencyContact)Activator.CreateInstance(typeof(EmergencyContact), true);

            Assert.NotNull(emergencyContact);
            Assert.IsNull(emergencyContact.Value);
        }
    }
}