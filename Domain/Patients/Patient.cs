using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot
    {
        public PatientName PatientName { get; private set; }
        public BirthDate BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public MedicalRecordNumber MedicalRecordNumber { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public List<MedicalConditions> MedicalConditions { get; private set; }
        public EmergencyContact EmergencyContact { get; private set; }
        public List<AppointmentHistory> AppointmentHistory { get; private set; }

        private Patient()
        {
            this.PatientName = null;
            this.PhoneNumber = null;
            this.MedicalConditions = null;
            this.EmergencyContact = null;
            this.BirthDate = null;
            this.Gender = null;
            this.AppointmentHistory = null;
            this.MedicalRecordNumber = null;
        }

        public Patient(PatientName patientName, BirthDate birthDate, Gender gender,
            MedicalRecordNumber medicalRecordNumber, PhoneNumber phoneNumber, List<MedicalConditions> medicalConditions,
            EmergencyContact emergencyContact, List<AppointmentHistory> appointmentHistory)
        {
            this.PatientName = patientName;
            this.BirthDate = birthDate;
            this.Gender = gender;
            this.MedicalRecordNumber = medicalRecordNumber;
            this.PhoneNumber = phoneNumber;
            this.MedicalConditions = medicalConditions;
            this.EmergencyContact = emergencyContact;
            this.AppointmentHistory = appointmentHistory;
        }
    }
}