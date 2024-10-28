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

            builder.Property(b => b.StaffName)
                .HasConversion(
                    v => v,
                    v => v)
                .HasColumnName("StaffName")
                .IsRequired();
            
            builder.OwnsOne(b => b.StaffName, nameBuilder =>
            {
                builder.Property(b => b.StaffName)
                    .HasConversion(
                        v => v,
                        v => v)
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

            builder.OwnsMany(b => b.StaffAvaiabilitySlots, StaffAvaiabilitySlots =>
            {
                builder.Property(b => b.StaffAvaiabilitySlots)
                    .HasColumnName("StaffSpecialization")
                    .HasConversion<string>();
            });
            
            builder.Property(b => b.StaffType)
                .HasColumnName("StaffType")
                .HasConversion<string>();

            builder.OwnsOne(b => b.StaffLicenseNumber, licenseNumberBuilder =>
            {
                licenseNumberBuilder.Property(p => p.Value)
                    .HasColumnName("StaffLicenseNumber")
                    .HasConversion<string>();
            });

        }
        
    }
}