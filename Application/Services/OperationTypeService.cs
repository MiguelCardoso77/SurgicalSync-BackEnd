using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationTypes;
using DDDSample1.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    public class OperationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationTypeRepository _repo;
        private readonly ILogger<OperationTypeService> _logger;
        
        public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository repo, ILogger<OperationTypeService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
        }
        
        public async Task<List<OperationTypeDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<OperationTypeDto> listDto = list.ConvertAll<OperationTypeDto>(ot => new OperationTypeDto{Id = ot.Id.AsString(), OperationName = ot.Name.ToString(), 
                RequiredStaff = ot.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(), EstimatedDuration = ot.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList()});
            return listDto;
        }
        
        public async Task<OperationTypeDto> GetByIdAsync(OperationTypeId id)
        {
            var oT = await this._repo.GetByIdAsync(id);
            
            if (oT == null)
                return null;
            
            return new OperationTypeDto {
                Id = oT.Id.AsString(),
                OperationName = oT.Name.ToString(),
                RequiredStaff = oT.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(),
                EstimatedDuration = oT.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList()
            };
        }
        
        public async Task<OperationTypeDto> AddAsync(OperationTypeDto dto)
        {
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
                new ("1 Orthopaedist"),
                new ("1 Anaesthetist"),
                new ("1 Instrumenting Nurse"),
                new ("1 Circulating Nurse"),
                new ("1 Nurse Anaesthetist"),
                new ("1 Medical Action Assistant")
            };
            requiredStaffList.AddRange(predefinedStaff);
            
            // Add the staff members from the DTO
            var dtoStaff = dto.RequiredStaff.Select(rs => new RequiredStaff(rs)).ToList();
            requiredStaffList.AddRange(dtoStaff);

            // Use the mapper to convert the DTO to a domain object
            var domainObj = OperationTypeMapper.ToDomain(dto, dtoId, requiredStaffList, estimatedDurations);
            
            await this._repo.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();
            
            // Log the successful creation
            _logger.LogInformation("OperationType with ID {OperationTypeId} was successfully created.", domainObj.Id.AsString());
            
            // Return the same DTO
            return new OperationTypeDto
            {
                Id = domainObj.Id.AsString(),
                OperationName = domainObj.Name.ToString(),
                RequiredStaff = domainObj.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(),
                EstimatedDuration = domainObj.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList()
            };
        }
        
        public async Task<OperationTypeDto> UpdateAsync(OperationTypeDto dto)
        {
            var ot = await this._repo.GetByIdAsync(new OperationTypeId(dto.Id)); 
            
            if (ot == null)
                return null;   
            
            //ot.Name = new Name(dto.Name);
            //ot.Description = new Description(dto.Description);
            
            //await this._repo.Update(ot);
            await this._unitOfWork.CommitAsync();

            return null;
        }
        
        public async Task<OperationTypeDto> InactivateAsync(OperationTypeId id)
        {
            var oT = await this._repo.GetByIdAsync(id);
            
            if (oT == null)
                return null;
            
            // Deactivate the operation type instead of deleting it from the system
            oT.DeactivateOperationType();
            
            await this._unitOfWork.CommitAsync();

            return new OperationTypeDto {
                Id = oT.Id.AsString(),
                OperationName = oT.Name.ToString(),
                RequiredStaff = oT.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(),
                EstimatedDuration = oT.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList()
            };
        }
    }
}