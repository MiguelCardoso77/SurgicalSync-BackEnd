using System.Collections.Generic;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Staffs;

namespace DDDNetCore.Application.DTO
{
    public class StaffDto
    {
        public string  Id { get;  set; }
        public string StaffName { get;  set; }
        public string StaffEmail { get;  set; }
        public string StaffPhoneNumber { get;  set; }
        public string StaffSpecialization { get;  set; }
        public List<string> StaffAvaiabilitySlots { get;  set; }
        public string StaffType { get;  set; }
    }
}