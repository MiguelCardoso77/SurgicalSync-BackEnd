using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Mappers
{
    /**
    * Mapper class for converting between Patient domain objects and data transfer objects (DTOs).
    */
    public class PatientMapper
    {
        /**
         * Maps a Patient domain object to a PatientDto containing detailed patient information.
         *
         * @param domain The Patient domain object.
         * @return A PatientDto object containing detailed information about the patient.
         */
        public PatientDto ToDto(Patient domain)
        {
            return new PatientDto
            {
                PatientName = domain.PatientName.ToString(),
                BirthDate = domain.BirthDate.ToString(),
                Gender = domain.Gender.ToString(),
                MedicalRecordNumber = domain.Id.AsString(),
                PhoneNumber = domain.PhoneNumber.ToString(),
                MedicalConditions = domain.MedicalConditions.Select(rs => rs.MedicalConditionsValue).ToList(),
                EmergencyContact = domain.EmergencyContact.ToString(),
                AppointmentHistory = domain.AppointmentHistory.Select(rs => rs.AppointmentHistoryValue).ToList(),
                Email = domain.UserEmail.ToString()
            };
        }
        
        /**
         * Maps a Patient domain object to a PatientListDto containing basic patient information.
         *
         * @param domain The Patient domain object.
         * @return A PatientListDto object containing basic information about the patient.
         */
        public PatientListDto ToDtoList(Patient domain)
        {
            return new PatientListDto
            {
                PatientName = domain.PatientName.ToString(),
                BirthDate = domain.BirthDate.ToString(),
                MedicalRecordNumber = domain.Id.AsString(),
                Email = domain.UserEmail.ToString()
            };
        }

        /**
         * Maps a PatientDto to a Patient domain object.
         *
         * @param dto The PatientDto containing data to be mapped.
         * @param medicalRecordNumber The patient's unique medical record number.
         * @param medicalConditions A list of medical conditions associated with the patient.
         * @param appointmentHistory A list of the patient's past appointments.
         * @return A Patient domain object created from the DTO data.
         */
        public Patient ToDomain(PatientDto dto, MedicalRecordNumber medicalRecordNumber,
            List<MedicalConditions> medicalConditions,
            List<AppointmentHistory> appointmentHistory)
        {
            return new Patient(
                new PatientName(dto.PatientName),
                new BirthDate(dto.BirthDate),
                new Gender(dto.Gender),
                medicalRecordNumber,
                new PhoneNumber(dto.PhoneNumber),
                medicalConditions,
                new EmergencyContact(dto.EmergencyContact), appointmentHistory,
                new UserEmail(dto.Email)
            );
        }

        /**
         * Maps a list of Patient domain objects to a list of PatientListDto objects.
         *
         * @param filteredPatientsList The list of Patient domain objects.
         * @return A list of PatientListDto objects containing basic information about each patient.
         */
        public List<PatientListDto> ToListDto(List<Patient> filteredPatientsList)
        {
            return filteredPatientsList.Select(patient => ToDtoList(patient)).ToList();
        }
    }
}