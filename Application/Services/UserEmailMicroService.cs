using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Services
{
    /**
     * Microservice for verifying user email uniqueness.
     * Provides a method to verify whether an email already exists in the system.
     */
    public class UserEmailMicroService
    {
        private readonly IUserRepository _userRepository;

        /**
         * Initializes a new instance of the UserEmailMicroService class.
         *
         * @param userRepository The repository for User entities.
         */
        public UserEmailMicroService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /**
         * Verifies if a specified email is unique in the system.
         *
         * @param userEmail The email to verify.
         * @return A boolean indicating whether the email is unique (true) or already exists (false).
         */
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