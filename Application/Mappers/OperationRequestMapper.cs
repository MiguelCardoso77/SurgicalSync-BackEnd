using System;
using System.ComponentModel;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Application.Mappers
{
    /**
     * Mapper class to convert between OperationRequest domain model and OperationRequestDto.
     */
    public class OperationRequestMapper
    {
        /**
         * Converts a domain OperationRequest object to an OperationRequestDto type object.
         */
        public OperationRequestDto ToDto(OperationRequest domain)
        {
            
            return new OperationRequestDto
            {
                OperationRequestId = domain.Id.AsString(),
                DeadlineDate = domain.DeadlineDate.ToString(),
                LicenseNumber = domain.LicenseNumber.AsString(),
                Priority = domain.Priority.ToString(),
                OperationTypeId = domain.OperationTypeId.AsString(),
                MedicalRecordNumber = domain.MedicalRecordNumber.AsString(),
            };
        }
        
        /**
         * Converts the input data to a domain OperationRequest object.
         */
        public OperationRequest ToDomain(OperationRequestDto dto, OperationRequestId operationRequestId)
        {
            var parsedDeadlineDate = DateTime.Parse(dto.DeadlineDate);
            return new OperationRequest(operationRequestId, Enum.Parse<Priority>(dto.Priority), new DeadlineDate(parsedDeadlineDate), new OperationTypeId(dto.OperationTypeId), new MedicalRecordNumber(dto.MedicalRecordNumber), new LicenseNumber(dto.LicenseNumber));
        }
    }
}