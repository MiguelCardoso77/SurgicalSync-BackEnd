using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class UserEmailMicroService
    {
        private readonly IUserRepository _userRepository;


        public UserEmailMicroService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> VerifyEmail(string userEmail)
        {
            var userList = await _userRepository.GetAllAsync();
            
            foreach (User user in userList)
            {
                if (userEmail.Equals(user.UserEmail.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
    }
}