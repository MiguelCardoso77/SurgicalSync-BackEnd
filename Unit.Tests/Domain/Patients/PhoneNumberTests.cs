using System;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.Patients
{
    [TestFixture]
    public class PhoneNumberTests
    {
        [Test]
        public void TestConstructor()
        {
            var phoneNumber = new PhoneNumber("934568742");
            Assert.AreEqual("934568742", phoneNumber.PhoneNumberValue);
        }
        
        [Test]
        public void TestToString()
        {
            var phoneNumber = new PhoneNumber("934568742");
            Assert.AreEqual("934568742", phoneNumber.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var phoneNumber1 = new PhoneNumber("934568742");
            var phoneNumber2 = new PhoneNumber("934568742");
            Assert.AreEqual(phoneNumber1, phoneNumber2);
        }
        
        [Test]
        public void TestEqualHashCodes()
        {
            // Arrange
            var phoneNumber1 = new PhoneNumber("30 de Junho de 2004");
            var phoneNumber2 = new PhoneNumber("30 de Junho de 2004");
            
            // Act
            var hashCode1 = phoneNumber1.GetHashCode();
            var hashCode2 = phoneNumber2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
        }
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var phoneNumber1 = new PhoneNumber("30 de Junho de 2004");
            var phoneNumber2 = new PhoneNumber("12 de Novembro de 2004");

            // Act
            var hashCode1 = phoneNumber1.GetHashCode();
            var hashCode2 = phoneNumber2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var phoneNumber = (PhoneNumber)Activator.CreateInstance(typeof(PhoneNumber), true);

            Assert.NotNull(phoneNumber);
            Assert.IsNull(phoneNumber.PhoneNumberValue);
        }
    }
}