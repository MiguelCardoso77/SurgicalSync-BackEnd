using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain;
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

        private const string GoogleSignInUrl = "https://oauth2.googleapis.com/token";

        private const string GoogleClientId = "921136635518-rgpgi8pcocq13r0u6ad9h0mrm6klln1n.apps.googleusercontent.com";

        private const string GoogleSecretId = "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";

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
         * Creates a new user in Firebase using a Google OAuth ID and email address.
         *
         * @param uid The user's Google OAuth ID.
         * @param email The user's email address.
         * @return A Task representing the asynchronous operation, with the created UserRecord.
         */
        public static async Task<UserRecord> CreateUserWithGoogleAsync(string uid, string email)
        {
            var userRecordArgs = new UserRecordArgs()
            {
                Uid = uid,
                Email = email,
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

            // Setting role and provider claims
            var claims = new Dictionary<string, object>()
            {
                { "provider", "google" },
                { "role", "Patient" }
            };
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);

            return userRecord;
        }

        /**
         * Initiates the Google OAuth process for the patient by generating an authentication URL.
         *
         * @param requestUriDto The redirect URI for the Google OAuth flow.
         * @param destination The destination email for the Google authentication URL.
         * @return A Task representing the asynchronous operation, with the generated Google OAuth URL.
         */
        public static async Task<string> GivePatientGoogleAuthAsync(string requestUriDto, string destination)
        {
            var googleAuthUrl =
                $"https://accounts.google.com/o/oauth2/v2/auth?client_id=1087452717471-mq11aoff38sf70g8nunu1ja9uh9e64ef.apps.googleusercontent.com&redirect_uri={requestUriDto}&response_type=code&scope=openid%20email%20profile";
            var content = $"Here is the URL to create your account through Google: {googleAuthUrl}";

            var email = new Email(content, destination, "Your request to authenticate with Google.");

            var emailService = new EmailService();
            await emailService.SendEmailAsync(email);

            return googleAuthUrl;
        }

        /**
         * Verifies an ID token using Firebase Authentication.
         *
         * @param idToken The ID token to verify.
         * @return A Task representing the asynchronous operation, with the decoded FirebaseToken.
         */
        public static async Task<FirebaseToken> VerifyIdTokenAsync(string idToken)
        {
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                Console.WriteLine($"Token belongs to: {decodedToken.Uid}");
                return decodedToken;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error verifying ID token: {e.Message}");
                return null;
            }
        }

        /**
         * Retrieves a user's email address from Google using an access token.
         *
         * @param accessToken The Google OAuth access token.
         * @return A Task representing the asynchronous operation, with the user's email address.
         */
        public static async Task<string> GetUserEmailFromGoogle(string accessToken)
        {
            var userInfoEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(userInfoEndpoint);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var userInfo = JsonConvert.DeserializeObject<dynamic>(responseString);
                    string email = userInfo.email;
                    return email;
                }
                else
                {
                    Console.WriteLine($"Failed to get user info: {responseString}");
                    return null;
                }
            }
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
            Console.WriteLine("Successfully generated password reset link for {email}");
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
                Console.WriteLine(jsonContent);
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

        /**
         * Exchanges an authorization code for an access token from Google.
         *
         * @param authorizationCode The Google authorization code.
         * @return A Task representing the asynchronous operation, with the retrieved access token.
         */
        public static async Task<string> ExchangeAuthorizationCodeForAccessToken(string authorizationCode)
        {
            var tokenEndpoint = GoogleSignInUrl;
            var clientId = GoogleClientId;
            var clientSecret = GoogleSecretId;
            var redirectUri = "http://localhost:5001/api/operationTypes";

            var requestData = new Dictionary<string, string>
            {
                { "code", authorizationCode },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(requestData);
                Console.WriteLine("Passou");
                var response = await client.PostAsync(tokenEndpoint, content);
                Console.WriteLine("Passou");
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Passou");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Passou");
                    var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
                    string accessToken = tokenResponse.access_token;
                    return accessToken;
                }
                else
                {
                    Console.WriteLine($"Failed to get access token: {responseString}");
                    return null;
                }
            }
        }

        /**
         * Exchanges an authorization code for an ID token from Google.
         *
         * @param authorizationCode The Google authorization code.
         * @return A Task representing the asynchronous operation, with the retrieved ID token.
         */
        public static async Task<string> ExchangeAuthorizationCodeForIdToken(string authorizationCode)
        {
            var tokenEndpoint = GoogleSignInUrl;
            var clientId = GoogleClientId;
            var clientSecret = GoogleSecretId;
            var redirectUri = "http://localhost:5001/api/operationTypes";

            var requestData = new Dictionary<string, string>
            {
                { "code", authorizationCode },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            using (var client = new HttpClient())
            {
                Console.WriteLine("Passou");
                var content = new FormUrlEncodedContent(requestData);
                Console.WriteLine("Passou");
                var response = await client.PostAsync(tokenEndpoint, content);
                Console.WriteLine("Passou");
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Passou");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Passou");
                    var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
                    Console.WriteLine("Passou");
                    string idToken = tokenResponse.id_token;
                    return idToken;
                }
                else
                {
                    Console.WriteLine($"Failed to get id_token: {responseString}");
                    return null;
                }
            }
        }
        
        public static async Task ResetUserPasswordAsync(string email, string newPassword, string oobCode)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key={ApiKey}";

            using (var client = new HttpClient())
            {
                var requestData = new
                {
                    oobCode = oobCode,         // Token do link de redefinição
                    newPassword = newPassword  // Nova senha fornecida pelo usuário
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