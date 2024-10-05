using DDDSample1.Domain.OperationTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.OperationTypes
{
    internal class OperationTypesEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.OwnsOne(b => b.Name, nameBuilder =>
            {
                nameBuilder.Property(p => p.OperationNameValue)
                    .HasColumnName("Name");
            });
            
            builder.OwnsOne(b => b.RequiredStaff, staffBuilder =>
            {
                staffBuilder.Property(p => p.RequiredStaffList)
                    .HasColumnName("RequiredStaff");
            });
            
            builder.OwnsOne(b => b.EstimatedDuration, durationBuilder =>
            {
                durationBuilder.Property(p => p.EstimatedDurationValue)
                    .HasColumnName("EstimatedDuration");
            });
        }
        
    }
}