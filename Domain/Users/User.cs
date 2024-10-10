using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public UserId Id { get; private set; }
        public Username Username { get; private set; }
        public UserEmail UserEmail { get; private set; }
        public UserRole UserRole { get; private set; }
        public bool IsActive { get; private set; }
        
        private User()
        {
            this.Username = null;
            this.UserEmail = null;
            this.UserRole = UserRole.None;
        }
        
        public User(UserId id, Username username, UserEmail email, UserRole role)
        {
            this.Id = id;
            this.Username = username;
            this.UserEmail = email;
            this.UserRole = role;
            this.IsActive = true;
        }
        
        public void ChangeUserName(Username username)
        {
            this.Username = username;
        }
        
        public void ChangeUserRole(UserRole role)
        {
            this.UserRole = role;
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