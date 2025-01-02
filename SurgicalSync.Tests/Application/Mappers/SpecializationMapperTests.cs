using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Specializations;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Mappers
{
    [TestFixture]
    public class SpecializationMapperTests
    {
        private SpecializationMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = new SpecializationMapper();
        }

        [Test]
        public void TestToDto()
        {
            var specialization = new Specialization(
                new SpecializationCode("SP001"),
                new SpecializationDesignation("Cardiology"),
                new SpecializationDescription("Focus on heart-related issues.")
            );

            var dto = _mapper.ToDto(specialization);

            Assert.AreEqual("SP001", dto.code);
            Assert.AreEqual("Cardiology", dto.designation);
            Assert.AreEqual("Focus on heart-related issues.", dto.description);
        }

        [Test]
        public void TestToDomain()
        {
            var dto = new SpecializationDto
            {
                code = "SP002",
                designation = "Neurology",
                description = "Focus on brain-related issues."
            };

            var specialization = _mapper.ToDomain(dto);

            Assert.AreEqual("SP002", specialization.Id.AsString());
            Assert.AreEqual("Neurology", specialization.Designation.Value);
            Assert.AreEqual("Focus on brain-related issues.", specialization.Description.Value);
        }

        [Test]
        public void TestToDomain_WithNullDescription()
        {
            var dto = new SpecializationDto
            {
                code = "SP003",
                designation = "Oncology",
                description = null
            };

            var specialization = _mapper.ToDomain(dto);

            Assert.AreEqual("SP003", specialization.Id.AsString());
            Assert.AreEqual("Oncology", specialization.Designation.Value);
            Assert.IsNull(specialization.Description);
        }

        [Test]
        public void TestToListDto()
        {
            var specializations = new List<Specialization>
            {
                new Specialization(
                    new SpecializationCode("SP004"),
                    new SpecializationDesignation("Pediatrics"),
                    new SpecializationDescription("Focus on children healthcare.")
                ),
                new Specialization(
                    new SpecializationCode("SP005"),
                    new SpecializationDesignation("Dermatology"),
                    new SpecializationDescription("Focus on skin issues.")
                )
            };

            var dtos = _mapper.ToListDto(specializations);

            Assert.AreEqual(2, dtos.Count);
            Assert.AreEqual("SP004", dtos[0].code);
            Assert.AreEqual("Pediatrics", dtos[0].designation);
            Assert.AreEqual("Focus on children healthcare.", dtos[0].description);

            Assert.AreEqual("SP005", dtos[1].code);
            Assert.AreEqual("Dermatology", dtos[1].designation);
            Assert.AreEqual("Focus on skin issues.", dtos[1].description);
        }
    }
}
 