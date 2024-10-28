using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.OperationType
{
    /**
     * The RequiredStaff class represents the required staff for a phase of an operation.
     */
    public class RequiredStaff : IValueObject<string>
    {
        public string Value { get; private set; }

        // Private constructor for EF Core
        private RequiredStaff() { }
        
        /**
         * Constructor for RequiredStaff
         * @param requiredStaff
         * @throws FormatException if requiredStaff is null, empty or has more than 99 characters.
         */
        public RequiredStaff(string requiredStaff)
        {
            if (string.IsNullOrWhiteSpace(requiredStaff) || requiredStaff.Length >= 99)
            {
                throw new FormatException("Staff must be a non-empty string with less than 99 characters.");
            }
            
            this.Value = requiredStaff;
        }

        /**
         * Returns the string representation of the RequiredStaff
         */
        public override string ToString()
        {
            return Value;
        }

        /**
         * Compares the RequiredStaff with another object
         * @param obj
         * @return true if the objects are equal, false otherwise
         */
        public override bool Equals(object obj)
        {
            if (obj is RequiredStaff other)
            {
                return Value == other.Value;
            }
            return false;
        }

        /**
         * Returns the hash code of the RequiredStaff
         */
        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
        
    }
}