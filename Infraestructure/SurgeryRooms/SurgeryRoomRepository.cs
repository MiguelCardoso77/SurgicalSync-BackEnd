using DDDNetCore.Domain.SurgeryRooms;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.SurgeryRooms
{
    public class SurgeryRoomRepository : BaseRepository<SurgeryRoom, RoomNumber>, ISurgeryRoomsRepository
    {
        public SurgeryRoomRepository(SurgicalSyncContext context) : base(context.SurgeryRooms)
        {
            
        }
    }
}