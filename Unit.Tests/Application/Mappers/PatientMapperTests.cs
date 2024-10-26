using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Mappers
{
    [TestFixture]
    public class PatientMapperTests
    {
        private PatientMapper _mapper;
        private Mock<MedicalRecordNumber> _mockMedicalRecordNumber;
        private Mock<PatientName> _mockPatientName;
        private Mock<BirthDate> _mockBirthDate;
        private Mock<Gender> _mockGender;
        private Mock<PhoneNumber> _mockPhoneNumber;
        private Mock<MedicalConditions> _mockMedicalConditions;
        private Mock<EmergencyContact> _mockEmergencyContact;
        private Mock<AppointmentHistory> _mockAppointmentHistory;
        private Mock<UserEmail> _mockUserEmail;

        [SetUp]
        public void Setup()
        {
            _mapper = new PatientMapper();
            _mockMedicalRecordNumber = new Mock<MedicalRecordNumber>("202410000001");
            _mockPatientName = new Mock<PatientName>("Diana");
            _mockBirthDate = new Mock<BirthDate>("30 de Junho de 2004");
            _mockGender = new Mock<Gender>("Feminino");
            _mockPhoneNumber = new Mock<PhoneNumber>("938413938");
            _mockMedicalConditions = new Mock<MedicalConditions>("Nurse");
            _mockEmergencyContact = new Mock<EmergencyContact>("933264402");
            _mockAppointmentHistory = new Mock<AppointmentHistory>("Nurse");
            _mockUserEmail = new Mock<UserEmail>("1221194@isep.ipp.pt");
        }

        [Test]
        public void TestToDomain()
        {
            var dto = new PatientDto()
            {
                PatientName = "Diana",
                BirthDate = "30 de Junho de 2004",
                Gender = "Feminino",
                MedicalRecordNumber = "202410000001",
                PhoneNumber = "938413938",
                MedicalConditions = new List<string>() { "Nurse" },
                EmergencyContact = "933264402",
                AppointmentHistory = new List<string>() { "Nurse" },
                Email = "1221194@isep.ipp.pt"
            };

            var medicalRecordNumber = new MedicalRecordNumber(dto.MedicalRecordNumber);
            var medicalConditions = dto.MedicalConditions.Select(rs => new MedicalConditions(rs)).ToList();
            var appointmentHistory = dto.AppointmentHistory.Select(rs => new AppointmentHistory(rs)).ToList();

            var patient = _mapper.ToDomain(dto, medicalRecordNumber, medicalConditions, appointmentHistory);

            Assert.AreEqual(dto.MedicalRecordNumber, patient.Id.AsString());
            Assert.AreEqual(dto.PatientName, patient.PatientName.ToString());
            Assert.AreEqual(dto.BirthDate, patient.BirthDate.ToString());
            Assert.AreEqual(dto.Gender, patient.Gender.ToString());
            Assert.AreEqual(dto.PhoneNumber, patient.PhoneNumber.ToString());
            Assert.AreEqual(dto.EmergencyContact, patient.EmergencyContact.ToString());
            Assert.AreEqual(dto.Email, patient.UserEmail.ToString());
            Assert.AreEqual(dto.MedicalConditions.First(), patient.MedicalConditions.First().MedicalConditionsValue);
            Assert.AreEqual(dto.AppointmentHistory.First(), patient.AppointmentHistory.First().AppointmentHistoryValue);
        }

        [Test]
        public void TestToDto()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                new List<MedicalConditions> { _mockMedicalConditions.Object },
                _mockEmergencyContact.Object,
                new List<AppointmentHistory> { _mockAppointmentHistory.Object },
                _mockUserEmail.Object
            );

            var dto = _mapper.ToDto(patient);

            Assert.AreEqual(_mockMedicalRecordNumber.Object.AsString(), dto.MedicalRecordNumber);
            Assert.AreEqual(_mockPatientName.Object.ToString(), dto.PatientName);
            Assert.AreEqual(_mockBirthDate.Object.ToString(), dto.BirthDate);
            Assert.AreEqual(_mockGender.Object.ToString(), dto.Gender);
            Assert.AreEqual(_mockPhoneNumber.Object.ToString(), dto.PhoneNumber);
            Assert.AreEqual(_mockEmergencyContact.Object.ToString(), dto.EmergencyContact);
            Assert.AreEqual(_mockUserEmail.Object.ToString(), dto.Email);
            Assert.AreEqual(_mockMedicalConditions.Object.MedicalConditionsValue, dto.MedicalConditions.First());
            Assert.AreEqual(_mockAppointmentHistory.Object.AppointmentHistoryValue, dto.AppointmentHistory.First());
        }

        [Test]
        public void TestToDtoList()
        {
            var patient = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                new List<MedicalConditions> { _mockMedicalConditions.Object },
                _mockEmergencyContact.Object,
                new List<AppointmentHistory> { _mockAppointmentHistory.Object },
                _mockUserEmail.Object
            );

            var dto = _mapper.ToDtoList(patient);

            Assert.AreEqual(_mockMedicalRecordNumber.Object.AsString(), dto.MedicalRecordNumber);
            Assert.AreEqual(_mockPatientName.Object.ToString(), dto.PatientName);
            Assert.AreEqual(_mockBirthDate.Object.ToString(), dto.BirthDate);
            Assert.AreEqual(_mockUserEmail.Object.ToString(), dto.Email);
        }

        [Test]
        public void TestToListDto()
        {
            var patient1 = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                new List<MedicalConditions> { _mockMedicalConditions.Object },
                _mockEmergencyContact.Object,
                new List<AppointmentHistory> { _mockAppointmentHistory.Object },
                _mockUserEmail.Object
            );

            var patient2 = new Patient(
                _mockPatientName.Object,
                _mockBirthDate.Object,
                _mockGender.Object,
                _mockMedicalRecordNumber.Object,
                _mockPhoneNumber.Object,
                new List<MedicalConditions> { _mockMedicalConditions.Object },
                _mockEmergencyContact.Object,
                new List<AppointmentHistory> { _mockAppointmentHistory.Object },
                _mockUserEmail.Object
            );

            var listPatients = new List<Patient>();
            listPatients.Add(patient1);
            listPatients.Add(patient2);

            var dto = _mapper.ToListDto(listPatients);

            Assert.AreEqual(2, dto.Count);
        }
    }
}