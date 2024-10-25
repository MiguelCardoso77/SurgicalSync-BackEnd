using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    /**
     * Interface that defines the methods that a repository for operation types must implement.
     * It extends IRepository and specifies that the entity type is User and the ID type is UserId.
     */
    public interface IUserRepository : IRepository<User, UserId>
    {
    }
}