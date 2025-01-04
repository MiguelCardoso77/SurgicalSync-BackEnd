using System;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Specializations;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Specializations
{
    [TestFixture]
    public class SpecializationTests
    {
        private Mock<SpecializationCode> _mockCode;
        private Mock<SpecializationDesignation> _mockDesignation;
        private Mock<SpecializationDescription> _mockDescription;

        [SetUp]
        public void SetUp()
        {
            _mockCode = new Mock<SpecializationCode>("SPEC1234");
            _mockDesignation = new Mock<SpecializationDesignation>("Cardiology");
            _mockDescription = new Mock<SpecializationDescription>("A specialization for heart-related issues.");
        }

        
        [Test]
        public void TestConstructor_ShouldThrowArgumentNullException_WhenCodeIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new Specialization(null, _mockDesignation.Object, _mockDescription.Object)
            );
        }

        [Test]
        public void TestConstructor_ShouldThrowArgumentNullException_WhenDesignationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new Specialization(_mockCode.Object, null, _mockDescription.Object)
            );
        }

        [Test]
        public void TestConstructor_ShouldSetDefaultDescription_WhenDescriptionIsNull()
        {
            var specialization = new Specialization(
                _mockCode.Object,
                _mockDesignation.Object,
                null // Passando null para Description
            );

            // Verifica se Description é null, pois queremos que seja null, conforme a lógica definida
            Assert.IsNull(specialization.Description);
        }


        [Test]
        public void TestChangeDesignation()
        {
            var specialization = new Specialization(
                _mockCode.Object,
                _mockDesignation.Object,
                _mockDescription.Object
            );

            Mock<SpecializationDesignation> newDesignation = new Mock<SpecializationDesignation>("Neurology");
            specialization.ChangeDesignation(newDesignation.Object);

            Assert.AreEqual("Neurology", specialization.Designation.Value);
        }

        [Test]
        public void TestChangeDescription()
        {
            var specialization = new Specialization(
                _mockCode.Object,
                _mockDesignation.Object,
                _mockDescription.Object
            );

            Mock<SpecializationDescription> newDescription = new Mock<SpecializationDescription>("A specialization for brain and nervous system issues.");
            specialization.ChangeDescription(newDescription.Object);

            Assert.AreEqual("A specialization for brain and nervous system issues.", specialization.Description.Value);
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var specialization = (Specialization)Activator.CreateInstance(typeof(Specialization), true);

            Assert.NotNull(specialization);
            Assert.IsNull(specialization.Id);
            Assert.IsNull(specialization.Designation);
            Assert.IsNull(specialization.Description);
        }
    }
}
