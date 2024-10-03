using DDDNetCore.Domain.OperationTypes;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace DDDNetCore.Infraestructure.OperationTypes
{
    public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeId>, IOperationTypeRepository
    {
        public OperationTypeRepository(DDDSample1DbContext context):base(context.OperationTypes)
        {
            
        }
    }
}