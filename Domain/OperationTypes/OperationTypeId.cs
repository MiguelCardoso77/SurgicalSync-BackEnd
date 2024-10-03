using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public class OperationTypeId : EntityId
    {
        public OperationTypeId(string value) : base(value)
        {
        }
        
        protected override object createFromString(string text)
        {
            return text;
        }
        
        public override string AsString()
        {
            return (string)Value;
        }
        
    }
}