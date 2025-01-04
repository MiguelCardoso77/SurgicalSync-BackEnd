using System;
using DDDNetCore.Domain.Specializations;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Specializations
{
    [TestFixture]
    public class SpecializationDesignationTests
    {
        private string _validDesignationValue;
        private SpecializationDesignation _specializationDesignation;

        [SetUp]
        public void SetUp()
        {
            _validDesignationValue = "Cardiology";
            _specializationDesignation = new SpecializationDesignation(_validDesignationValue);
        }

        [Test]
        public void TestConstructor_ShouldSetDesignationCorrectly()
        {
            Assert.AreEqual(_validDesignationValue, _specializationDesignation.ToString());
        }

        [Test]
        public void TestConstructor_ShouldThrowFormatException_WhenDesignationIsNullOrEmpty()
        {
            Assert.Throws<FormatException>(() => new SpecializationDesignation(null));
            Assert.Throws<FormatException>(() => new SpecializationDesignation(string.Empty));
            Assert.Throws<FormatException>(() => new SpecializationDesignation("   "));
        }

        [Test]
        public void TestConstructor_ShouldThrowFormatException_WhenDesignationIsTooLong()
        {
            var longDesignation = new string('A', 101);
            Assert.Throws<FormatException>(() => new SpecializationDesignation(longDesignation));
        }

        [Test]
        public void TestToString_ShouldReturnDesignation()
        {
            Assert.AreEqual(_validDesignationValue, _specializationDesignation.ToString());
        }

        [Test]
        public void TestEquals_ShouldReturnTrue_WhenDesignationsAreEqual()
        {
            var otherDesignation = new SpecializationDesignation("Cardiology");
            Assert.IsTrue(_specializationDesignation.Equals(otherDesignation));
        }

        [Test]
        public void TestEquals_ShouldReturnFalse_WhenDesignationsAreNotEqual()
        {
            var otherDesignation = new SpecializationDesignation("Neurology");
            Assert.IsFalse(_specializationDesignation.Equals(otherDesignation));
        }

        [Test]
        public void TestEquals_ShouldReturnFalse_WhenComparedWithNull()
        {
            Assert.IsFalse(_specializationDesignation.Equals(null));
        }

        [Test]
        public void TestEquals_ShouldReturnFalse_WhenComparedWithDifferentType()
        {
            Assert.IsFalse(_specializationDesignation.Equals("Some String"));
        }

        [Test]
        public void TestGetHashCode_ShouldReturnHashCodeOfValue()
        {
            var designationHashCode = _specializationDesignation.GetHashCode();
            Assert.AreEqual(_validDesignationValue.GetHashCode(), designationHashCode);
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var specializationDesignation = (SpecializationDesignation)Activator.CreateInstance(typeof(SpecializationDesignation), true);
            Assert.NotNull(specializationDesignation);
        }
    }
}
