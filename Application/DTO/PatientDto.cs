using System.Collections.Generic;

namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) representing detailed information of a patient.
     */
    public class PatientDto
    {
        /**
         * Full name of the patient.
         */
        public string PatientName { get; set; }

        /**
         * Birth date of the patient in YYYY-MM-DD format.
         */
        public string BirthDate { get; set; }

        /**
         * Gender of the patient.
         */
        public string Gender { get; set; }

        /**
         * Unique medical record number assigned to the patient.
         */
        public string MedicalRecordNumber { get; set; }

        /**
         * Contact phone number of the patient.
         */
        public string PhoneNumber { get; set; }
        
        /**
         * Emergency contact information for the patient.
         */
        public string EmergencyContact { get; set; }

        /**
         * List of past appointment dates or details.
         */
        public string AppointmentHistory { get; set; }

        /**
         * Email address of the patient.
         */
        public string Email { get; set; }
    }

    /**
     * Data Transfer Object (DTO) representing basic information of a patient in a list view.
     */
    public class PatientListDto
    {
        /**
         * Full name of the patient.
         */
        public string PatientName { get; set; }

        /**
         * Birth date of the patient in YYYY-MM-DD format.
         */
        public string BirthDate { get; set; }

        /**
         * Unique medical record number assigned to the patient.
         */
        public string MedicalRecordNumber { get; set; }

        /**
         * Email address of the patient.
         */
        public string Email { get; set; }
    }
}