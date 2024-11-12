using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.SurgeryRooms
{
    public interface ISurgeryRoomsRepository : IRepository<SurgeryRoom, RoomNumber>
    {
    
    }
}