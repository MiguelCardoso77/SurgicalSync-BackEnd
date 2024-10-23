using System.Threading.Tasks;
using DDDNetCore.Application.DTO;

namespace DDDNetCore.Application.Services
{
    public class PatientMicroService
    {
        private readonly PatientService _service;

        public PatientMicroService(PatientService service)
        {
            this._service = service;
        }
        
        public async Task<PatientDto> CreatePatientProfile(GoogleLoginDto dto)
        {
            var patientDto = new PatientDto
            {
                Email = dto.Email,
                PatientName = dto.PatientName,
                BirthDate = dto.BirthDate,
                PhoneNumber = dto.PhoneNumber,
                MedicalRecordNumber = dto.MedicalRecordNumber,
                MedicalConditions = dto.MedicalConditions,
                EmergencyContact = dto.EmergencyContact,
                AppointmentHistory = dto.AppointmentHistory
            };
                
            await _service.AddAsync(patientDto);
            
            return patientDto;
        }
    }
}