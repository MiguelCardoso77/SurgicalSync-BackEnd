using DDDNetCore.Domain.Users;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace DDDNetCore.Infraestructure.Users
{
    /**
     * Class that represents the repository of the users.
     */
    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        public UserRepository(DDDSample1DbContext context):base(context.Users)
        {
            
        }
    }
}