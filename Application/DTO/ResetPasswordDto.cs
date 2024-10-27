using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.DTO
{
    public class ResetPasswordDto
    {
        public string UserEmail { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
    
    public class ForgotPasswordDto
    {
        public string UserEmail { get; set; }
    }

}