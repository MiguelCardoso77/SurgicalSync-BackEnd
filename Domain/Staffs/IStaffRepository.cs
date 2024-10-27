using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Interface for the Staff Repository.
     * This repository is responsible for defining the methods for accessing and
     * manipulating Staff entities in the underlying data store.
     *
     * @typeparam Staff        The entity that the repository will handle.
     * @typeparam StaffId      The identifier type of the Staff entity.
     */
    public interface IStaffRepository : IRepository<Staff, StaffId>
    {
    }
}