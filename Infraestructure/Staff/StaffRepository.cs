using DDDNetCore.Domain.Staffs;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.Staff
{
    public class StaffRepository : BaseRepository<Domain.Staffs.Staff, StaffId>, IStaffRepository
    {
        public StaffRepository(DDDSample1DbContext context) : base(context.Staffs)
        {
            
        }
    
    }
}