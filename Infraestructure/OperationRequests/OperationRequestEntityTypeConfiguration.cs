using System;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.OperationRequests
{
    /**
     * Configures the entity properties and relationships for the <see cref="OperationRequest"/> entity.
     * This class implements the <IEntityTypeConfiguration/> interface to define
     * how the <OperationRequest/> entity maps to the database schema.
     */
    
    internal class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest>
    {
        /**
         * Configures the entity of type <OperationRequest/> using the provided <EntityTypeBuilder/>.
         * This method sets up the primary key, properties, and relationships with other entities.
         * <param name="builder">The <EntityTypeBuilder/> used to configure the entity.</param>
         */
        public void Configure(EntityTypeBuilder<OperationRequest> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), "The builder cannot be null.");
            }
            
            // Primary key configuration
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasConversion(
                b => b.ToString(),
                b => new OperationRequestId(b)).
                IsRequired().
                ValueGeneratedOnAdd();

            // Configure owned Priority value object
            builder.Property(b => b.Priority)
                .HasConversion(
                    b => b.ToString(),
                    b => (Priority)Enum.Parse(typeof(Priority), b))
                .HasColumnName("Priority")
                .IsRequired();
            
            // Configure owned DeadlineDate value object
            builder.OwnsOne(b => b.DeadlineDate, deadlineDateBuilder =>
            {
                deadlineDateBuilder.Property(p => p.DateTime)
                    .HasColumnName("DeadlineDate")
                    .HasConversion<string>()
                    .IsRequired();
            });

            builder.OwnsOne(b => b.MedicalRecordNumber, medicalRecordBuilder =>
            {
                medicalRecordBuilder.Property(p => p.Value)
                    .HasColumnName("MedicalRecordNumber")
                    .HasConversion(
                        v => v,
                        v => v)
                    .IsRequired();
            });
            
            builder.OwnsOne(b => b.OperationTypeId, operationTypeBuilder =>
            {
                operationTypeBuilder.Property(p => p.Value)
                    .HasColumnName("OperationTypeId")
                    .HasConversion(
                        v => v,
                        v => v)
                    .IsRequired();
            });
            
            builder.OwnsOne(b => b.StaffId, staffBuilder =>
            {
                staffBuilder.Property(p => p.Value)
                    .HasColumnName("StaffId")
                    .HasConversion(
                        v => v,
                        v => v)
                    .IsRequired();
            });

            // Configure the foreign key relationship for OperationType
            

            // Configure the IsActive property
            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();  // Optional: enforce it to be required
        }
    }
}
