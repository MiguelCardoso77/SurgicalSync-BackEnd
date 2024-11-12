using DDDNetCore.Domain.SurgeryRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = DDDNetCore.Domain.SurgeryRooms.Type;
using System;

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

            // Ajuste no mapeamento de Capacity
            builder.OwnsOne(s => s.Capacity, capacityBuilder =>
            {
                capacityBuilder.Property(p => p.MaxPatients)
                    .HasColumnName("MaxPatients")
                    .IsRequired();

                capacityBuilder.Property(p => p.MaxStaff)
                    .HasColumnName("MaxStaff")
                    .IsRequired();

                // Ignorar a propriedade Value pois é calculada e não necessita de persistência
                capacityBuilder.Ignore(p => p.Value);
            });

            builder.Property(s => s.Type)
                .HasConversion(
                    s => s.ToString(),
                    s => (Type)Enum.Parse(typeof(Type), s))
                .HasColumnName("RoomType")
                .IsRequired();
        }
    }
}
