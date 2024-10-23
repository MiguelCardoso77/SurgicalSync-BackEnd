using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.Patients
{
    [TestFixture]
    public class GenderTests
    {
        [Test]
        public void TestConstructor()
        {
            var gender = new Gender("masculino");
            Assert.AreEqual("masculino", gender.GenderValue);
        }
        
        [Test]
        public void TestToString()
        {
            var gender = new Gender("masculino");
            Assert.AreEqual("masculino", gender.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var gender1 = new Gender("masculino");
            var gender2 = new Gender("masculino");
            Assert.AreEqual(gender1, gender2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var gender1 = new Gender("30 de Junho de 2004");
            var gender2 = new Gender("30 de Junho de 2004");
            
            // Act
            var hashCode1 = gender1.GetHashCode();
            var hashCode2 = gender2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var gender1 = new Gender("30 de Junho de 2004");
            var gender2 = new Gender("12 de Novembro de 2004");

            // Act
            var hashCode1 = gender1.GetHashCode();
            var hashCode2 = gender2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var gender = (Gender)Activator.CreateInstance(typeof(Gender), true);

            Assert.NotNull(gender);
            Assert.IsNull(gender.GenderValue);
        }
    }
}