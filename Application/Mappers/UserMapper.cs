using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Users;

namespace DDDSample1.Application.Mappers
{
    public class UserMapper
    {
        public static User ToDomain(UserDto dto, UserId userId)
        {
            return new User(userId, new Username(dto.UserName), new UserEmail(dto.UserEmail), new UserRole(dto.UserRole), new Password(dto.Password));
        }
        
        public static UserDto ToDto(User user)
        {
            return new UserDto { Id = user.Id.AsString(), UserName = user.Username.ToString(), UserEmail = user.UserEmail.ToString(),
                UserRole = user.UserRole.ToString(), Password = user.Password.ToString() };
        }
    }
}