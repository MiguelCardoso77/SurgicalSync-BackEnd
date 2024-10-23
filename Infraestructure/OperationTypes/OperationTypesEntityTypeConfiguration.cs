using DDDNetCore.Domain.OperationType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.OperationTypes
{
    /**
     * Configures the entity properties and relationships for the <see cref="OperationType"/> entity.
     * This class implements the <IEntityTypeConfiguration/> interface to define
     * how the <OperationType/> entity maps to the database schema.
     */
    internal class OperationTypesEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        /**
         * Configures the entity of type <OperationType/> using the provided <EntityTypeBuilder/>.
         * This method sets up the primary key, properties, and relationships with other entities.
         * <param name="builder">The <EntityTypeBuilder/> used to configure the entity.</param>
         */
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            // Primary key configuration
            builder.HasKey(b => b.Id);

            // Configure owned Name value object
            builder.OwnsOne(b => b.Name, nameBuilder =>
            {
                nameBuilder.Property(p => p.OperationNameValue)
                    .HasColumnName("Name");
            });

            // Configure the multiple owned RequiredStaff value objects
            builder.OwnsMany(b => b.RequiredStaff, staffBuilder =>
            {
                staffBuilder.Property(p => p.RequiredStaffValue)
                    .HasColumnName("RequiredStaff");
            });

            // Configure the multiple owned EstimatedDuration value objects
            builder.OwnsMany(b => b.EstimatedDuration, durationBuilder =>
            {
                durationBuilder.Property(p => p.EstimatedDurationValue)
                    .HasColumnName("EstimatedDuration");
            });
        }
    }
}