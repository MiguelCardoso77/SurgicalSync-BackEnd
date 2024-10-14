using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Staffs;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace DDDNetCore.Infraestructure.Staffs
{
    public class StaffRepository : BaseRepository<Staff, LicenseNumber>, IStaffRepository
    {
        public StaffRepository(DDDSample1DbContext context) : base(context.Staffs)
        {
            
        }
    
    }
}