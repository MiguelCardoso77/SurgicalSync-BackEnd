using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    /**
     * The Username class represents the username of a user.
     */
    public class Username : IValueObject
    {
        public string UsernameValue { get; private set; }
        
        // Private constructor for EF Core
        private Username() { }
        
        /**
         * Constructor for Username
         * @param username
         * @throws FormatException if username is null, empty or has more than 99 characters.
         */
        public Username(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length >= 99)
            {
                throw new FormatException("Username must be a non-empty string with less than 99 characters.");
            }
            
            this.UsernameValue = username;
        }
        
        /**
         * Returns the string representation of the Username
         */
        public override string ToString()
        {
            return UsernameValue;
        }
        
        /**
         * Compares the Username with another object
         * @param obj
         * @return true if the objects are equal, false otherwise
         */
        public override bool Equals(object obj)
        {
            if (obj is Username other)
            {
                return UsernameValue == other.UsernameValue;
            }
            return false;
        }

        /**
         * Returns the hash code of the Username
         */
        public override int GetHashCode()
        {
            return UsernameValue.GetHashCode();
        }
    }
}