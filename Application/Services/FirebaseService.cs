using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace DDDNetCore.Application.Services
{
    public class FirebaseService
    {
        public static async Task<UserRecord> CreateUserRecordAsync(string email, string password)
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = email,
                Password = password,
            };
            
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
            Console.WriteLine($"Successfully created new user: {userRecord.Uid}");

            return userRecord;
        }
    }
}