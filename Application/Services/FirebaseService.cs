using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace DDDNetCore.Application.Services
{
    public class FirebaseService
    {
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
    }
}