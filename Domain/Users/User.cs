using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    /**
     * The User class represents a user in the system.
     * It is an aggregate root in the domain model, responsible for handling
     * user properties such as username, email, role, and activation status.
     */
    public class User : Entity<UserId>, IAggregateRoot
    {
        public new UserId Id { get; private set; }
        public Username Username { get; private set; }
        public UserEmail UserEmail { get; private set; }
        public UserRole UserRole { get; private set; }
        public bool IsActive { get; private set; }
        
        // Private constructor for EF
        private User()
        {
            this.Username = null;
            this.UserEmail = null;
            this.UserRole = UserRole.None;
            this.IsActive = false;
        }
        
        /**
         * Constructor that initializes the User with a unique identifier, username, email, and role.
         * The user is activated by default.
         * @param id The unique identifier for the user.
         * @throws ArgumentNullException if any of the parameters are null.
         */
        public User(UserId id, Username username, UserEmail email, UserRole role)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id), "UserId cannot be null.");
            this.Username = username ?? throw new ArgumentNullException(nameof(username), "Username cannot be null.");
            this.UserEmail = email ?? throw new ArgumentNullException(nameof(email), "UserEmail cannot be null.");
            this.UserRole = role;
            this.IsActive = true;
        }
        
        /**
         * Method that changes the username of the User.
         */
        public void ChangeUserName(Username username)
        {
            this.Username = username;
        }

        /**
         * Method that changes the email of the User.
         */
        public void ChangeUserEmail(UserEmail userEmail)
        {
            this.UserEmail = userEmail;
        }
        
        /**
         * Method that changes the role of the User.
         */
        public void ChangeUserRole(UserRole role)
        {
            this.UserRole = role;
        }
        
        /**
         * Method that activates the User.
         */
        public void ActivateUser()
        {
            this.IsActive = true;
        }
        
        /**
         * Method that deactivates the User.
         */
        public void DeactivateUser()
        {
            this.IsActive = false;
        }
    }
}