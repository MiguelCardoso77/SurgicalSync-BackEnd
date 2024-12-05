using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.RoomTypes;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.RoomTypes;

public class RoomTypeRepository : BaseRepository<RoomType, RoomTypeId>, IRoomTypeRepository
{
    public RoomTypeRepository(SurgicalSyncContext context):base(context.RoomTypes)
    {
            
    }
}