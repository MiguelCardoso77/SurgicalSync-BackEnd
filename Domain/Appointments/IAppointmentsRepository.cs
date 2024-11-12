using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Appointments
{
    public interface IAppointmentsRepository : IRepository<Appointment, AppointmentId>
    {
    
    }
}