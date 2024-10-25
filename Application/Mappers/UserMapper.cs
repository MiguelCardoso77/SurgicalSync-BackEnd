using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Mappers
{
    /**
     * Mapper class to convert between User domain model and UserDto.
     */
    public class UserMapper
    {
        /**
         * Converts the input data to a domain User object.
         */
        public User ToDomain(UserDto dto, UserId userId)
        {
            return new User(userId, new Username(dto.UserName), new UserEmail(dto.UserEmail), Enum.Parse<UserRole>(dto.UserRole));
        }
        
        /**
         * Converts a domain User object to an UserDto type object.
         */
        public UserDto ToDto(User user)
        {
            return new UserDto { Id = user.Id.AsString(), 
                UserName = user.Username.ToString(), 
                UserEmail = user.UserEmail.ToString(),
                UserRole = user.UserRole.ToString() };
        }

        /**
         * Converts a list of domain User objects to a list of UserDto objects.
         * @param domainList a list of User domain objects to be converted.
         * @return a list of UserDto objects corresponding to the provided domain objects.
         */
        public List<UserDto> ToDtoList(List<User> domainList)
        {
            return domainList == null ? new List<UserDto>() : domainList.Select(ToDto).ToList();
        }
    }
}