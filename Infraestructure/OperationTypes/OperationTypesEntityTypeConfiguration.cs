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

            // Use HasConversion for Id
            builder.Property(b => b.Id)
                .HasConversion(
                    b => b.AsString(),
                    b => new OperationTypeId(b))
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure owned Name value object with HasConversion
            builder.OwnsOne(b => b.Name, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value)
                    .HasConversion(
                        v => v,
                        v => v)
                    .HasColumnName("OperationTypeName")
                    .IsRequired();
            });

            // Configure the multiple owned RequiredStaff value objects with HasConversion
            builder.OwnsOne(b => b.RequiredStaff, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value)
                    .HasConversion(
                        v => v,
                        v => v)
                    .HasColumnName("RequiredStaff")
                    .IsRequired();
            });
            
            // Configure the multiple owned EstimatedDuration value objects with HasConversion
            builder.OwnsOne(b => b.EstimatedDuration, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value)
                    .HasConversion(
                        v => v,
                        v => v)
                    .HasColumnName("EstimatedDuration")
                    .IsRequired();
            });
            
        }
    }
}
