using System;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationType
{
    /**
     * The OperationName class represents the name of an operation type.
     */
    public class OperationName : IValueObject
    {
        public string OperationNameValue { get; private set; }
        
        // Private constructor for EF Core
        private OperationName() { }
        
        /**
         * Constructor for OperationName
         * @param operationName
         * @throws FormatException if operationName is null, empty or has more than 99 characters.
         */
        public OperationName(string operationName)
        {
            if (string.IsNullOrWhiteSpace(operationName) || operationName.Length >= 99)
            {
                throw new FormatException("Operation name must be a non-empty string with less than 99 characters.");
            }
            
            this.OperationNameValue = operationName;
        }
        
        /**
         * Returns the string representation of the OperationName
         */
        public override string ToString()
        {
            return OperationNameValue;
        }
        
        /**
         * Compares the OperationName with another object
         * @param obj
         * @return true if the objects are equal, false otherwise
         */
        public override bool Equals(object obj)
        {
            if (obj is OperationName other)
            {
                return OperationNameValue == other.OperationNameValue;
            }
            return false;
        }
        
        /**
         * Returns the hash code of the OperationName
         */
        public override int GetHashCode()
        {
            return OperationNameValue.GetHashCode();
        }
        
    }
}