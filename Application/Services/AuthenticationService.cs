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
         * Authenticates a user using Google login and creates a new patient profile if successful.
         *
         * @param dto The GoogleLoginDto containing Google login details.
         * @return A Task representing the asynchronous operation, returning the GoogleLoginDto.
         */
        public async Task<GoogleLoginDto> LoginWithGoogle(GoogleLoginDto dto)
        {
            await FirebaseService.GivePatientGoogleAuthAsync(dto.RequestUri, dto.Email);
            await FirebaseService.CreateUserRecordAsync(dto.Email, "Default999###", "Patient");
            await _micro.CreatePatientProfile(dto);

            return dto;
        }
        
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

        /**
         * Authenticates a patient using a Google authorization token, verifies the token,
         * and creates a new patient profile if successful.
         *
         * @param dto The GoogleLoginDto containing Google authorization token information.
         * @return A Task representing the asynchronous operation, returning the created PatientDto.
         */
        public async Task<PatientDto> AuthenticatePatientToken(GoogleLoginDto dto)
        {
            var accessToken = await FirebaseService.ExchangeAuthorizationCodeForAccessToken(dto.AuthCode);
            var jwtToken = await FirebaseService.ExchangeAuthorizationCodeForIdToken(dto.AuthCode);

            await FirebaseService.VerifyIdTokenAsync(jwtToken);
            var patientEmail = await FirebaseService.GetUserEmailFromGoogle(accessToken);

            await FirebaseService.CreateUserWithGoogleAsync(jwtToken, patientEmail);

            return await _micro.CreatePatientProfile(dto);
        }
    }
}