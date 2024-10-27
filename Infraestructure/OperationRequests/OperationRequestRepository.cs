using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.OperationRequests
{
    public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestId>, IOperationRequestRepository
    {
        public OperationRequestRepository(DDDSample1DbContext context) : base(context.OperationRequests)
        {
            
        }
    }
}