using System;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.Patients
{
    [TestFixture]
    public class PatientTests
    {
        private Mock<MedicalRecordNumber> _mockMedicalRecordNumber;
        private Mock<PatientName> _mockPatientName;
        private Mock<BirthDate> _mockBirthDate;
        private Mock<Gender> _mockGender;
        private Mock<PhoneNumber> _mockPhoneNumber;
        private Mock<EmergencyContact> _mockEmergencyContact;
        private Mock<AppointmentHistory> _mockAppointmentHistory;
        private Mock<UserEmail> _mockUserEmail;

        [SetUp]
        public void SetUp()
        {
            _mockMedicalRecordNumber = new Mock<MedicalRecordNumber>("202410000001");
            _mockPatientName = new Mock<PatientName>("Diana");
            _mockBirthDate = new Mock<BirthDate>("30 de Junho de 2004");
            _mockGender = new Mock<Gender>("Feminino");
            _mockPhoneNumber = new Mock<PhoneNumber>("938413938");
            _mockEmergencyContact = new Mock<EmergencyContact>("933264402");
            _mockAppointmentHistory = new Mock<AppointmentHistory>("02/04/2024");
            _mockUserEmail = new Mock<UserEmail>("1221194@isep.ipp.pt");
        }

        [Test]
        public void TestConstructor()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );

            Assert.AreEqual("Diana", patient.PatientName.Value);
            Assert.AreEqual("30 de Junho de 2004", patient.BirthDate.Value);
            Assert.AreEqual("Feminino", patient.Gender.Value);
            Assert.AreEqual("938413938", patient.PhoneNumber.Value);
            Assert.AreEqual("933264402", patient.EmergencyContact.Value);
            Assert.AreEqual("02/04/2024", patient.AppointmentHistory.Value);
        }

        [Test]
        public void TestChangePhoneNumber()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );
            Mock<PhoneNumber> mockPhoneNumber = new Mock<PhoneNumber>("933264402");

            patient.ChangePhoneNumber(mockPhoneNumber.Object);
        }

        [Test]
        public void TestChangePatientName()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );
            Mock<PatientName> mockPatientName = new Mock<PatientName>("Miguel");

            patient.ChangePatientName(mockPatientName.Object);
        }

        [Test]
        public void TestChangeEmergencyContact()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );
            Mock<EmergencyContact> mockEmergencyContact = new Mock<EmergencyContact>("933264402");

            patient.ChangeEmergencyContact(mockEmergencyContact.Object);
        }

        [Test]
        public void TestChangeBirthDate()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );
            Mock<BirthDate> mockBirthDate = new Mock<BirthDate>("3 de Janeiro de 2024");

            patient.ChangeBirthDate(mockBirthDate.Object);
        }

        [Test]
        public void TestChangeGender()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );
            Mock<Gender> mockGender = new Mock<Gender>("Masculino");

            patient.ChangeGender(mockGender.Object);
        }

        [Test]
        public void TestChangeAppointmentHistory()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory.Object,
                _mockUserEmail.Object
            );

            Mock<AppointmentHistory> mockAppointment = new Mock<AppointmentHistory>("03/04/2024");

            patient.ChangeAppointmentHistory(mockAppointment.Object);
        }

        [Test]
        public void TestPrivateConstructor()
        {
            var patient = (Patient)Activator.CreateInstance(typeof(Patient), true);

            Assert.NotNull(patient);
        }
    }
}