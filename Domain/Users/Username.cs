using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Username : IValueObject
    {
        public string UsernameValue { get; private set; }
        
        private Username() { }
        
        public Username(string username)
        {
            this.UsernameValue = username;
        }
        
        public override string ToString()
        {
            return UsernameValue;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Username other)
            {
                return UsernameValue == other.UsernameValue;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UsernameValue.GetHashCode();
        }
    }
}