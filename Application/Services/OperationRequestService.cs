using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    // Service class for handling operation requests. Provides methods to add, update, and retrieve operation requests.
    public class OperationRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _repo;
        private readonly ILogger<OperationRequestService> _logger;

        // Initializes a new instance of the <OperationRequestService> class.
        // <param name="unitOfWork">The unit of work to manage transactions.
        // <param name="repo">The repository for operation requests.
        // <param name="logger">Logger instance for logging operations.
        
        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository repo, ILogger<OperationRequestService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
        }
        
        // Asynchronously retrieves all operation requests.
        // Return: A task representing the asynchronous operation, containing a list of <OperationRequestDto> objects.
        
        public async Task<List<OperationRequestDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<OperationRequestDto> listDto = list.ConvertAll<OperationRequestDto>(ot => new OperationRequestDto
            {
                OperationRequestId = ot.Id.AsString(), OperationTypeId = ot.OperationTypeId.AsString(),
                DeadlineDate = ot.DeadlineDate.ToString(), LicenseNumber = ot.LicenseNumber.AsString(),
                MedicalRecordNumber = ot.MedicalRecordNumber.AsString(), Priority = ot.Priority.ToString()
            });
            return listDto;
        }
        
        // Asynchronously retrieves an operation request by its ID.
        // <param name="operationRequestId">The ID of the operation request.
        // Return: A task representing the asynchronous operation, containing the <OperationRequestDto> object or null if not found.

        public async Task<OperationRequestDto> GetByIdAsync(OperationRequestId operationRequestId)
        {
            var ot = await this._repo.GetByIdAsync(operationRequestId);
            
            if(ot == null)
                return null;

            return null;
        }
        
        // Asynchronously adds a new operation request.
        // <param name="operationRequestDto">The data transfer object representing the operation request to be added.
        /// Return : A task representing the asynchronous operation, containing the added <OperationRequestDto/>.

        public async Task<OperationRequestDto> AddAsync(OperationRequestDto operationRequestDto) 
        {
            
            var dtoId = string.IsNullOrEmpty(operationRequestDto.OperationRequestId)
            ? new OperationRequestId(Guid.NewGuid().ToString())
            : new OperationRequestId(operationRequestDto.OperationRequestId);

            var deadlineDate = new DeadlineDate(DateTime.Parse(operationRequestDto.DeadlineDate));

            if (!Enum.TryParse<Priority>(operationRequestDto.Priority, out var priority))
            {
                throw new ArgumentException($"Invalid priority value: {operationRequestDto.Priority}");
            }

            var operationTypeId = new OperationTypeId(operationRequestDto.OperationTypeId);
            var medicalRecordNumber = new MedicalRecordNumber(operationRequestDto.MedicalRecordNumber);
            var licenseNumber = new LicenseNumber(operationRequestDto.LicenseNumber);

            var domainObj = OperationRequestMapper.ToDomain(
            operationRequestDto, dtoId, deadlineDate, priority, operationTypeId, medicalRecordNumber, licenseNumber);

            await this._repo.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();

            _logger.LogInformation("OperationRequest with ID: {OperationRequestId} was successfully created!", domainObj.Id.AsString());

            return new OperationRequestDto()
            {
                OperationRequestId = domainObj.Id.AsString(),
                DeadlineDate = domainObj.DeadlineDate.ToString(),
                Priority = domainObj.Priority.ToString(),
                MedicalRecordNumber = domainObj.MedicalRecordNumber.AsString(),
                OperationTypeId = domainObj.OperationTypeId.AsString(),
                LicenseNumber = domainObj.LicenseNumber.AsString()
            };
        }
        
        // Asynchronously updates an existing operation request.
        // <param name="operationRequestDto">The data transfer object representing the updated operation request.
        /// Return : A task representing the asynchronous operation, containing the updated <OperationRequestDto/>, or null if not found.

        public async Task<OperationRequestDto> UpdateAsync(OperationRequestDto operationRequestDto)
        {
            var ot = await this._repo.GetByIdAsync(new OperationRequestId(operationRequestDto.OperationRequestId));

            if (ot == null)
                return null;
            
            await this._unitOfWork.CommitAsync();

            return null;
        }
        
        // Asynchronously inactivates (deactivates) an operation request.
        // <param name="operationRequestId">The ID of the operation request to inactivate.
        // Return : A task representing the asynchronous operation, returning null if the operation request was not found.
        
        public async Task<OperationRequestDto> InactivateAsync(OperationRequestId operationRequestId)
        {
            var oT = await this._repo.GetByIdAsync(operationRequestId);

            if (oT == null)
                return null;
            
            oT.DeactivateOperationRequest();
            
            await this._unitOfWork.CommitAsync();

            return null;
        }
    }
}