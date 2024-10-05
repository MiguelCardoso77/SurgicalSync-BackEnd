using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public class RequiredStaff : IValueObject
    {
        public List<string> RequiredStaffList { get; private set; }

        private RequiredStaff()
        {
            RequiredStaffList = new List<string>();
        }
        
        public RequiredStaff(List<string> requiredStaff)
        {
            this.RequiredStaffList = requiredStaff;
        }
        
        public override string ToString()
        {
            return string.Join(", ", RequiredStaffList);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is RequiredStaff other)
            {
                return RequiredStaffList.SequenceEqual(other.RequiredStaffList);
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return RequiredStaffList != null ? string.Join(",", RequiredStaffList).GetHashCode() : 0;
        }
        
    }
}