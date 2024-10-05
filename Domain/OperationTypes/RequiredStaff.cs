using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public class RequiredStaff : IValueObject
    {
        public string RequiredStaffValue { get; private set; }

        private RequiredStaff() { }
        
        public RequiredStaff(string requiredStaff)
        {
            this.RequiredStaffValue = requiredStaff;
        }

        public override string ToString()
        {
            return RequiredStaffValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is RequiredStaff other)
            {
                return RequiredStaffValue == other.RequiredStaffValue;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return RequiredStaffValue != null ? RequiredStaffValue.GetHashCode() : 0;
        }
        
    }
}