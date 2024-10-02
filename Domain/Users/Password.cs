using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Password : IValueObject
    {
        public string PasswordValue { get; private set; }
        
        private Password() { }
        
        public Password(string password)
        {
            this.PasswordValue = password;
        }
        
        public override string ToString()
        {
            return PasswordValue;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Password other)
            {
                return PasswordValue == other.PasswordValue;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return PasswordValue.GetHashCode();
        }
        
    }
}