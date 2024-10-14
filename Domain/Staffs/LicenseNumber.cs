using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs
{
    public class LicenseNumber : EntityId
    {
        
        public LicenseNumber(string value) : base(value)
        {
        }
        
        override 
            protected Object createFromString(String text)
        {
            return text;
        }

        override
            public string AsString()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
