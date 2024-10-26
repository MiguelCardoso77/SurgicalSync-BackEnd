namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) for handling login requests.
     */
    public class LoginDto
    {
        /**
         * Email address associated with the user's account.
         */
        public string Email { get; set; }
        
        /**
         * Password for the user's account.
         */
        public string Password { get; set; }
        
        /**
         * Specifies whether a secure token should be returned upon successful login.
         * Expected values are "true" or "false".
         */
        public string ReturnSecureToken { get; set; }
    }
}