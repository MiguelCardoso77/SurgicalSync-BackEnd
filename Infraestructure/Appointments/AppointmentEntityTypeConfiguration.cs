using System;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.SurgeryRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DDDNetCore.Infraestructure.Appointments
{
    internal class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), "The builder cannot be null!");
            }
            
            // Primary key configuration
            
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Id).HasConversion(
                a => a.AsString(),
                a => new AppointmentId(a))
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            // Configure owned Status value object
            
            builder.Property(a => a.Status)
                .HasConversion(
                    a => a.ToString(),
                    a => (Status)Enum.Parse(typeof(Status), a))
                .HasColumnName("Status")
                .IsRequired();
            
            // Configure owned Date value object

            builder.Property(a => a.Date)
                .HasConversion(
                    d => d.DateTime,
                    d => new Date(d))
                .HasColumnName("Date")
                .IsRequired();
            
            // Configure owned Time value object
            
            builder.Property(a => a.Time)
                .HasConversion(
                    t => t.Value,            
                    v => new Time(v))         
                .HasColumnName("TimeInMinutes")
                .IsRequired(); 
            
            // Configure foreign key for SurgeryRoom
            
            builder.HasOne<SurgeryRoom>()
                .WithMany()
                .HasForeignKey(a => a.RoomNumber)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}