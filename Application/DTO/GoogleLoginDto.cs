using System.Collections.Generic;

namespace DDDNetCore.Application.DTO
{
    public class GoogleLoginDto
    {
        public string Email { get; set; }
        public string RequestUri { get; set; }
        public string AuthCode { get; set; }
        public string PatientName { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalRecordNumber { get; set; }
        public List<string> MedicalConditions { get; set; }
        public string EmergencyContact { get; set; }
        public List<string> AppointmentHistory { get; set; }
    }
}