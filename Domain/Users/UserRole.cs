using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class UserRole : IValueObject
    {
        public string RoleValue { get; private set; }
        
        private UserRole() { }
        
        public UserRole(string roleValue)
        {
            this.RoleValue = roleValue;
        }

        public override string ToString()
        {
            return RoleValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is UserRole other)
            {
                return RoleValue == other.RoleValue;
            }

            return false;
        }
        
        public override int GetHashCode()
        {
            return RoleValue.GetHashCode();
        }
    }
}