using DDDNetCore.Domain.Users;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.Users
{
    /**
     * Class that represents the repository of the users.
     */
    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        public UserRepository(SurgicalSyncContext context):base(context.Users)
        {
            
        }
    }
}