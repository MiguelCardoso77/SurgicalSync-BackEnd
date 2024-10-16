using DDDNetCore.Domain.OperationRequests;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace DDDNetCore.Infraestructure.OperationRequests
{
    public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestId>, IOperationRequestRepository
    {
        public OperationRequestRepository(DDDSample1DbContext context) : base(context.OperationRequests)
        {
            
        }
    }
}