using System;
using System.Linq;

namespace DDDNetCore.Application.Services
{
    public class PasswordService
    {
        public static string GeneratePassword()
        {
            const int passwordLength = 10;
            const string digits = "0123456789";
            const string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            const string allCharacters = digits + capitalLetters + specialCharacters + "abcdefghijklmnopqrstuvwxyz";
            
            var random = new Random();
            var password = new char[passwordLength];
            
            // Ensure at least one digit
            password[0] = digits[random.Next(digits.Length)];
            // Ensure at least one capital letter
            password[1] = capitalLetters[random.Next(capitalLetters.Length)];
            // Ensure at least one special character
            password[2] = specialCharacters[random.Next(specialCharacters.Length)];
            
            // Fill the remaining characters randomly
            for (var i = 3; i < passwordLength; i++)
            {
                password[i] = allCharacters[random.Next(allCharacters.Length)];
            }
            
            // Shuffle the password to ensure randomness
            return new string(password.OrderBy(c => random.Next()).ToArray());
        }
    }
}