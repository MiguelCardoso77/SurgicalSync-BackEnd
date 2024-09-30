using System;

namespace Domain.Users
{
    public class UserDto
    {
        public String Id { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String Password { get; set; }
    }
}