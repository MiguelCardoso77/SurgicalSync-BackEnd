using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    /**
     * The Username class represents the username of a user.
     */
    public class Username : IValueObject<string>
    {
        public string Value { get; private set; }
        
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
            
            this.Value = username;
        }
        
        /**
         * Returns the string representation of the Username
         */
        public override string ToString()
        {
            return Value;
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
                return Value == other.Value;
            }
            return false;
        }

        /**
         * Returns the hash code of the Username
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}