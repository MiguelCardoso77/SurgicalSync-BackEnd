using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class MedicalConditionsTests
    {
        [Test]
        public void TestConstructor()
        {
            var medicalConditions = new MedicalConditions("Asma");
            Assert.AreEqual("Asma", medicalConditions.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var medicalConditions = new MedicalConditions("Asma");
            Assert.AreEqual("Asma", medicalConditions.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var medicalConditions1 = new MedicalConditions("Asma");
            var medicalConditions2 = new MedicalConditions("Asma");
            Assert.AreEqual(medicalConditions1, medicalConditions2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var medicalConditions1 = new MedicalConditions("Surgeon");
            var medicalConditions2 = new MedicalConditions("Surgeon");
            
            // Act
            var hashCode1 = medicalConditions1.GetHashCode();
            var hashCode2 = medicalConditions2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var medicalConditions1 = new MedicalConditions("Surgeon");
            var medicalConditions2 = new MedicalConditions("Nurse");

            // Act
            var hashCode1 = medicalConditions1.GetHashCode();
            var hashCode2 = medicalConditions2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var medicalConditions = (MedicalConditions)Activator.CreateInstance(typeof(MedicalConditions), true);

            Assert.NotNull(medicalConditions);
            Assert.IsNull(medicalConditions.Value);
        }
    }
}