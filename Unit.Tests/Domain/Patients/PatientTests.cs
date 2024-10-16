using System.Collections.Generic;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class PatientTests
    {
        [Test]
        public void TestConstructor()
        {
            List<MedicalConditions> medicalConditions = new List<MedicalConditions>();
            medicalConditions.Add(new MedicalConditions("asma"));
            medicalConditions.Add(new MedicalConditions("papo na testa"));

            List<AppointmentHistory> appointmentHistories = new List<AppointmentHistory>();
            appointmentHistories.Add(new AppointmentHistory("3 de junho de 2012"));
            appointmentHistories.Add(new AppointmentHistory("4 de setembro de 2013"));

            var patient = new Patient(
                new PatientName("Miguel"),
                new BirthDate("4 de Julho de 2004"),
                new Gender("masculino"),
                new MedicalRecordNumber("1000"),
                new PhoneNumber("930983821"),
                medicalConditions,
                new EmergencyContact("983457634"),
                appointmentHistories
            );

            Assert.AreEqual("1000", patient.Id.AsString());
            Assert.AreEqual("Miguel", patient.PatientName.PatientNameValue);
            Assert.AreEqual("4 de Julho de 2004", patient.BirthDate.BirthDateValue);
            Assert.AreEqual("masculino", patient.Gender.GenderValue);
            Assert.AreEqual("983457634", patient.EmergencyContact.EmergencyContactValue);
            Assert.AreEqual("930983821", patient.PhoneNumber.PhoneNumberValue);
            Assert.AreEqual(2, patient.MedicalConditions.Count);
            Assert.AreEqual(2, patient.AppointmentHistory.Count);
        }
    }
}