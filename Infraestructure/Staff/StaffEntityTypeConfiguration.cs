using System;
using DDDNetCore.Domain.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                    .HasConversion(
                        v => v,
                        v => v)
                    .HasColumnName("UserEmail")
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

            builder.Property(p => p.StaffSpecialization)
                .HasConversion(
                    b => b.ToString(),
                    b => (StaffSpecialization)Enum.Parse(typeof(StaffSpecialization), b))
                .HasColumnName("StaffSpecialization")
                .IsRequired();

            builder.OwnsOne(b => b.StaffAvaiabilitySlots, availabilityBuilder =>
            {
                availabilityBuilder.Property(b => b.Value)
                    .HasColumnName("AvailabilitySlots")
                    .HasConversion<string>();
            });
            
            builder.Property(b => b.StaffType)
                .HasColumnName("Type")
                .HasConversion<string>();

            builder.OwnsOne(b => b.StaffLicenseNumber, licenseNumberBuilder =>
            {
                licenseNumberBuilder.Property(p => p.Value)
                    .HasColumnName("LicenseNumber")
                    .HasConversion<string>();
            });

        }
        
    }
}