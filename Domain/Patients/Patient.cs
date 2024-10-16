using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot
    {
        public MedicalRecordNumber Id { get; private set; }
        public PatientName PatientName { get; private set; }
        public BirthDate BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public List<MedicalConditions> MedicalConditions { get; private set; }
        public EmergencyContact EmergencyContact { get; private set; }
        public List<AppointmentHistory> AppointmentHistory { get; private set; }
        public UserEmail UserEmail { get; private set; }

        private Patient()
        {
            this.PatientName = null;
            this.PhoneNumber = null;
            this.MedicalConditions = null;
            this.EmergencyContact = null;
            this.BirthDate = null;
            this.Gender = null;
            this.AppointmentHistory = null;
            this.UserEmail = null;
        }

        public Patient(PatientName patientName, BirthDate birthDate, Gender gender,
            MedicalRecordNumber medicalRecordNumber, PhoneNumber phoneNumber, List<MedicalConditions> medicalConditions,
            EmergencyContact emergencyContact, List<AppointmentHistory> appointmentHistory, UserEmail userEmail)
        {
            this.PatientName = patientName;
            this.BirthDate = birthDate;
            this.Gender = gender;
            this.Id = medicalRecordNumber;
            this.PhoneNumber = phoneNumber;
            this.MedicalConditions = medicalConditions;
            this.EmergencyContact = emergencyContact;
            this.AppointmentHistory = appointmentHistory;
            this.UserEmail = userEmail;
        }
        
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
        
        public void ChangePatientName(PatientName patientName)
        {
            this.PatientName = patientName;
        }
        
        public void ChangeEmergencyContact(EmergencyContact emergencyContact)
        {
            this.EmergencyContact = emergencyContact;
        }
        
        public void ChangeBirthDate(BirthDate birthDate)
        {
            this.BirthDate = birthDate;
        }
        public void ChangeGender(Gender gender)
        {
            this.Gender = gender;
        }

        public void ChangeEmail(UserEmail email)
        {
            this.UserEmail = email;
        }
    }
}