using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO
{
    [TestFixture]
    public class SpecializationDtoTests
    {
        [Test]
        public void TestCreateIncompleteSpecializationDto()
        {
            var dto = new SpecializationDto
            {
                code = "1",
                designation = "Cardiology"
            };

            Assert.AreEqual(dto.code, "1");
            Assert.AreEqual(dto.designation, "Cardiology");
            Assert.IsNull(dto.description);
        }

        [Test]
        public void TestCreateCompleteSpecializationDto()
        {
            var dto = new SpecializationDto
            {
                code = "2",
                designation = "Neurology",
                description = "Specialization in neurological disorders."
            };

            Assert.AreEqual(dto.code, "2");
            Assert.AreEqual(dto.designation, "Neurology");
            Assert.AreEqual(dto.description, "Specialization in neurological disorders.");
        }

        [Test]
        public void TestModifySpecializationDto()
        {
            var dto = new SpecializationDto
            {
                code = "3",
                designation = "Oncology",
                description = "Specialization in cancer treatment."
            };

            // Modifying properties
            dto.designation = "Updated Oncology";
            dto.description = "Updated description for oncology.";

            Assert.AreEqual(dto.code, "3");
            Assert.AreEqual(dto.designation, "Updated Oncology");
            Assert.AreEqual(dto.description, "Updated description for oncology.");
        }
    }
}