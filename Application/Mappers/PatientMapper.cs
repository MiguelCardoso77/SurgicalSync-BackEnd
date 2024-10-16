using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Mappers
{
    public class PatientMapper
    {
        public static PatientDto ToDto(Patient domain)
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

        public static Patient ToDomain(PatientDto dto, MedicalRecordNumber medicalRecordNumber, List<MedicalConditions> medicalConditions,
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
    }
}
