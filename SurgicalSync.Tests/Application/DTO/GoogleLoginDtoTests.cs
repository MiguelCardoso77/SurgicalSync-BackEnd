using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO
{
    [TestFixture]
    public class GoogleLoginDtoTests
    {
        [Test]
        public void CreateIncompleteGoogleLoginDto()
        {
            var dto = new GoogleLoginDto()
            {
                Email = "email@email.com",
                RequestUri = "http://localhost:5000",
                PatientName = "John Doe",
                BirthDate = "01/01/2000"
            };

            Assert.AreEqual(dto.Email, "email@email.com");
            Assert.AreEqual(dto.RequestUri, "http://localhost:5000");
            Assert.AreEqual(dto.PatientName, "John Doe");
            Assert.AreEqual(dto.BirthDate, "01/01/2000");
        }

        [Test]
        public void CreateCompleteGoogleLoginDto()
        {
            var dto = new GoogleLoginDto()
            {
                Email = "email@email.com",
                RequestUri = "http://localhost:5000",
                AuthCode = "134567890",
                PatientName = "John Doe",
                BirthDate = "01/01/2000",
                PhoneNumber = "123456789",
                MedicalRecordNumber = "123456",
                EmergencyContact = "Jane Doe",
                MedicalConditions = new List<string>() { "Diabetes", "Hypertension" },
                AppointmentHistory = new List<string>() { "01/01/2020", "01/02/2020" }
            };

            Assert.AreEqual(dto.Email, "email@email.com");
            Assert.AreEqual(dto.RequestUri, "http://localhost:5000");
            Assert.AreEqual(dto.AuthCode, "134567890");
            Assert.AreEqual(dto.PatientName, "John Doe");
            Assert.AreEqual(dto.BirthDate, "01/01/2000");
            Assert.AreEqual(dto.PhoneNumber, "123456789");
            Assert.AreEqual(dto.MedicalRecordNumber, "123456");
            Assert.AreEqual(dto.EmergencyContact, "Jane Doe");
            Assert.AreEqual(dto.MedicalConditions, new List<string>() { "Diabetes", "Hypertension" });
            Assert.AreEqual(dto.AppointmentHistory, new List<string>() { "01/01/2020", "01/02/2020" });
        }
    }
}