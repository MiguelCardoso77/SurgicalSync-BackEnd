using System;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<string> LoginWithEmailPasswordAsync(LoginDto dto)
        {
           var response = await FirebaseService.LoginWithEmailPassword(dto.Email, dto.Password);
           Console.WriteLine(response);
           return response;
        }
    }
}