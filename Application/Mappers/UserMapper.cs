using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Mappers
{
    public class UserMapper
    {
        public static User ToDomain(UserDto dto, UserId userId)
        {
            return new User(userId, new Username(dto.UserName), new UserEmail(dto.UserEmail), new UserRole(dto.UserRole));
        }
        
        public static UserDto ToDto(User user)
        {
            return new UserDto { Id = user.Id.AsString(), UserName = user.Username.ToString(), UserEmail = user.UserEmail.ToString(),
                UserRole = user.UserRole.ToString() };
        }
    }
}