using System.ComponentModel;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Application.Mappers
{
    public class OperationRequestMapper
    {
        // Mapper class to convert between OperationRequest domain model and OperationRequestDto.
        public static OperationRequestDto ToDto(OperationRequest domain)
        {
            // Converts a domain OperationRequest object to an OperationRequestDto.
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
        // Converts the input data to a domain OperationRequest object.
        public static OperationRequest ToDomain(OperationRequestDto dto, OperationRequestId operationRequestId,
            DeadlineDate deadlineDate, Priority priority, OperationTypeId operationTypeId,
            MedicalRecordNumber medicalRecordNumber, LicenseNumber licenseNumber)
        {
            return new OperationRequest(operationRequestId, priority, deadlineDate, operationTypeId, medicalRecordNumber, licenseNumber);
        }
    }
}