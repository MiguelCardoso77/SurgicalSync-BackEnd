using System;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;

namespace DDDNetCore.Application.Services
{
    public class AuthenticationService
    {
        private readonly PatientMicroService _micro;

        public AuthenticationService(PatientMicroService micro)
        {
            this._micro = micro;
        }
        
        public async Task<string> LoginWithEmailPasswordAsync(LoginDto dto)
        {
           var response = await FirebaseService.LoginWithEmailPassword(dto.Email, dto.Password);
           Console.WriteLine(response);
           return response;
        }

        public async Task<GoogleLoginDto> LoginWithGoogle(GoogleLoginDto dto)
        {
            await FirebaseService.GivePatientGoogleAuthAsync(dto.RequestUri, dto.Email);
            await FirebaseService.CreateUserRecordAsync(dto.Email, "Default999###", "Patient");
            await _micro.CreatePatientProfile(dto);
            
            return dto;
        }
        
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