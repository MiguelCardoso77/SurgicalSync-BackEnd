using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Mappers
{
    public class StaffMapper
    {
        
        public   Staff ToDomain(StaffDto dto, StaffId id ,List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList)
        {
            return new Staff(id, new StaffName(dto.StaffName),
                new UserEmail(dto.UserEmail),
                new StaffPhoneNumber(dto.StaffPhoneNumber),
                Enum.Parse<StaffSpecialization>(dto.StaffSpecialization),
                staffAvaiabilitySlotsList
                ,Enum.Parse<StaffType>(dto.StaffType),
                dto.isActive
                );
        }

        
        
        public  StaffDto ToDto(Staff domain)
        {
            return new StaffDto
            {
                Id = domain.Id.AsString(),
                StaffName = domain.StaffName.ToString(),
                UserEmail = domain.UserEmail.ToString(),
                StaffPhoneNumber = domain.StaffPhoneNumber.ToString(),
                StaffSpecialization = domain.StaffSpecialization.ToString(),  
                StaffAvaiabilitySlots = domain.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                StaffType = domain.StaffType.ToString(),
                isActive = domain.IsActive
            };
        }

        public StaffDto2 ToDto2(Staff domain)
        {
            return new StaffDto2
            {
                StaffName = domain.StaffName.ToString(),
                UserEmail = domain.UserEmail.ToString(),
                StaffSpecialization = domain.StaffSpecialization.ToString(),
            };
        }
        
        public  List<StaffDto2> ToListDto(List<Staff> domainList)
        {
            return domainList.Select(domain => ToDto2(domain)).ToList();
        }

        
    }
}