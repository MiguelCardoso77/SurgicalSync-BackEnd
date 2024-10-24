using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class UserEmailMicroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;


        public UserEmailMicroService(IUnitOfWork unitOfWork, IUserRepository userRepository,
            IPatientRepository patientRepository)
        {
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
            this._patientRepository = patientRepository;
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