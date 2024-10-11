
using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Patients;

namespace DDDNetCore.Application.DTO
{
    public class PatientDto
    {
        public string PatientName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> MedicalConditions { get; set; }
        public string EmergencyContact { get; set; }
        public List<string> AppointmentHistory { get; set; }
    }
}
