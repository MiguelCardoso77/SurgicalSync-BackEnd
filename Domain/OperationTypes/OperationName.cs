using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public class OperationName : IValueObject
    {
        public string OperationNameValue { get; private set; }
        
        private OperationName() { }
        
        public OperationName(string operationName)
        {
            this.OperationNameValue = operationName;
        }
        
        public override string ToString()
        {
            return OperationNameValue;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is OperationName other)
            {
                return OperationNameValue == other.OperationNameValue;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return OperationNameValue.GetHashCode();
        }
        
    }
}