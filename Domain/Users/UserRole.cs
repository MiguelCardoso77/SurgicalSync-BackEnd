namespace DDDNetCore.Domain.Users
{
    public class UserRole
    {
        public string RoleValue { get; private set; }
        
        public UserRole(string roleValue)
        {
            this.RoleValue = roleValue;
        }
    }
}