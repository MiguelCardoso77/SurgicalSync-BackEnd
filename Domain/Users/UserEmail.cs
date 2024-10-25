using System;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    /**
     * The UserEmail class represents the email of a user.
     */
    public class UserEmail : IValueObject
    {
        public string UserEmailValue { get; private set; }
        
        // Private constructor for EF Core
        private UserEmail() { }
        
        /**
         * Constructor for UserEmail
         * @param email
         * @throws FormatException if email is null, empty or has more than 99 characters.
         */
        public UserEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || email.Length >= 99)
            {
                throw new FormatException("User email must be a non-empty string with less than 99 characters.");
            }
            
            this.UserEmailValue = email;
        }

        /**
         * Returns the string representation of the UserEmail
         */
        public override string ToString()
        {
            return UserEmailValue;
        }

        /**
         * Compares the UserEmail with another object
         * @param obj
         * @return true if the objects are equal, false otherwise
         */
        public override bool Equals(object obj)
        {
            if (obj is UserEmail other)
            {
                return UserEmailValue == other.UserEmailValue;
            }
            return false;
        }

        /**
         * Returns the hash code of the UserEmail
         */
        public override int GetHashCode()
        {
            return UserEmailValue.GetHashCode();
        }
    }
}