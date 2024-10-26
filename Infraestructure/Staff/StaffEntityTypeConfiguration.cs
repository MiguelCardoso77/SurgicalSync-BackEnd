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
            
            builder.OwnsOne(b => b.StaffName, nameBuilder =>
            {
                nameBuilder.Property(p => p.StaffNameValue)
                    .HasColumnName("StaffName");
            });
            
            builder.OwnsOne(b => b.UserEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.UserEmailValue)
                    .HasColumnName("UserEmail");
            });
            
            builder.OwnsOne(b => b.StaffPhoneNumber, phoneNumberBuilder =>
            {
                phoneNumberBuilder.Property(p => p.StaffPhoneNumberValue)
                    .HasColumnName("PhoneNumber");
            });

            builder.Property(b => b.StaffSpecialization)
                .HasColumnName("StaffSpecialization")
                .HasConversion<string>();
            


            builder.OwnsMany(b => b.StaffAvaiabilitySlots, avaiabilitySlotsBuilder =>
            {
                avaiabilitySlotsBuilder.Property(p => p.StaffAvaiabilitySlotsValue)
                    .HasColumnName("StaffAvaiabilitySlots");
            });
            
            builder.Property(b => b.StaffType)
                .HasColumnName("StaffType")
                .HasConversion<string>();

            builder.OwnsOne(b => b.StaffLicenseNumber, licenseNumberBuilder =>
            {
                licenseNumberBuilder.Property(p => p.StaffLicenseNumberValue)
                    .HasColumnName("StaffLicenseNumber");
            });



        }
        
    }
}