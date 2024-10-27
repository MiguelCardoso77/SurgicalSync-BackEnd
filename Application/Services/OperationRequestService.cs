using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class for handling operation requests. Provides methods to add, update, and retrieve operation requests.
     */
    public class  OperationRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _repo;
        private readonly ILogger<OperationRequestService> _logger;
        private readonly OperationRequestMapper _mapper;
        private readonly PatientNameMicroService _patientNameMicroService;

        /** Initializes a new instance of the <OperationRequestService/> class.
         * <param name="unitOfWork"> The unit of work to manage transactions </param>
         * <param name="repo"> The repository for operation requests </param>
         * <param name="logger"> Logger instance for logging operations </param>
         * <param name="operationRequestMapper"> Mapper instance for mapping operations (e.g., domain to dto, dto to domain)</param>
         */
        
        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository repo, ILogger<OperationRequestService> logger, OperationRequestMapper operationRequestMapper, PatientNameMicroService patientNameMicroService)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
            this._mapper = operationRequestMapper;
            this._patientNameMicroService = patientNameMicroService;
        }
        
        /**
         * Asynchronously retrieves an operation request by its ID.
         * <param name="operationRequestId">The ID of the operation request </param>
         *  Return: A task representing the asynchronous operation, containing the <OperationRequestDto/> object or null if not found.
         */
        
        public async Task<OperationRequestDto> GetByIdAsync(OperationRequestId operationRequestId)
        {
            var operationRequest = await _repo.GetByIdAsync(operationRequestId);

            if (operationRequest == null)
            {
                return null;
            }
            
            var response = _mapper.ToDto(operationRequest);

            return response;
        }
        
        /**
         * Asynchronously retrieves all operation requests.
         * Return: A task representing the asynchronous operation, containing a list of <OperationRequestDto/> objects.
         */
        
        public async Task<List<OperationRequestDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            List<OperationRequestDto> operationRequestDto = list.ConvertAll<OperationRequestDto>(or => _mapper.ToDto(or));

            return operationRequestDto;
        }
        
        /**
         * Asynchronously retrieves a list of OperationRequestDto objects filtered by their active status.
         * @param isActive a boolean indicating whether to retrieve active (true) or inactive (false) operation requests.
         * @return a task representing the asynchronous operation, containing a list of OperationRequestDto objects
         * that match the specified active status.
         */

        public async Task<List<OperationRequestDto>> GetAllByStatus(bool isActive)
        {
            var list = await this._repo.GetAllAsync();
            
            var filteredList = list.Where(or => or.IsActive == isActive).ToList();

            return _mapper.ToListDto(filteredList);
        }
        
        /**
         * Asynchronously adds a new operation request.
         * <param name="operationRequestDto">The data transfer object representing the operation request to be added </param>
         * Return : A task representing the asynchronous operation, containing the added <OperationRequestDto/>.
         */

        public async Task<OperationRequestDto> AddAsync(OperationRequestDto operationRequestDto)
        {
            var dtoId = string.IsNullOrEmpty(operationRequestDto.OperationRequestId) ? new OperationRequestId(Guid.NewGuid().ToString()) : new OperationRequestId(operationRequestDto.OperationRequestId);

            var domainObj = _mapper.ToDomain(operationRequestDto, dtoId);
            
            await this._repo.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();
            
            _logger.LogInformation("Operation request with Id: {OperationRequestId} was successfully created.", domainObj.Id.AsString());
            
            var dto = _mapper.ToDto(domainObj);
            return dto;
        }
        
        /**
         * Asynchronously updates an existing operation request.
         * <param name="operationRequestDto">The data transfer object representing the updated operation request </param>
         * Return : A task representing the asynchronous operation, containing the updated <OperationRequestDto/>, or null if not found.
         */

        public async Task<OperationRequestDto> UpdateAsync(OperationRequestDto operationRequestDto)
        {
            var operationRequest = await _repo.GetByIdAsync(new OperationRequestId(operationRequestDto.OperationRequestId));

            if (operationRequest == null)
                return null;
            
            operationRequest.ChangeDeadlineDate(new DeadlineDate(DateTime.Parse(operationRequestDto.DeadlineDate)));
            
            operationRequest.ChangePriority(Enum.Parse<Priority>(operationRequestDto.Priority));

            await _unitOfWork.CommitAsync();
            
            return _mapper.ToDto(operationRequest);
        }
        
        /**
         *  Asynchronously inactivates (deactivates) an operation request.
         * <param name="operationRequestId">The ID of the operation request to inactivate </param>
         * Return : A task representing the asynchronous operation, returning null if the operation request was not found.
         */
        
        public async Task<OperationRequestDto> InactivateAsync(OperationRequestId operationRequestId)
        {
            var operationRequest = await _repo.GetByIdAsync(operationRequestId);

            if (operationRequest == null)
                return null;
            
            _repo.Remove(operationRequest);
            await _unitOfWork.CommitAsync();
            
            return _mapper.ToDto(operationRequest);
        }
        
        /**
         * Asynchronously retrieves a list of OperationRequestDto objects filtered by operation type.
         * <param name="operationTypeId">The ID of the operation type to filter by.</param>
         * <return> A task representing the asynchronous operation, containing a list of OperationRequestDto objects
         * that match the specified operation type. </return>
         */

        public async Task<List<OperationRequestDto>> GetAllByOperationType(string operationTypeId)
        {
            var list = await _repo.GetAllAsync();
    
            var filteredList = list.Where(or => or.OperationTypeId.AsString().Equals(operationTypeId)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        /**
         * Asynchronously retrieves a list of OperationRequestDto objects filtered by medical record number.
         * <param name="medicalRecordNumber">The medical record number of the patient to filter by.</param>
         * <return> A task representing the asynchronous operation, containing a list of OperationRequestDto objects
         * that match the specified medical record number. </return>
         */

        public async Task<List<OperationRequestDto>> GetAllByMedicalRecordNumber(string medicalRecordNumber)
        {
            var list = await _repo.GetAllAsync();
    
            var filteredList = list.Where(or => or.MedicalRecordNumber.AsString().Equals(medicalRecordNumber)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        /**
         * Asynchronously retrieves a list of OperationRequestDto objects filtered by a date range.
         * <param name="startDate">The start date of the range.</param>
         * <param name="endDate">The end date of the range.</param>
         * <return> A task representing the asynchronous operation, containing a list of OperationRequestDto objects
         * that fall within the specified date range. </return>
         */

        public async Task<List<OperationRequestDto>> GetAllInsideDateRange(string startDate, string endDate)
        {
            var list = await _repo.GetAllAsync();
            
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            
            var filteredList = list.Where(or => or.DeadlineDate.Date >= start && or.DeadlineDate.Date <= end).ToList();
            
            return _mapper.ToListDto(filteredList);
        }
        
        /**
         * Asynchronously retrieves a list of OperationRequestDto objects filtered by patient name.
         * <param name="patientName">The name of the patient to filter by.</param>
         * <return> A task representing the asynchronous operation, containing a list of OperationRequestDto objects that match the specified patient name. </return>
         */

        public async Task<List<OperationRequestDto>> GetAllByPatientName(string patientName)
        {
            return await _patientNameMicroService.GetAllOperationRequestsByPatientName(patientName);
        }
    }
}