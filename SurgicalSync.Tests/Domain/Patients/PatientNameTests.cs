using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class PatientNameTests
    {
        [Test]
        public void TestConstructor()
        {
            var patientName = new PatientName("Diana");
            Assert.AreEqual("Diana", patientName.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var patientName = new PatientName("Diana");
            Assert.AreEqual("Diana", patientName.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var patientName1 = new PatientName("Diana");
            var patientName2 = new PatientName("Diana");
            Assert.AreEqual(patientName1, patientName2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var patientName1 = new PatientName("30 de Junho de 2004");
            var patientName2 = new PatientName("30 de Junho de 2004");
            
            // Act
            var hashCode1 = patientName1.GetHashCode();
            var hashCode2 = patientName2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var patientName1 = new PatientName("30 de Junho de 2004");
            var patientName2 = new PatientName("12 de Novembro de 2004");

            // Act
            var hashCode1 = patientName1.GetHashCode();
            var hashCode2 = patientName2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var patientName = (PatientName)Activator.CreateInstance(typeof(PatientName), true);

            Assert.NotNull(patientName);
            Assert.IsNull(patientName.Value);
        }
    }
}