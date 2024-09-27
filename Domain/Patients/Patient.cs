using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class Patient : Entity<PatientId>, IAggregateRoot
    {
        public string PatientName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Gender { get; private set; }
        private List<string> MedicalConditions { get; }
        public string EmergencyContact { get; private set; }
        public string BirthDate { get; private set; }

        private Patient()
        {
            this.PatientName = "";
            this.PhoneNumber = "";
            this.MedicalConditions = new List<string>();
            this.EmergencyContact = "";
            this.BirthDate = "";
            this.Gender = "";
        }
        
        public Patient(string patientName, string phoneNumber, List<string> medicalConditions, string emergencyContact, string birthDate, string gender)
        {
            this.Id = new PatientId(patientName);
            this.PatientName = patientName;
            this.PhoneNumber = phoneNumber;
            this.MedicalConditions = medicalConditions;
            this.EmergencyContact = emergencyContact;
            this.BirthDate = birthDate;
            this.Gender = gender;
        }
        
        public Patient(string patientName, string phoneNumber, string emergencyContact, string birthDate, string gender)
        {
            this.Id = new PatientId(patientName);
            this.PatientName = patientName;
            this.PhoneNumber = phoneNumber;
            this.MedicalConditions = new List<string>();
            this.EmergencyContact = emergencyContact;
            this.BirthDate = birthDate;
            this.Gender = gender;
        }
        
        public void ChangePhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
        
        public void AddMedicalCondition(string medicalCondition)
        {
            this.MedicalConditions.Add(medicalCondition);
        }
        
        public void RemoveMedicalCondition(string medicalCondition)
        {
            this.MedicalConditions.Remove(medicalCondition);
        }
        
        public void ChangeEmergencyContact(string emergencyContact)
        {
            this.EmergencyContact = emergencyContact;
        }
        
        public void ChangeBirthDate(string birthDate)
        {
            this.BirthDate = birthDate;
        }
    }
}