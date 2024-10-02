namespace DDDNetCore.Domain.Users
{
    public class Password
    {
        public string PasswordValue { get; private set; }
        
        public Password(string password)
        {
            this.PasswordValue = password;
        }
        
    }
}