using System;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;

namespace DDDNetCore.Application.Services
{
    /**
     * Service responsible for handling authentication processes, including login and token verification for patients.
     */
    public class AuthenticationService
    {
        private readonly PatientMicroService _micro;

        /**
         * Constructs an AuthenticationService with a specified PatientMicroService.
         *
         * @param micro The PatientMicroService for handling patient profile creation.
         */
        public AuthenticationService(PatientMicroService micro)
        {
            this._micro = micro;
        }

        /**
         * Authenticates a user using email and password and retrieves a login response.
         *
         * @param dto The LoginDto containing email and password.
         * @return A Task representing the asynchronous operation, with a login response string.
         */
        public async Task<LoginResponse> LoginWithEmailPasswordAsync(LoginDto dto)
        {
            return await FirebaseService.LoginWithEmailPassword(dto.Email, dto.Password);
        }
        
        /**
         * Authenticates a user using an auth code and retrieves a login response.
         *
         * @param dto The AuthCodeDto containing the auth code.
         * @return A Task representing the asynchronous operation, with a login response string.
         */
        public async Task<AuthCodeDto> ExchangeToken(AuthCodeDto dto)
        {
            // Verify the ID token and extract user details
            var email = await FirebaseService.VerifyIdTokenAsync(dto.AuthCode);
            if (email == null)
            {
                throw new Exception("Failed to verify ID token.");
            }
            
            return new AuthCodeDto
            {
                AuthCode = dto.AuthCode,
                Email = email
            };
        }
    }
}