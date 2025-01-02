using System;
using DDDNetCore.Domain.Specializations;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Specializations
{
    [TestFixture]
    public class SpecializationDescriptionTests
    {
        private string _descriptionValue;
        private SpecializationDescription _specializationDescription;

        [SetUp]
        public void SetUp()
        {
            _descriptionValue = "A specialization for heart-related issues.";
            _specializationDescription = new SpecializationDescription(_descriptionValue);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(_descriptionValue, _specializationDescription.ToString());
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual(_descriptionValue, _specializationDescription.ToString());
        }

        [Test]
        public void TestEquals_ShouldReturnTrue_WhenDescriptionsAreEqual()
        {
            var otherDescription = new SpecializationDescription("A specialization for heart-related issues.");
            Assert.IsTrue(_specializationDescription.Equals(otherDescription));
        }

        [Test]
        public void TestEquals_ShouldReturnFalse_WhenDescriptionsAreNotEqual()
        {
            var otherDescription = new SpecializationDescription("A specialization for brain-related issues.");
            Assert.IsFalse(_specializationDescription.Equals(otherDescription));
        }

        [Test]
        public void TestEquals_ShouldReturnFalse_WhenComparedWithNull()
        {
            Assert.IsFalse(_specializationDescription.Equals(null));
        }

        [Test]
        public void TestEquals_ShouldReturnFalse_WhenComparedWithDifferentType()
        {
            Assert.IsFalse(_specializationDescription.Equals("Some String"));
        }

        [Test]
        public void TestGetHashCode()
        {
            var descriptionHashCode = _specializationDescription.GetHashCode();
            Assert.AreEqual(_descriptionValue.GetHashCode(), descriptionHashCode);
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var specializationDescription = (SpecializationDescription)Activator.CreateInstance(typeof(SpecializationDescription), true);
            Assert.NotNull(specializationDescription);
        }
    }
}
