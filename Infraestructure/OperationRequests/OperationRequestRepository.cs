using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.OperationRequests
{
    public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestId>, IOperationRequestRepository
    {
        public OperationRequestRepository(SurgicalSyncContext context) : base(context.OperationRequests)
        {
            
        }
    }
}