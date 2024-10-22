namespace DDDNetCore.Application.DTO
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnSecureToken { get; set; }
    }
}