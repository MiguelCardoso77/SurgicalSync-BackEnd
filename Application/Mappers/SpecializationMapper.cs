using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Specializations;
using NSubstitute;

namespace DDDNetCore.Application.Mappers;

public class SpecializationMapper
{
    public SpecializationDto ToDto(Specialization specialization)
    {
        return new SpecializationDto
        {
            code = specialization.Id.AsString(),
            designation = specialization.Designation.Value,
            description = specialization.Description.Value,
        };
    }
    
    public Specialization ToDomain(SpecializationDto specializationDto)
    {
        return new Specialization(
            new SpecializationCode(specializationDto.code), 
            new SpecializationDesignation(specializationDto.designation), 
            string.IsNullOrEmpty(specializationDto.description) ? null : new SpecializationDescription(specializationDto.description)
        );
    }
    
    public List<SpecializationDto> ToListDto(List<Specialization> specialization)
    {
        return specialization.Select(ToDto).ToList();
    }
}