namespace DDDNetCore.Domain.Users
{
    public class UserEmail
    {
        public string UserEmailValue { get; private set; }
        
        public UserEmail(string email)
        {
            this.UserEmailValue = email;
        }
    }
}