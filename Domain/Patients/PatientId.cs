using System;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class PatientId : EntityId
    {
        public PatientId(string value) : base(value)
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
    }
}