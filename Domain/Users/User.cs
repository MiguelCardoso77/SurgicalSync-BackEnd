using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public string UserEmail { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }
        
        private User()
        {
            this.UserEmail = "";
            this.UserName = "";
            this.Password = "";
        }
        
        public User(string userEmail, string userName, string password)
        {
            this.Id = new UserId(userEmail);
            this.UserEmail = userEmail;
            this.UserName = userName;
            this.Password = password;
        }
        
        public void ChangeUserName(string userName)
        {
            this.UserName = userName;
        }
        
        public void ChangePassword(string password)
        {
            this.Password = password;
        }
        
        public void ActivateUser()
        {
            this.IsActive = true;
        }
        
        public void DeactivateUser()
        {
            this.IsActive = false;
        }
    }
}