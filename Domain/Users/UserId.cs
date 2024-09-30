using System;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class UserId : EntityId
    {
        public UserId(string value) : base(value)
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