using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.OperationType;

namespace DDDNetCore.Application.Mappers
{
    /**
     * Mapper class to convert between OperationType domain model and OperationTypeDto.
     */
    public class OperationTypeMapper
    {
        /**
         * Converts a domain OperationType object to an OperationTypeDto type object.
         */
        public OperationTypeDto ToDto(OperationType domain)
        {
            return new OperationTypeDto { 
                Id = domain.Id.AsString(), 
                OperationName = domain.Name.ToString(), 
                RequiredStaff = domain.RequiredStaff.Select(rs => rs.Value).ToList(), 
                EstimatedDuration = domain.EstimatedDuration.Select(rs => rs.Value.ToString()).ToList()
            };
        }

        /**
         * Converts a list of domain OperationType objects to a list of OperationTypeDto objects.
         * @param domainList a list of OperationType domain objects to be converted.
         * @return a list of OperationTypeDto objects corresponding to the provided domain objects.
         */
        public List<OperationTypeDto> ToListDto(List<OperationType> domainList)
        {
            return domainList == null ? new List<OperationTypeDto>() : domainList.Select(ToDto).ToList();
        }
        
        /**
         * Converts the input data to a domain OperationType object.
         */
        public OperationType ToDomain(OperationTypeDto dto, OperationTypeId operationTypeId, List<RequiredStaff> requiredStaffList, List<EstimatedDuration> durations)
        {
            return new OperationType(operationTypeId, new OperationName(dto.OperationName), requiredStaffList, durations);
        }
    }
}