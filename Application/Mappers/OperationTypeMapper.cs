using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.OperationTypes;
using Microsoft.JSInterop.Infrastructure;

namespace DDDNetCore.Application.Mappers
{
    public class OperationTypeMapper
    {
        public OperationTypeDto ToDto(OperationType domain)
        {
            return new OperationTypeDto { 
                Id = domain.Id.AsString(), 
                OperationName = domain.Name.ToString(), 
                RequiredStaff = domain.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(), 
                EstimatedDuration = domain.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList()
            };
        }

        public List<OperationTypeDto> ToListDto(List<OperationType> domainList)
        {
            return domainList.Select(domain => ToDto(domain)).ToList();
        }
        
        public OperationType ToDomain(OperationTypeDto dto, OperationTypeId operationTypeId, List<RequiredStaff> requiredStaffList, List<EstimatedDuration> durations)
        {
            return new OperationType(operationTypeId, new OperationName(dto.OperationName), requiredStaffList, durations);
        }
    }
}