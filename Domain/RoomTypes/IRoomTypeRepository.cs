using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.RoomTypes;

/**
 * Interface that defines the methods that a repository for operation types must implement.
 * It extends IRepository and specifies that the entity type is OperationType and the ID type is OperationTypeId.
 */
public interface IRoomTypeRepository : IRepository<RoomType, RoomTypeId>
{
    
}