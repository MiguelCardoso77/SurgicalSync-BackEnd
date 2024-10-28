using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.OperationType
{
    /**
     * The EstimatedDuration class represents the estimated duration for a phase of an operation.
     */
    public class EstimatedDuration : IValueObject<string>
    {
        public string Value { get; private set; }
        
        // Private constructor for EF Core
        private EstimatedDuration() { }
        
        /**
         * Constructor for EstimatedDuration
         * @param estimatedDuration
         * @throws FormatException if estimatedDuration is not a positive integer.
         */
        public EstimatedDuration(string estimatedDuration)
        {
            if (!int.TryParse(estimatedDuration, out var parsedDuration) || parsedDuration <= 0)
            {
                throw new FormatException("Estimated duration must be a positive integer.");
            }
            
            this.Value = estimatedDuration;
        }
        
        /**
         * Returns the string representation of the EstimatedDuration
         */
        public override string ToString()
        {
            return Value.ToString();
        }
        
        /**
         * Compares the EstimatedDuration with another object
         * @param obj
         * @return true if the objects are equal, false otherwise
         */
        public override bool Equals(object obj)
        {
            if (obj is EstimatedDuration other)
            {
                return Value == other.Value;
            }
            return false;
        }
        
        /**
         * Returns the hash code of the EstimatedDuration
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        
    }
}