using System;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Domain.Staffs
{
    public interface IStaffRepository:IRepository<Staff,StaffId>
    {

        
    }
}