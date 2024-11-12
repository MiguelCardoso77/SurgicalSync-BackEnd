using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.SurgeryRooms;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infraestructure.Appointments;
using DDDNetCore.Infraestructure.OperationRequests;
using DDDNetCore.Infraestructure.OperationTypes;
using DDDNetCore.Infraestructure.Patients;
using DDDNetCore.Infraestructure.Staff;
using DDDNetCore.Infraestructure.SurgeryRooms;
using DDDNetCore.Infraestructure.Users;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infraestructure
{
    public class SurgicalSyncContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Domain.Staffs.Staff> Staffs { get; set; }
        public DbSet<OperationRequest> OperationRequests { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        
        public DbSet<SurgeryRoom> SurgeryRooms { get; set; }
        
        public SurgicalSyncContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OperationTypesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SurgeryRoomEntityTypeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}