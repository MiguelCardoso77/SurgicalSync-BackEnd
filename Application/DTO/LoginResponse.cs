namespace DDDNetCore.Application.DTO
{
    /**
     * Represents the response data received after a successful login attempt.
     */
    public class LoginResponse
    {
        /**
         * Type or kind of the response, often used to distinguish between different types of responses.
         */
        public string Kind { get; set; }
        
        /**
         * Unique identifier for the user in the system.
         */
        public string LocalId { get; set; }
        
        /**
         * Email address associated with the user's account.
         */
        public string Email { get; set; }

        /**
         * Display name of the user.
         */
        public string DisplayName { get; set; }

        /**
         * Token used to authenticate the user in subsequent requests.
         */
        public string IdToken { get; set; }
        
        /**
         * Indicates whether the user is already registered.
         */
        public bool Registered { get; set; }
        
        /**
         * Token used to refresh the IdToken when it expires.
         */
        public string RefreshToken { get; set; }
        
        /**
         * Expiration time for the IdToken, in seconds.
         */
        public string ExpiresIn { get; set; }
    }
}