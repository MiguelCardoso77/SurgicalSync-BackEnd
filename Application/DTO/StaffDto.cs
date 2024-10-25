using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Application.DTO
{
    public class StaffDto
    {
        public string  Id { get;  set; }
        public string StaffName { get;  set; }
        public string UserEmail { get;  set; }
        public string StaffPhoneNumber { get;  set; }
        public string StaffSpecialization { get;  set; }
        public List<string> StaffAvaiabilitySlots { get;  set; }
        public string StaffType { get;  set; }
        public Boolean isActive{ get;  set; }
    }
    
    public class StaffDto2
    {
        public string StaffName { get;  set; }
        public string UserEmail { get;  set; }
        public string StaffSpecialization { get;  set; }

    }
    
    
}