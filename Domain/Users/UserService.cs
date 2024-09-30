using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Families;
using DDDSample1.Domain.Shared;
using Domain.Users;

namespace DDDNetCore.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;
        
        public UserService(IUnitOfWork unitOfWork, IUserRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }
        
        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<UserDto> listDto = list.ConvertAll<UserDto>(user => new UserDto{Id = user.Id.AsString(), UserName = user.UserName});
            
            return listDto;
        }
        
        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);
            
            if(user == null)
                return null;
            
            return new UserDto{Id = user.Id.AsString(), UserName = user.UserName};
        }
        
        public async Task<UserDto> AddAsync(UserDto dto)
        {
            var userId = string.IsNullOrEmpty(dto.Id) ? new UserId(Guid.NewGuid().ToString()) : new UserId(dto.Id);
            
            var user = new User(new UserId(dto.Id), dto.UserName, dto.UserEmail, dto.Password);
            
            await this._repo.AddAsync(user);
            
            await this._unitOfWork.CommitAsync();
            
            return new UserDto { Id = user.Id.AsString(), UserName = user.UserName, UserEmail = user.UserEmail, Password = user.Password };
        }
        
        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id)); 
            
            if (user == null)
                return null;   
            
            // change all field
            user.ChangeUserName(dto.UserName);
            
            await this._unitOfWork.CommitAsync();
            
            return new UserDto { Id = user.Id.AsString(), UserName = user.UserName };
        }
        
        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;
            
            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();
            
            return new UserDto { Id = user.Id.AsString(), UserName = user.UserName };
        }
    }
    
}