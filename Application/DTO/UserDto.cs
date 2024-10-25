namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) for a user.
     * Represents the data needed for communication between the application and external layers.
     */
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
    }
}