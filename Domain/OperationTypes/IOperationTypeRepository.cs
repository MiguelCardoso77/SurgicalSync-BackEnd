using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public interface IOperationTypeRepository:IRepository<OperationType,OperationTypeId>
    {
        
    }
}