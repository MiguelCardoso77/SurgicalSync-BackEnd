using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Specializations;

namespace DDDNetCore.Application.Mappers;

public class SpecializationMapper
{
    public SpecializationDto ToDto(Specialization specialization)
    {
        return new SpecializationDto
        {
            SpecializationCode = specialization.Id.AsString(),
            SpecializationDesignation = specialization.Designation.Value,
            SpecializationDescription = specialization.Description.Value,
        };
    }
    
    public Specialization ToDomain(SpecializationDto specializationDto)
    {
        return new Specialization(
            new SpecializationId(specializationDto.SpecializationCode), 
            new SpecializationDesignation(specializationDto.SpecializationDesignation), 
            string.IsNullOrEmpty(specializationDto.SpecializationDescription) ? null : new SpecializationDescription(specializationDto.SpecializationDescription)
        );
    }
    
    public List<SpecializationDto> ToListDto(List<Specialization> specialization)
    {
        return specialization.Select(ToDto).ToList();
    }
}