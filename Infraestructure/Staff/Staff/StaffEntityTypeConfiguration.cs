using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DDDNetCore.Infraestructure.Staff
{
    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Staffs.Staff>
    {
        public void Configure(EntityTypeBuilder<Domain.Staffs.Staff> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Id).HasConversion(
                b => b.ToString(),
                b => new StaffId(b))
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.OwnsOne(b => b.StaffName, nameBuilder =>
            {
                nameBuilder.Property(b => b.Value)
                    .HasConversion<string>()
                    .HasColumnName("StaffName")
                    .IsRequired();
            });
            
           
            builder.OwnsOne(b => b.UserEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.Value)
                    .HasColumnName("UserEmail")
                    .HasConversion<string>()
                    .IsRequired();
            });
            
            builder.OwnsOne(b => b.StaffPhoneNumber, phoneNumberBuilder =>
            {
                phoneNumberBuilder.Property(p => p.Value)
                    .HasConversion(
                        v => v,
                        v => v)
                    .HasColumnName("PhoneNumber")
                    .IsRequired();
            });

            builder.OwnsOne(b => b.StaffSpecialization, nameBuilder =>
            {
                nameBuilder.Property(b => b.Value)
                    .HasConversion<string>()
                    .HasColumnName("StaffSpecialization")
                    .IsRequired();
            });
          
           builder.OwnsOne(b => b.StaffAvailabilitySlots, StaffAvailabilitySlotsBuilder =>
           {
               StaffAvailabilitySlotsBuilder.Property(p => p.Value)
                   .HasColumnName("StaffAvailabilitySlots")
                   .IsRequired();
           });
        
           builder.Property(p => p.StaffType)
               .HasConversion(
                   b => b.ToString(),
                   b => (StaffType)Enum.Parse(typeof(StaffType), b))
               .HasColumnName("StaffType")
               .IsRequired();

            builder.OwnsOne(b => b.StaffLicenseNumber, licenseNumberBuilder =>
            {
                licenseNumberBuilder.Property(p => p.Value)
                    .HasColumnName("StaffLicenseNumber")
                    .HasConversion<string>()
                    .IsRequired();
            });
            
            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

        }
        
        private static string SerializeAvailabilitySlots(List<StaffAvailabilitySlots> slots)
        {
            var slotValues = slots.Select(slot => slot.Value).ToList(); // Extrai apenas os valores das instâncias
            return JsonSerializer.Serialize(slotValues); // Serializa a lista de valores em JSON
        }

        private static List<StaffAvailabilitySlots> DeserializeAvailabilitySlots(string json)
        {
            var slotValues = JsonSerializer.Deserialize<List<string>>(json); // Desserializa JSON em lista de strings
            return slotValues.Select(value => new StaffAvailabilitySlots(value)).ToList(); // Converte strings para AvailabilitySlot
        }

    }
}