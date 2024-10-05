using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationTypes;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class OperationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationTypeRepository _repo;
        
        public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }
        
        public async Task<List<OperationTypeDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<OperationTypeDto> listDto = list.ConvertAll<OperationTypeDto>(ot => new OperationTypeDto{Id = ot.Id.AsString(), OperationName = ot.Name.ToString(), 
                RequiredStaff = ot.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(), EstimatedDuration = ot.EstimatedDuration.ToString()});
            
            return listDto;
        }
        
        public async Task<OperationTypeDto> GetByIdAsync(OperationTypeId id)
        {
            var ot = await this._repo.GetByIdAsync(id);
            
            if(ot == null)
                return null;
            
            return null;
        }
        
        public async Task<OperationTypeDto> AddAsync(OperationTypeDto dto)
        {
            var dtoId = string.IsNullOrEmpty(dto.Id) ? new OperationTypeId(Guid.NewGuid().ToString()) : new OperationTypeId(dto.Id);

            var requiredStaffList = new List<RequiredStaff>();
            
            // Predefined staff members
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
            
            var dtoStaff = dto.RequiredStaff.Select(rs => new RequiredStaff(rs)).ToList();
            requiredStaffList.AddRange(dtoStaff);

            var domainObj = OperationTypeMapper.ToDomain(dto, dtoId, requiredStaffList);
            
            await this._repo.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();
            
            return new OperationTypeDto
            {
                Id = domainObj.Id.AsString(),
                OperationName = domainObj.Name.ToString(),
                RequiredStaff = domainObj.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(),
                EstimatedDuration = domainObj.EstimatedDuration.ToString()
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
        
        public async Task<OperationTypeDto> RemoveAsync(OperationTypeId id)
        {
            var ot = await this._repo.GetByIdAsync(id);
            
            if (ot == null)
                return null;
            
            this._repo.Remove(ot);
            await this._unitOfWork.CommitAsync();

            return null;
        }
    }
}