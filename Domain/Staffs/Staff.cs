using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
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
        }
        public Staff(StaffId id ,StaffName staffName, UserEmail userEmail, StaffPhoneNumber staffPhoneNumber,
            StaffSpecialization staffSpecialization, List<StaffAvaiabilitySlots > staffAvaiabilitySlots, StaffType staffType, Boolean isActive)
        {
            this.Id = id;
            this.StaffName = staffName;
            this.UserEmail = userEmail;
            this.StaffPhoneNumber = staffPhoneNumber;
            this.StaffSpecialization = staffSpecialization;
            this.StaffAvaiabilitySlots = staffAvaiabilitySlots;
            this.StaffType = staffType;
            this.IsActive = true;
        }
        
        public void ChangeStaffSpecialization(StaffSpecialization specialization)
        {
            this.StaffSpecialization = specialization;
        }
        
        public void ChangeStaffPhoneNumber(StaffPhoneNumber phoneNumber)
        {
            this.StaffPhoneNumber = phoneNumber;
        }
        
        public void ChangeUserEmail(UserEmail userEmail)
        {
            this.UserEmail = userEmail;
        }

        public void ActivateStaff()
        {
            this.IsActive = true;
        }
        
        public void DeactivateStaff()
        {
            this.IsActive = false;
        }

        public void ChangeStaffAvaiabilitySlots(List<StaffAvaiabilitySlots> staffAvaiabilitySlots)
        {
            this.StaffAvaiabilitySlots = staffAvaiabilitySlots;
        }
       
    }
}