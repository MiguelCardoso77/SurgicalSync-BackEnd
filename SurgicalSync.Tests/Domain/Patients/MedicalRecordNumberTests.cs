using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class MedicalRecordNumberTests
    {
        [Test]
        public void TestConstructor()
        {
            var medicalRecordNumber = new MedicalRecordNumber("1");
            Assert.AreEqual("1", medicalRecordNumber.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var medicalRecordNumber = new MedicalRecordNumber("1");
            Assert.AreEqual("1", medicalRecordNumber.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var medicalRecordNumber1 = new MedicalRecordNumber("1");
            var medicalRecordNumber2 = new MedicalRecordNumber("1");
            Assert.AreEqual(medicalRecordNumber1, medicalRecordNumber2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var medicalRecordNumber1 = new MedicalRecordNumber("1");
            var medicalRecordNumber2 = new MedicalRecordNumber("1");
            
            // Act
            var hashCode1 = medicalRecordNumber1.GetHashCode();
            var hashCode2 = medicalRecordNumber2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var medicalRecordNumber1 = new MedicalRecordNumber("1");
            var medicalRecordNumber2 = new MedicalRecordNumber("2");

            // Act
            var hashCode1 = medicalRecordNumber1.GetHashCode();
            var hashCode2 = medicalRecordNumber2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var medicalRecordNumber = (MedicalRecordNumber)Activator.CreateInstance(typeof(MedicalRecordNumber), true);

            Assert.AreEqual("202411000001",medicalRecordNumber.ToString());
            Assert.AreEqual(medicalRecordNumber.Value, medicalRecordNumber.ToString());
        }
    }
}