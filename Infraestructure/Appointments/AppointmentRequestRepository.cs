using DDDNetCore.Domain.Appointments;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.Appointments
{
    public class AppointmentRequestRepository : BaseRepository<Appointment, AppointmentId>, IAppointmentsRepository
    {
        public AppointmentRequestRepository(SurgicalSyncContext context) : base(context.Appointments)
        {
            
        }
    }
}