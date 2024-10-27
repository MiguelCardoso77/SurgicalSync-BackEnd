using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using NUnit.Framework.Internal.Execution;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents a Staff entity in the system.
     * Staff entities are aggregate roots responsible for representing employees, including their
     * personal information, specialization, availability, and state (active/inactive).
     */
    public class Staff : Entity<StaffId>, IAggregateRoot
    {
        public StaffId Id { get; private set; }
        public StaffName StaffName { get; private set; }
        public UserEmail UserEmail { get; private set; }
        public StaffPhoneNumber StaffPhoneNumber { get; private set; }
        public StaffSpecialization StaffSpecialization { get; private set; }
        public List<StaffAvaiabilitySlots> StaffAvaiabilitySlots { get; private set; }
        public bool IsActive { get; set; }
        public StaffType StaffType { get; private set; }
        public StaffLicenseNumber StaffLicenseNumber { get; private set; }

        /**
         * Default constructor.
         * This constructor is used for ORM and serialization purposes. It initializes the entity with default values.
         */
        private Staff()
        {
            this.Id = null;
            this.StaffName = null;
            this.UserEmail = null;
            this.StaffPhoneNumber = null;
            this.StaffSpecialization = StaffSpecialization.None;
            this.StaffAvaiabilitySlots = null;
            this.StaffType = StaffType.Other;
            this.IsActive = true;
            this.StaffLicenseNumber = null;
        }

        /**
         * Constructs a new Staff entity with the provided values.
         *
         * @param id The unique identifier for the staff member.
         * @param staffName The name of the staff member.
         * @param userEmail The email address of the staff member.
         * @param staffPhoneNumber The phone number of the staff member.
         * @param staffSpecialization The specialization of the staff member.
         * @param staffAvaiabilitySlots The list of availability slots for the staff member.
         * @param staffType The type of the staff member.
         * @param isActive The active status of the staff member.
         * @param staffLicenseNumber The license number of the staff member.
         */
        public Staff(StaffId id, StaffName staffName, UserEmail userEmail, StaffPhoneNumber staffPhoneNumber,
            StaffSpecialization staffSpecialization, List<StaffAvaiabilitySlots> staffAvaiabilitySlots,
            StaffType staffType, Boolean isActive,
            StaffLicenseNumber staffLicenseNumber)
        {
            this.Id = id;
            this.StaffName = staffName;
            this.UserEmail = userEmail;
            this.StaffPhoneNumber = staffPhoneNumber;
            this.StaffSpecialization = staffSpecialization;
            this.StaffAvaiabilitySlots = staffAvaiabilitySlots;
            this.StaffType = staffType;
            this.IsActive = isActive;
            this.StaffLicenseNumber = staffLicenseNumber;
        }

        /**
         * Changes the specialization of the staff member.
         *
         * @param specialization The new specialization to be set.
         */
        public void ChangeStaffSpecialization(StaffSpecialization specialization)
        {
            this.StaffSpecialization = specialization;
        }

        /**
         * Changes the phone number of the staff member.
         *
         * @param phoneNumber The new phone number to be set.
         */
        public void ChangeStaffPhoneNumber(StaffPhoneNumber phoneNumber)
        {
            this.StaffPhoneNumber = phoneNumber;
        }

        /**
         * Changes the email of the staff member.
         *
         * @param userEmail The new email address to be set.
         */
        public void ChangeUserEmail(UserEmail userEmail)
        {
            this.UserEmail = userEmail;
        }

        /**
         * Activates the staff member.
         * This sets the staff member's active status to true.
         */
        public void ActivateStaff()
        {
            this.IsActive = true;
        }

        /**
        * Deactivates the staff member.
        * This sets the staff member's active status to false.
        */
        public void DeactivateStaff()
        {
            this.IsActive = false;
        }

        /**
         * Updates the availability slots of the staff member.
         *
         * @param staffAvaiabilitySlots The new list of availability slots to be set.
         */
        public void ChangeStaffAvaiabilitySlots(List<StaffAvaiabilitySlots> staffAvaiabilitySlots)
        {
            this.StaffAvaiabilitySlots = staffAvaiabilitySlots;
        }
    }
}