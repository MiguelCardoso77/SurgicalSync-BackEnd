using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationType;
using DDDSample1.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class for handling operation types. Provides methods to add, update, and retrieve operation requests.
     */
    public class OperationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationTypeRepository _repo;
        private readonly ILogger<OperationTypeService> _logger;
        private readonly OperationTypeMapper _mapper;

        /** Initializes a new instance of the <OperationTypeService/> class.
         * <param name="unitOfWork"> The unit of work to manage transactions </param>
         * <param name="repo"> The repository for operation types </param>
         * <param name="logger"> Logger instance for logging operations </param>
         * <param name="mapper"> Mapper instance for mapping operations (e.g., domain to dto, dto to domain)</param>
         */
        public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository repo,
            ILogger<OperationTypeService> logger, OperationTypeMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
            this._mapper = mapper;
        }

        /**
         * Asynchronously retrieves all operation types.
         * Return: A task representing the asynchronous operation, containing a list of <OperationTypeDto/> objects.
         */
        public async Task<List<OperationTypeDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            return _mapper.ToListDto(list);
        }

        /**
         * Asynchronously retrieves all operation types filtered by status.
         * <param name="isActive"> The status of the operation type </param>
         * Return: A task representing the asynchronous operation, containing a list of <OperationTypeDto/> objects.
         */
        public async Task<List<OperationTypeDto>> GetAllByStatus(bool isActive)
        {
            var list = await this._repo.GetAllAsync();
            
            var filteredList = list.Where(oT => oT.IsActive == isActive).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Asynchronously retrieves all operation types filtered by name.
         * <param name="operationName"> The name of the operation type </param>
         * Return: A task representing the asynchronous operation, containing a list of <OperationTypeDto/> objects.
         */
        public async Task<List<OperationTypeDto>> GetAllByName(string operationName)
        {
            var list = await this._repo.GetAllAsync();
            
            var filteredList = list.Where(op => op.Name.ToString().Contains(operationName, StringComparison.OrdinalIgnoreCase)).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Asynchronously retrieves all operation types filtered by specialization.
         * <param name="specialization"> The specialization of the operation type </param>
         * Return: A task representing the asynchronous operation, containing a list of <OperationTypeDto/> objects.
         */
        public async Task<List<OperationTypeDto>> GetAllBySpecialization(string specialization)
        {
            var list = await this._repo.GetAllAsync();

            // Available specializations: Prosthetics, Arthroscopy, Spine
            var surgeryIds = specialization switch
            {
                "Prosthetics" => new List<string> { "2", "3", "4" },
                "Arthroscopy" => new List<string> { "1", "5", "6", "7" },
                "Spine" => new List<string> { "8" },
                _ => new List<string>()
            };

            var filteredList = list.Where(op => surgeryIds.Contains(op.Id.AsString())).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Asynchronously retrieves an operation type by its ID.
         * <param name="id"> The ID of the operation type </param>
         * Return: A task representing the asynchronous operation, containing the <OperationTypeDto/> object or null if not found.
         */
        public async Task<OperationTypeDto> GetByIdAsync(OperationTypeId id)
        {
            var oT = await this._repo.GetByIdAsync(id);

            if (oT == null)
                return null;

            var dto = _mapper.ToDto(oT);

            return dto;
        }

        /**
         * Asynchronously adds a new operation type.
         * <param name="dto"> The data transfer object representing the new operation type </param>
         * Return: A task representing the asynchronous operation, containing the created <OperationTypeDto/> object.
         * @throws InvalidOperationException if an operation type with the same name already exists.
         */
        public async Task<OperationTypeDto> AddAsync(OperationTypeDto dto)
        {
            var existingOperationType = await GetAllAsync();
            foreach(OperationTypeDto ot in existingOperationType)
            {
                if (ot.OperationName.Equals(dto.OperationName, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("An operation type with the same name already exists.");
            }
            
            var dtoId = string.IsNullOrEmpty(dto.Id) ? new OperationTypeId(Guid.NewGuid().ToString()) : new OperationTypeId(dto.Id);

            // List of estimated durations (Preparation, Surgery, Cleaning)
            var estimatedDurations = new List<EstimatedDuration>
            {
                new(dto.EstimatedDuration[0]),
                new(dto.EstimatedDuration[1]),
                new(dto.EstimatedDuration[2])
            };

            var requiredStaffList = new List<RequiredStaff>();

            // Add predefined staff members to the list
            var predefinedStaff = new List<RequiredStaff>
            {
                new("1 Orthopaedist"),
                new("1 Anaesthetist"),
                new("1 Instrumenting Nurse"),
                new("1 Circulating Nurse"),
                new("1 Nurse Anaesthetist"),
                new("1 Medical Action Assistant")
            };
            requiredStaffList.AddRange(predefinedStaff);

            // Add the staff members from the DTO
            var dtoStaff = dto.RequiredStaff.Select(rs => new RequiredStaff(rs)).ToList();
            requiredStaffList.AddRange(dtoStaff);

            // Use the mapper to convert the DTO to a domain object
            var domainObj = _mapper.ToDomain(dto, dtoId, requiredStaffList, estimatedDurations);

            await this._repo.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();

            // Log the successful creation
            _logger.LogInformation("OperationType with ID {OperationTypeId} was successfully created.", domainObj.Id.AsString());

            // Return the same DTO
            return _mapper.ToDto(domainObj);
        }

        /**
         * Asynchronously updates an existing operation type.
         * <param name="dto"> The data transfer object representing the updated operation type </param>
         * Return: A task representing the asynchronous operation, containing the updated <OperationTypeDto/> object or null if not found.
         */
        public async Task<OperationTypeDto> UpdateAsync(OperationTypeDto dto)
        {
            var oT = await this._repo.GetByIdAsync(new OperationTypeId(dto.Id));

            if (oT == null)
                return null;

            oT.ChangeOperationTypeName(new OperationName(dto.OperationName));

            var requiredStaffList = dto.RequiredStaff.Select(rs => new RequiredStaff(rs)).ToList();
            oT.ChangeRequiredStaff(requiredStaffList);

            var estimatedDurationList = dto.EstimatedDuration.Select(ed => new EstimatedDuration(ed)).ToList();
            oT.ChangeEstimatedDuration(estimatedDurationList);

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(oT);
        }

        /**
         * Asynchronously inactivates an operation type.
         * <param name="id"> The ID of the operation type to inactivate </param>
         * Return: A task representing the asynchronous operation, containing the inactivated <OperationTypeDto/> object or null if not found.
         */
        public async Task<OperationTypeDto> InactivateAsync(OperationTypeId id)
        {
            var oT = await this._repo.GetByIdAsync(id);

            if (oT == null)
                return null;
            
            oT.DeactivateOperationType();

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(oT);
        }
    }
}