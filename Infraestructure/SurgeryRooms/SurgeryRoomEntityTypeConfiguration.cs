using DDDNetCore.Domain.SurgeryRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = DDDNetCore.Domain.SurgeryRooms.Type;
using System;
using DDDNetCore.Domain.RoomTypes;

namespace DDDNetCore.Infraestructure.SurgeryRooms
{
    internal class SurgeryRoomEntityTypeConfiguration : IEntityTypeConfiguration<SurgeryRoom>
    {
        public void Configure(EntityTypeBuilder<SurgeryRoom> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    s => s.AsString(),
                    s => new RoomNumber(s))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.OwnsOne(s => s.MaintenanceSlots, maintenanceSlotsBuilder =>
            {
                maintenanceSlotsBuilder.Property(p => p.Value)
                    .HasColumnName("MaintenanceSlots")
                    .IsRequired();
            });

            builder.Property(s => s.CurrentStatus)
                .HasConversion(
                    s => s.ToString(),
                    s => (CurrentStatus)Enum.Parse(typeof(CurrentStatus), s))
                .HasColumnName("CurrentStatus")
                .IsRequired();

            builder.OwnsOne(s => s.AssignedEquipment, assignedEquipmentBuilder =>
            {
                assignedEquipmentBuilder.Property(p => p.Value)
                    .HasColumnName("AssignedEquipment")
                    .IsRequired();
            });

            builder.OwnsOne(s => s.Capacity, capacityBuilder =>
            {
                capacityBuilder.Property(p => p.MaxPatients)
                    .HasColumnName("MaxPatients")
                    .IsRequired();

                capacityBuilder.Property(p => p.MaxStaff)
                    .HasColumnName("MaxStaff")
                    .IsRequired();

                capacityBuilder.Ignore(p => p.Value);
            });

            builder.HasOne<RoomType>()
                .WithMany()
                .HasForeignKey(s => s.Type)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
