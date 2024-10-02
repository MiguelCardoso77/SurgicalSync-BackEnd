using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class UserEmail : IValueObject
    {
        public string UserEmailValue { get; private set; }
        
        private UserEmail() { }
        
        public UserEmail(string email)
        {
            this.UserEmailValue = email;
        }

        public override string ToString()
        {
            return UserEmailValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is UserEmail other)
            {
                return UserEmailValue == other.UserEmailValue;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UserEmailValue.GetHashCode();
        }
    }
}