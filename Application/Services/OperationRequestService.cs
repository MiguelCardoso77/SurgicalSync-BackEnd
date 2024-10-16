using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDSample1.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class for handling operation requests. Provides methods to add, update, and retrieve operation requests.
     */
    public class OperationRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _repo;
        private readonly ILogger<OperationRequestService> _logger;
        private readonly OperationRequestMapper _mapper;

        /** Initializes a new instance of the <OperationRequestService/> class.
         * <param name="unitOfWork"> The unit of work to manage transactions </param>
         * <param name="repo"> The repository for operation requests </param>
         * <param name="logger"> Logger instance for logging operations </param>
         * <param name="operationRequestMapper"> Mapper instance for mapping operations (e.g., domain to dto, dto to domain)</param>
         */
        
        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository repo, ILogger<OperationRequestService> logger, OperationRequestMapper operationRequestMapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
            this._mapper = operationRequestMapper;
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

            List<OperationRequestDto> operationRequestDto = list.ConvertAll<OperationRequestDto>(ot => _mapper.ToDto(ot));

            return operationRequestDto;
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
    }
}