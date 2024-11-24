using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class responsible for interacting with Firebase Authentication and Google OAuth services.
     * Provides methods for creating users, logging in, and managing authentication tokens.
     */
    public class FirebaseService
    {
        private const string ApiKey = "AIzaSyDTHG-LMx7-4UW6Gr3H-djBX0eFzs1k43s";

        private static readonly string SignInUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";

        /**
         * Creates a new user in Firebase with the specified email, password, and role.
         *
         * @param email The user's email address.
         * @param password The user's password.
         * @param role The user's role (e.g., Patient).
         * @return A Task representing the asynchronous operation, with the created UserRecord.
         */
        public static async Task<UserRecord> CreateUserRecordAsync(string email, string password, string role)
        {
            var args = new UserRecordArgs()
            {
                Email = email,
                Password = password,
            };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

            // Setting role claim
            var claims = new Dictionary<string, object>()
            {
                { "role", role }
            };
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);

            Console.WriteLine($"Successfully created new user: {userRecord.Uid}");

            return userRecord;
        }

        /**
         * Verifies an ID token using Firebase Authentication.
         *
         * @param idToken The ID token to verify.
         * @return A Task representing the asynchronous operation, with the decoded FirebaseToken.
         */
        public static async Task<string> VerifyIdTokenAsync(string idToken)
        {
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                
                if (decodedToken.Claims.TryGetValue("email", out object email))
                {
                    Console.WriteLine("Token belongs to: " + email);
                    return email.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error verifying ID token: {e.Message}");
                return null;
            }
            
            return null;
        }

        /**
         * Generates a password reset link for the specified email.
         *
         * @param email The email for which the password reset link is generated.
         * @return A Task representing the asynchronous operation, with the generated reset link.
         */
        public static async Task<string> GeneratePasswordResetLink(string email)
        {
            var link = await FirebaseAuth.DefaultInstance.GeneratePasswordResetLinkAsync(email);
            Console.WriteLine("Successfully generated password reset link for " + email +".");
            return link;
        }

        /**
         * Logs in a user using email and password, returning an access token.
         *
         * @param emailDto The user's email address.
         * @param passwordDto The user's password.
         * @return A Task representing the asynchronous operation, with the access token string if successful.
         */
        public static async Task<LoginResponse> LoginWithEmailPassword(string emailDto, string passwordDto)
        {
            using (var client = new HttpClient())
            {
                var loginData = new
                {
                    email = emailDto,
                    password = passwordDto,
                    returnSecureToken = true
                };

                var jsonContent = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(SignInUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseData);
                    
                    return loginResponse;
                }
                
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login Error: {response.StatusCode} - {errorResponse}");
                return null;
            }
        }
        
        public static async Task ResetUserPasswordAsync(string email, string newPassword, string oobCode)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key={ApiKey}";

            using (var client = new HttpClient())
            {
                var requestData = new
                {
                    oobCode = oobCode,         
                    newPassword = newPassword 
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Password reset successfully.");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error resetting password: {errorResponse}");
                    throw new Exception($"Failed to reset password: {errorResponse}");
                }
            }
        }


    }
}