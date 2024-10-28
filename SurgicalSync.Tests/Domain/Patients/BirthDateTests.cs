using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class BirthDateTests
    {
        [Test]
        public void TestConstructor()
        {
            var birthDate = new BirthDate("30 de Junho de 2004");
            Assert.AreEqual("30 de Junho de 2004", birthDate.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var birthDate = new BirthDate("30 de Junho de 2004");
            Assert.AreEqual("30 de Junho de 2004", birthDate.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var birthDate1 = new BirthDate("30 de Junho de 2004");
            var birthDate2 = new BirthDate("30 de Junho de 2004");
            Assert.AreEqual(birthDate1, birthDate2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var birthDate1 = new BirthDate("30 de Junho de 2004");
            var birthDate2 = new BirthDate("30 de Junho de 2004");
            
            // Act
            var hashCode1 = birthDate1.GetHashCode();
            var hashCode2 = birthDate2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var birthDate1 = new BirthDate("30 de Junho de 2004");
            var birthDate2 = new BirthDate("12 de Novembro de 2004");

            // Act
            var hashCode1 = birthDate1.GetHashCode();
            var hashCode2 = birthDate2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var birthDate = (BirthDate)Activator.CreateInstance(typeof(BirthDate), true);

            Assert.NotNull(birthDate);
            Assert.IsNull(birthDate.Value);
        }
    }
}