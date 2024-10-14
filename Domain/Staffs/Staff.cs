using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class Staff : Entity<LicenseNumber>, IAggregateRoot
    {
        public StaffType StaffType{ get; private set; }
        public LicenseNumber Id { get; private set; }
        public StaffName StaffName { get; private set; }
        public StaffEmail StaffEmail { get; private set; }
        public StaffPhoneNumber StaffPhoneNumber { get; private set; }
        public StaffSpecialization StaffSpecialization { get; private set; }
        public List<StaffAvaiabilitySlots> StaffAvaiabilitySlots { get; private set; }
        public bool IsActive { get; private set; }

        private Staff()
        {
            this.Id = null;
            this.StaffName = null;
            this.StaffEmail = null;
            this.StaffPhoneNumber = null;
            this.StaffSpecialization = StaffSpecialization.None;
            this.StaffAvaiabilitySlots = null;
            this.StaffType = StaffType.Other;
        }

        public Staff(LicenseNumber id ,StaffName staffName, StaffEmail staffEmail, StaffPhoneNumber staffPhoneNumber,
            StaffSpecialization staffSpecialization, List<StaffAvaiabilitySlots > staffAvaiabilitySlots, StaffType staffType)
        {
            this.Id = id;
            this.StaffName = staffName;
            this.StaffEmail = staffEmail;
            this.StaffPhoneNumber = staffPhoneNumber;
            this.StaffSpecialization = staffSpecialization;
            this.StaffAvaiabilitySlots = staffAvaiabilitySlots;
            this.IsActive = true;
            this.StaffType = staffType;


        }
        
        public void ChangeStaffSpecialization(StaffSpecialization specialization)
        {
            this.StaffSpecialization = specialization;
        }
        
        public void ChangeStaffPhoneNumber(StaffPhoneNumber phoneNumber)
        {
            this.StaffPhoneNumber = phoneNumber;
        }
        
        public void ActivateStaff()
        {
            this.IsActive = true;
        }
        
        public void DeactivateStaff()
        {
            this.IsActive = false;
        }
        
    }
}