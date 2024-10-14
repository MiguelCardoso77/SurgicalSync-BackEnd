using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public interface IStaffRepository:IRepository<Staff,LicenseNumber>
    {
        
    }
}