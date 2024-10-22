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
    public class FirebaseService
    {
        private const string ApiKey = "AIzaSyDTHG-LMx7-4UW6Gr3H-djBX0eFzs1k43s";
        private static readonly string SignInUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";
        
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
        
        public static async Task<string> GeneratePasswordResetLink(string email)
        {
            var link = await FirebaseAuth.DefaultInstance.GeneratePasswordResetLinkAsync(email);
            Console.WriteLine("Successfully generated password reset link for {email}");
            return link;
        }
        
        public static async Task<string> LoginWithEmailPassword(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var loginData = new
                {
                    Email = email,
                    Password = password,
                    returnSecureToken = true
                };
                
                var jsonContent = JsonConvert.SerializeObject(loginData);
                Console.WriteLine(jsonContent);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(SignInUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Passou");
                    var responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Passou");

                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseData);
                    
                    Console.WriteLine($"Access Token: {loginResponse.IdToken}");
                    Console.WriteLine("Passou");

                    return loginResponse.IdToken;
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login Error: {response.StatusCode} - {errorResponse}");
                return null;
            }
            
        }
    }
}