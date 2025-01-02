using System;
using DDDNetCore.Domain.Specializations;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Specializations
{
    [TestFixture]
    public class SpecializationCodeTests
    {
        private string _validCodeValue;
        private SpecializationCode _specializationCode;

        [SetUp]
        public void SetUp()
        {
            _validCodeValue = "SPEC1234";
            _specializationCode = new SpecializationCode(_validCodeValue);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(_validCodeValue, _specializationCode.AsString());
        }

        [Test]
        public void TestCreateFromString()
        {
            var specializationCode = new SpecializationCode("SPEC5678");
            Assert.AreEqual("SPEC5678", specializationCode.AsString());
        }

        [Test]
        public void TestAsString()
        {
            Assert.AreEqual(_validCodeValue, _specializationCode.AsString());
        }
        
    }
}