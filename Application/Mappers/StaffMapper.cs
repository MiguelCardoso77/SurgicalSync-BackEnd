using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Mappers
{
    /**
     * Mapper class for converting between Staff domain entities and their corresponding Data Transfer Objects (DTOs).
     */
    public class StaffMapper
    {
        /**
         * Converts a StaffDto to a Staff domain entity.
         *
         * @param dto The Data Transfer Object containing staff information.
         * @param id The unique identifier for the staff member.
         * @param staffAvaiabilitySlotsList The list of availability slots for the staff member.
         * @return A Staff domain entity populated with the data from the provided StaffDto.
         */
        public Staff ToDomain(StaffDto dto, StaffId id, List<StaffAvaiabilitySlots> staffAvaiabilitySlotsList)
        {
            return new Staff(id,
                new StaffName(dto.StaffName),
                new UserEmail(dto.UserEmail),
                new StaffPhoneNumber(dto.StaffPhoneNumber),
                Enum.Parse<StaffSpecialization>(dto.StaffSpecialization),
                staffAvaiabilitySlotsList,
                Enum.Parse<StaffType>(dto.StaffType),
                dto.isActive,
                new StaffLicenseNumber(dto.StaffLicenseNumber));
        }

        /**
         * Converts a Staff domain entity to a StaffDto.
         *
         * @param domain The Staff domain entity to be converted.
         * @return A StaffDto containing the data from the provided Staff domain entity.
         */
        public StaffDto ToDto(Staff domain)
        {
            return new StaffDto
            {
                Id = domain.Id.AsString(),
                StaffName = domain.StaffName.ToString(),
                UserEmail = domain.UserEmail.ToString(),
                StaffPhoneNumber = domain.StaffPhoneNumber.ToString(),
                StaffSpecialization = domain.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots =
                    domain.StaffAvaiabilitySlots.Select(rs => rs.Value).ToList(),
                StaffType = domain.StaffType.ToString(),
                isActive = domain.IsActive,
                StaffLicenseNumber = domain.StaffLicenseNumber.ToString()
            };
        }

        /**
         * Converts a Staff domain entity to a simplified StaffDto2.
         *
         * @param domain The Staff domain entity to be converted.
         * @return A StaffDto2 containing the basic staff data from the provided Staff domain entity.
         */
        public StaffDtoList ToDtoList(Staff domain)
        {
            return new StaffDtoList
            {
                Id = domain.Id.AsString(),
                StaffName = domain.StaffName.ToString(),
                UserEmail = domain.UserEmail.ToString(),
                StaffSpecialization = domain.StaffSpecialization.ToString(),
            };
        }

        /**
         * Converts a list of Staff domain entities to a list of StaffDto2 objects.
         *
         * @param domainList The list of Staff domain entities to be converted.
         * @return A list of StaffDto2 objects representing the provided Staff domain entities.
         */
        public List<StaffDtoList> ToListDto(List<Staff> domainList)
        {
            return domainList.Select(domain => ToDtoList(domain)).ToList();
        }
    }
}