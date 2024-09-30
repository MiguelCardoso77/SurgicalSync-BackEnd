using System;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class UserId : EntityId
    {
        public UserId(string value) : base(value)
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