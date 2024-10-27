using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.OperationType
{
    /**
     * Interface that defines the methods that a repository for operation types must implement.
     * It extends IRepository and specifies that the entity type is OperationType and the ID type is OperationTypeId.
     */
    public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeId>
    {
    }
}