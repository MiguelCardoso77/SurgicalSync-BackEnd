using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.OperationTypes;
using DDDSample1.Domain.OperationTypes;
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
            
            List<OperationTypeDto> listDto = list.ConvertAll<OperationTypeDto>(ot => new OperationTypeDto{Id = ot.Id.ToString(), OperationName = ot.Name.ToString(), 
            RequiredStaff = ot.RequiredStaff.ToString(), EstimatedDuration = ot.EstimatedDuration.ToString()});
            
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
            var otId = string.IsNullOrEmpty(dto.Id) ? new OperationTypeId(Guid.NewGuid().ToString()) : new OperationTypeId(dto.Id);
            
            List<string> requiredStaff = new List<string>();
            
            var ot = new OperationType(otId, new OperationName(dto.OperationName), new RequiredStaff(requiredStaff), new EstimatedDuration(dto.EstimatedDuration));
            
            await this._repo.AddAsync(ot);
            await this._unitOfWork.CommitAsync();
            
            return new OperationTypeDto{Id = ot.Id.ToString(), OperationName = ot.Name.ToString(), RequiredStaff = ot.RequiredStaff.ToString(), EstimatedDuration = ot.EstimatedDuration.ToString()};
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