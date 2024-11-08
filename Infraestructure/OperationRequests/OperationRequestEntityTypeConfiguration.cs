using System;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
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
                b => b.AsString(),
                b => new OperationRequestId(b))
                .IsRequired()
                .ValueGeneratedOnAdd();

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

            // Configure foreign key for MedicalRecordNumber (Patient)
            builder.HasOne<Patient>()
                .WithMany()
                .HasForeignKey(b => b.MedicalRecordNumber)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            // Configure foreign key for OperationTypeId
            builder.HasOne<OperationType>()
                .WithMany()
                .HasForeignKey(b => b.OperationTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            // Configure foreign key for StaffId
            builder.HasOne<Domain.Staffs.Staff>()
                .WithMany()
                .HasForeignKey(b => b.StaffId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // Configure the IsActive property
            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();  // Optional: enforce it to be required
        }
    }
}
