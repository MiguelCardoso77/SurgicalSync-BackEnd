using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationRequests
{
    // Represents a unique identifier for an operation request in the domain.
    // This class extends EntityId and is used to ensure that each operation request 
    // has a distinct and properly formatted identifier. 
    // It provides methods for creating an ID from a string and retrieving the 
    // ID value as a string.
    public class OperationRequestId : EntityId
    {
        // Constructor that initializes the OperationRequestId with a string value.
        public OperationRequestId(string value) : base(value)
        {
            
        }
        // Protected method that creates the ID from a string.
        // In this case, it simply returns the passed string.
        protected override object createFromString(string text)
        {
            return text;
        }
        // Method that returns the value of OperationRequestId as a string.
        public override string AsString()
        {
            return (string)Value;
        }
    }
}