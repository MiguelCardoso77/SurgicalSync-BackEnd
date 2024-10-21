using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Application.Mappers
{
    public class StaffMapper
    {
        
        public  static Staff ToDomain(StaffDto dto, LicenseNumber id ,List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList)
        {
            return new Staff(new LicenseNumber(dto.Id), new StaffName(dto.StaffName.ToString()),
                new StaffEmail(dto.StaffEmail.ToString()),
                new StaffPhoneNumber(dto.StaffPhoneNumber.ToString()),
                Enum.Parse<StaffSpecialization>(dto.StaffSpecialization),
                staffAvaiabilitySlotsList
                //,Enum.Parse<StaffType>(dto.StaffType)
                );
        }

        
        
        public  static StaffDto ToDto(Staff domain)
        {
            return new StaffDto
            {
                Id = domain.Id.AsString(),
                StaffName = domain.StaffName.ToString(),
                StaffEmail = domain.StaffEmail.ToString(),
                StaffPhoneNumber = domain.StaffPhoneNumber.ToString(),
                StaffSpecialization = domain.StaffSpecialization.ToString(),  
                StaffAvaiabilitySlots = domain.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                //StaffType = domain.StaffType.ToString()
            };
        }
        
        public  List<StaffDto> ToListDto(List<Staff> domainList)
        {
            return domainList.Select(domain => ToDto(domain)).ToList();
        }

        
    }
}