using System;
using System.Collections.Generic;
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
        private List<MedicalConditions> _mockMedicalConditions;
        private Mock<EmergencyContact> _mockEmergencyContact;
        private List<AppointmentHistory> _mockAppointmentHistory;
        private Mock<UserEmail> _mockUserEmail;

        [SetUp]
        public void SetUp()
        {
            _mockMedicalRecordNumber = new Mock<MedicalRecordNumber>("202410000001");
            _mockPatientName = new Mock<PatientName>("Diana");
            _mockBirthDate = new Mock<BirthDate>("30 de Junho de 2004");
            _mockGender = new Mock<Gender>("Feminino");
            _mockPhoneNumber = new Mock<PhoneNumber>("938413938");
            _mockMedicalConditions = new List<MedicalConditions>();
            _mockEmergencyContact = new Mock<EmergencyContact>("933264402");
            _mockAppointmentHistory =  new List<AppointmentHistory>();
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
                _mockUserEmail.Object
            );

            Assert.AreEqual("Diana", patient.PatientName.Value);
            Assert.AreEqual("30 de Junho de 2004", patient.BirthDate.Value);
            Assert.AreEqual("Feminino", patient.Gender.Value);
            Assert.AreEqual("938413938", patient.PhoneNumber.Value);
            Assert.AreEqual("933264402", patient.EmergencyContact.Value);
            Assert.AreEqual(0, patient.MedicalConditions.Count);
            Assert.AreEqual(0, patient.AppointmentHistory.Count);
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
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
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
                _mockUserEmail.Object
            );
            List<AppointmentHistory> mockAppointmentHistory = new List<AppointmentHistory>();
            
            Mock<AppointmentHistory> mockAppointment1 = new Mock<AppointmentHistory>("3 de Julho de 2023");
            Mock<AppointmentHistory> mockAppointment2 = new Mock<AppointmentHistory>("2 de Novembro de 2023");
            Mock<AppointmentHistory> mockAppointment3 = new Mock<AppointmentHistory>("17 de Fevereiro de 2024");

            mockAppointmentHistory.Add(mockAppointment1.Object);
            mockAppointmentHistory.Add(mockAppointment2.Object);
            mockAppointmentHistory.Add(mockAppointment3.Object);
            
            patient.ChangeAppointmentHistory(mockAppointmentHistory);
        }
        
        [Test]
        public void TestChangeMedicalConditions()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                _mockMedicalConditions,
                _mockEmergencyContact.Object,
                _mockAppointmentHistory,
                _mockUserEmail.Object
            );
            List<MedicalConditions> mockMedicalConditions = new List<MedicalConditions>();
            
            Mock<MedicalConditions> mockMedicalConditions1 = new Mock<MedicalConditions>("Asma");
            Mock<MedicalConditions> mockMedicalConditions2 = new Mock<MedicalConditions>("Escoliose");
            Mock<MedicalConditions> mockMedicalConditions3 = new Mock<MedicalConditions>("Alergia ao Fiambre");

            mockMedicalConditions.Add(mockMedicalConditions1.Object);
            mockMedicalConditions.Add(mockMedicalConditions2.Object);
            mockMedicalConditions.Add(mockMedicalConditions3.Object);
            
            patient.ChangeMedicalConditions(mockMedicalConditions);
        }
        
        [Test]
        public void TestPrivateConstructor()
        {
            var patient = (Patient)Activator.CreateInstance(typeof(Patient), true);

            Assert.NotNull(patient);
            Assert.IsNull(patient.MedicalConditions);
            Assert.IsNull(patient.AppointmentHistory);
        }
    }
}