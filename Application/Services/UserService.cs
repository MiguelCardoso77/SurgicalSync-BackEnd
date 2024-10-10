using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;
using FirebaseAdmin.Auth;

namespace DDDNetCore.Application.Services
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
            
            List<UserDto> listDto = list.ConvertAll<UserDto>(user => new UserDto{Id = user.Id.AsString(), UserName = user.Username.ToString(), 
            UserEmail = user.UserEmail.ToString(), UserRole = user.UserRole.ToString()});
            
            return listDto;
        }
        
        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);
            
            if(user == null)
                return null;
            
            return new UserDto{Id = user.Id.AsString(), UserName = user.Username.ToString(), 
            UserEmail = user.UserEmail.ToString(), UserRole = user.UserRole.ToString()};
        }
        
        public async Task<UserDto> AddAsync(UserDto dto)
        {
            // Generate random password
            var password = PasswordService.GeneratePassword();
            
            // Create user in Firebase IAM
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = dto.UserEmail,
                Password = password,
            };
            
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
            Console.WriteLine($"Successfully created new user: {userRecord.Uid}");
            
            // Send set-up email to user
            
            // Create user in system database
            var user = UserMapper.ToDomain(dto, new UserId(userRecord.Uid));
            await this._repo.AddAsync(user);
            await this._unitOfWork.CommitAsync();
            
            return UserMapper.ToDto(user);
        }
        
        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id)); 
            
            if (user == null)
                return null;   
            
            // change all field
            // user.ChangeUserName(dto.UserName.ToString());
            
            await this._unitOfWork.CommitAsync();
            
            return new UserDto{Id = user.Id.AsString(), UserName = user.Username.ToString(), 
            UserEmail = user.UserEmail.ToString(), UserRole = user.UserRole.ToString()};
        }
        
        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;
            
            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();
            
            return new UserDto{Id = user.Id.AsString(), UserName = user.Username.ToString(), 
            UserEmail = user.UserEmail.ToString(), UserRole = user.UserRole.ToString()};
        }
    }
    
}