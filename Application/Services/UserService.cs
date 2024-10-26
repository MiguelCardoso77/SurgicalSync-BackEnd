using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class for handling users. Provides methods to add, update, and retrieve users.
     */
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;
        private readonly UserMapper _mapper;
        
        /** Initializes a new instance of the <UserService/> class.
         * <param name="unitOfWork"> The unit of work to manage transactions </param>
         * <param name="repo"> The repository for operation types </param>
         * <param name="mapper"> Mapper instance for mapping operations (e.g., domain to dto, dto to domain)</param>
         */
        public UserService(IUnitOfWork unitOfWork, IUserRepository repo, UserMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
        }
        
        /**
         * Asynchronously retrieves all users.
         * Return: A task representing the asynchronous operation, containing a list of <UserDto/> objects.
         */
        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            return _mapper.ToDtoList(list);
        }
        
        /**
         * Asynchronously retrieves a user by its id.
         * <param name="id"> The id of the user </param>
         * Return: A task representing the asynchronous operation, containing an <UserDto/> object.
         */
        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);
            
            if (user == null)
                return null;
            
            var dto = _mapper.ToDto(user);

            return dto;
        }
        
        /**
         * Asynchronously retrieves a user by their email address.
         * <param name="userEmail"> The email address of the user to retrieve. </param>
         * Fetches all users from the repository, finds the user with the matching email,
         * and returns a corresponding <UserDto/> if a match is found.
         * The email comparison is case-insensitive.
         * <returns> A task representing the asynchronous operation, containing a <UserDto/> if found; otherwise, <c>null</c>. </returns>
         */
        public async Task<UserDto> GetUserByEmail(UserEmail userEmail)
        {
            if (userEmail == null)
            {
                return null;
            }

            var list = await this._repo.GetAllAsync();

            var user = list.FirstOrDefault(pt => pt.UserEmail.ToString().Equals(userEmail.ToString(), StringComparison.OrdinalIgnoreCase));
            
            if (user == null)
            {
                return null;
            }
            
            var userDto = _mapper.ToDto(user);

            return userDto;
        }
        
        /**
         * Asynchronously adds a new user.
         * <param name="dto"> The user data transfer object </param>
         * Generates a random default password, creates the user in Firebase IAM and sends an email to the user with the password and a link to reset it.
         * Return: A task representing the asynchronous operation, containing a <UserDto/> object.
         */
        public async Task<UserDto> AddAsync(UserDto dto)
        {
            // Generate random password
            var password = PasswordService.GeneratePassword();

            // Create User in IAM
            var userRecord = FirebaseService.CreateUserRecordAsync(dto.UserEmail, password, dto.UserRole);
            
            // Create user in system database
            var user = _mapper.ToDomain(dto, new UserId(userRecord.Result.Uid));
            await this._repo.AddAsync(user);
            await this._unitOfWork.CommitAsync();
            
            var resetLink = await FirebaseService.GeneratePasswordResetLink(dto.UserEmail);
            
            // Send set-up email to user
            var smtpEmailService = new EmailService();
            var emailContent = $"Hello {user.Username}! \n Your current password is: {password} , here is the link to reset it: {resetLink} \n Your account will be active once you set-up your account!";            
            var email = new Email(emailContent, user.UserEmail.ToString(), "Activate Your SurgicalSync Account");
            await smtpEmailService.SendEmailAsync(email);
            
            return _mapper.ToDto(user);
        }
        
        /**
         * Asynchronously updates a user.
         * <param name="dto"> The user data transfer object </param>
         * Return: A task representing the asynchronous operation, containing a <UserDto/> object.
         */
        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id)); 
            
            if (user == null)
                return null;
            
            var emailUser = user.UserEmail.ToString();
            
            // Change all fields
            user.ChangeUserName(new Username(dto.UserName));
            user.ChangeUserEmail(new UserEmail(dto.UserEmail));
            
            var smtpEmailService = new EmailService();
            var emailContent = $"Hello {user.Username}! \n Your  contact information was changed. Now it is : email : {emailUser}";
            var email = new Email(emailContent, user.UserEmail.ToString(), "Changes On Your Contact Information");
            await smtpEmailService.SendEmailAsync(email);
            
            await this._unitOfWork.CommitAsync();
            
            return _mapper.ToDto(user);
        }
        
        /**
         * Asynchronously deletes a user.
         * <param name="id"> The id of the user </param>
         * Return: A task representing the asynchronous operation, containing a <UserDto/> object.
         */
        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;
            
            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();
            
            return _mapper.ToDto(user);
        }
    }
    
}