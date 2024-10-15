using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationRequests
{
    public interface IOperationRequestRepository: IRepository<OperationRequest, OperationRequestId>
    {
        
    }
}