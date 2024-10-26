using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a patient in the system, encapsulating personal details and medical history.
     *
     * The Patient class serves as an aggregate root within the domain, providing methods
     * for modifying the patient's details while maintaining the integrity of the patient's
     * information. It contains properties such as name, birth date, gender, contact information,
     * medical conditions, emergency contacts, appointment history, and user email.
     */
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

        /**
         * Private constructor for the Patient class, used for ORM purposes.
         * Initializes all properties to null.
         */
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

        /**
         * Initializes a new instance of the Patient class with specified personal details and medical history.
         *
         * @param patientName The name of the patient.
         * @param birthDate The birth date of the patient.
         * @param gender The gender of the patient.
         * @param medicalRecordNumber The unique medical record number of the patient.
         * @param phoneNumber The phone number of the patient.
         * @param medicalConditions A list of the patient's medical conditions.
         * @param emergencyContact The emergency contact information for the patient.
         * @param appointmentHistory A list of the patient's appointment history.
         * @param userEmail The email address of the patient.
         */
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

        /**
         * Changes the phone number of the patient.
         *
         * @param phoneNumber The new phone number to set for the patient.
         */
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

        /**
         * Changes the name of the patient.
         *
         * @param patientName The new name to set for the patient.
         */
        public void ChangePatientName(PatientName patientName)
        {
            this.PatientName = patientName;
        }

        /**
         * Changes the emergency contact information for the patient.
         *
         * @param emergencyContact The new emergency contact to set for the patient.
         */
        public void ChangeEmergencyContact(EmergencyContact emergencyContact)
        {
            this.EmergencyContact = emergencyContact;
        }

        /**
         * Changes the birth date of the patient.
         *
         * @param birthDate The new birth date to set for the patient.
         */
        public void ChangeBirthDate(BirthDate birthDate)
        {
            this.BirthDate = birthDate;
        }

        /**
         * Changes the gender of the patient.
         *
         * @param gender The new gender to set for the patient.
         */
        public void ChangeGender(Gender gender)
        {
            this.Gender = gender;
        }

        /**
         * Changes the appointment history of the patient.
         *
         * @param appointmentHistory The new appointment history to set for the patient.
         */
        public void ChangeAppointmentHistory(List<AppointmentHistory> appointmentHistory)
        {
            this.AppointmentHistory = appointmentHistory;
        }

        /**
         * Changes the medical conditions of the patient.
         *
         * @param medicalConditions The new medical conditions to set for the patient.
         */
        public void ChangeMedicalConditions(List<MedicalConditions> medicalConditions)
        {
            this.MedicalConditions = medicalConditions;
        }
    }
}