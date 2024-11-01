using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO
{
    [TestFixture]
    public class PatientDtoTests
    {
        [Test]
        public void TestCreateIncompletePatientDto()
        {
            var dto = new PatientDto()
            {
                MedicalRecordNumber = "202410000001",
                PatientName = "Diana"
            };

            Assert.AreEqual(dto.MedicalRecordNumber, "202410000001");
            Assert.AreEqual(dto.PatientName, "Diana");
        }

        [Test]
        public void TestCreateCompletePatientDto()
        {
            var dto = new PatientDto()
            {
                MedicalRecordNumber = "202410000001",
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Gender = "Feminino",
                PhoneNumber = "938413938",
                EmergencyContact = "933264402",
                MedicalConditions = "Asma",
                AppointmentHistory = null,
                Email = "1221194@isep.ipp.pt"
            };

            Assert.AreEqual(dto.MedicalRecordNumber, "202410000001");
            Assert.AreEqual(dto.PatientName, "Diana");
            Assert.AreEqual(dto.BirthDate, "30 de Junho de 2004");
            Assert.AreEqual(dto.Gender, "Feminino");
            Assert.AreEqual(dto.PhoneNumber, "938413938");
            Assert.AreEqual(dto.EmergencyContact, "933264402");
            Assert.AreEqual(dto.MedicalConditions, "Asma");
            Assert.AreEqual(dto.AppointmentHistory, null);
            Assert.AreEqual(dto.Email, "1221194@isep.ipp.pt");
        }

        [Test]
        public void TestCreateIncompletePatientListDto()
        {
            var dto = new PatientListDto()
            {
                MedicalRecordNumber = "202410000001",
                PatientName = "Diana"
            };

            Assert.AreEqual(dto.MedicalRecordNumber, "202410000001");
            Assert.AreEqual(dto.PatientName, "Diana");
        }

        [Test]
        public void TestCreateCompletePatientListDto()
        {
            var dto = new PatientListDto()
            {
                MedicalRecordNumber = "202410000001",
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Email = "1221194@isep.ipp.pt"
            };

            Assert.AreEqual(dto.MedicalRecordNumber, "202410000001");
            Assert.AreEqual(dto.PatientName, "Diana");
            Assert.AreEqual(dto.BirthDate, "30 de Junho de 2004");
            Assert.AreEqual(dto.Email, "1221194@isep.ipp.pt");
        }
    }
}