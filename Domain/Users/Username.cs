namespace DDDNetCore.Domain.Users
{
    public class Username
    {
        public string UsernameValue { get; private set; }
        
        public Username(string username)
        {
            this.UsernameValue = username;
        }
    }
}