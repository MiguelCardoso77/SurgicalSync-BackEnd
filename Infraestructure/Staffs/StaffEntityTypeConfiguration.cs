using DDDNetCore.Domain.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.Staffs
{
    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.OwnsOne(b => b.StaffName, nameBuilder =>
            {
                nameBuilder.Property(p => p.StaffNameValue)
                    .HasColumnName("StaffName");
            });
            
            builder.OwnsOne(b => b.StaffEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.StaffEmailValue)
                    .HasColumnName("StaffEmail");
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




        }
        
    }
}