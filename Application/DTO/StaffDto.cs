using System.Collections.Generic;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) for staff member details.
     * This class is used to transfer staff information between processes.
     */
    public class StaffDto
    {
        /**
         * Gets or sets the unique identifier for the staff member.
         */
        public string Id { get; set; }

        /**
         * Gets or sets the name of the staff member.
         */
        public string StaffName { get; set; }

        /**
         * Gets or sets the email address of the staff member.
         */
        public string UserEmail { get; set; }

        /**
         * Gets or sets the phone number of the staff member.
         */
        public string StaffPhoneNumber { get; set; }

        /**
         * Gets or sets the specialization of the staff member.
         */
        public string StaffSpecialization { get; set; }

        /**
         * Gets or sets the list of availability slots for the staff member.
         */
        public string StaffAvaiabilitySlots { get; set; }

        /**
         * Gets or sets the type of the staff member (e.g., Doctor, Nurse).
         */
        public string StaffType { get; set; }

        /**
         * Gets or sets a value indicating whether the staff member is active.
         */
        public bool isActive { get; set; }

        /**
         * Gets or sets the license number of the staff member.
         */
        public string StaffLicenseNumber { get; set; }
    }

    /**
     * Data Transfer Object (DTO) for basic staff member details.
     * This class is used to transfer a simplified set of staff information.
     */
    public class StaffDtoList
    {
        /**
         * Gets or sets the unique identifier for the staff member.
         */
        public string Id { get; set; }

        /**
         * Gets or sets the name of the staff member.
         */
        public string StaffName { get; set; }

        /**
         * Gets or sets the email address of the staff member.
         */
        public string UserEmail { get; set; }

        /**
         * Gets or sets the specialization of the staff member.
         */
        public string StaffSpecialization { get; set; }
    }
}