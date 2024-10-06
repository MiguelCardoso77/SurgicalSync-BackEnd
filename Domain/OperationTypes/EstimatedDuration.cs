using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public class EstimatedDuration : IValueObject
    {
        public string EstimatedDurationValue { get; private set; }
        
        private EstimatedDuration() { }
        
        public EstimatedDuration(string estimatedDuration)
        {
            this.EstimatedDurationValue = estimatedDuration;
        }
        
        public override string ToString()
        {
            return EstimatedDurationValue;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is EstimatedDuration other)
            {
                return EstimatedDurationValue == other.EstimatedDurationValue;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return EstimatedDurationValue != null ? EstimatedDurationValue.GetHashCode() : 0;
        }
        
    }
}