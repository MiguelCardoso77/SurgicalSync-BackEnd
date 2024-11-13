using DDDNetCore.Domain.Appointments;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.Appointments
{
    public class AppointmentRepository : BaseRepository<Appointment, AppointmentId>, IAppointmentsRepository
    {
        public AppointmentRepository(SurgicalSyncContext context) : base(context.Appointments)
        {
            
        }
    }
}