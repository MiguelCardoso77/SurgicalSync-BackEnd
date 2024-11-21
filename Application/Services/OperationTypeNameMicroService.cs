using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Shared;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DDDNetCore.Application.Services
{
    public class OperationTypeNameMicroService
    {
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly IOperationRequestRepository _operationRequestRepository;
        private readonly OperationRequestMapper _operationRequestMapper;
        private readonly ILogger<OperationTypeNameMicroService> _logger;

        public OperationTypeNameMicroService(IOperationTypeRepository operationTypeRepository,
            IOperationRequestRepository operationRequestRepository,
            OperationRequestMapper operationRequestMapper, ILogger<OperationTypeNameMicroService> logger)
        {
            _operationTypeRepository = operationTypeRepository;
            _operationRequestRepository = operationRequestRepository;
            _operationRequestMapper = operationRequestMapper;
            _logger = logger;
        }

        public async Task<List<OperationRequestDto>> GetAllOperationRequestsByOperationTypeName(string operationTypeName)
        {
            if (string.IsNullOrEmpty(operationTypeName))
            {
                _logger.LogError("operationTypeName is null or empty.");
                return new List<OperationRequestDto>();
            }

            var operationTypes = await _operationTypeRepository.GetAllAsync();
            if (operationTypes == null || !operationTypes.Any())
            {
                _logger.LogError("No operation types found.");
                return new List<OperationRequestDto>();
            }

            var operationType = operationTypes.FirstOrDefault(oT => oT.Name.ToString().Equals(operationTypeName, StringComparison.OrdinalIgnoreCase));
            if (operationType == null)
            {
                _logger.LogError($"Operation type not found: {operationTypeName}");
                return new List<OperationRequestDto>();
            }

            var operationRequests = await _operationRequestRepository.GetAllAsync();
            if (operationRequests == null || !operationRequests.Any())
            {
                _logger.LogError("No operation requests found.");
                return new List<OperationRequestDto>();
            }

            var filteredOperationRequests = operationRequests.Where(or => or.OperationTypeId.Equals(operationType.Id)).ToList();
            var operationRequestDtos = _operationRequestMapper.ToListDto(filteredOperationRequests);
            if (operationRequestDtos == null)
            {
                _logger.LogError("Mapping operation requests to DTOs failed.");
                return new List<OperationRequestDto>();
            }

            return operationRequestDtos;
        }

    }
}