using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.OperationTypes;

namespace DDDNetCore.Application.Mappers
{
    public class OperationTypeMapper
    {
        public static OperationTypeDto ToDto(OperationType operationType)
        {
            return new OperationTypeDto { Id = operationType.Id.AsString(), OperationName = operationType.Name.ToString() };
        }
        
        public static OperationType ToDomain(OperationTypeDto dto, OperationTypeId operationTypeId, List<RequiredStaff> requiredStaffList)
        {
            return new OperationType(operationTypeId, new OperationName(dto.OperationName), requiredStaffList, new EstimatedDuration(dto.EstimatedDuration));
        }
    }
}