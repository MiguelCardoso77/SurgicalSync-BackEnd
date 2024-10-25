using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
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
            // Primary key configuration
            builder.HasKey(b => b.Id);

            // Configure owned Priority value object
            builder.Property(b => b.Priority)
                .HasColumnName("Priority")
                .IsRequired();
            
            // Configure owned DeadlineDate value object
            builder.OwnsOne(b => b.DeadlineDate, deadlineDateBuilder =>
            {
                deadlineDateBuilder.Property(p => p.Date)
                    .HasColumnName("DeadlineDate")
                    .IsRequired();
            });

            // Configure the foreign key relationship for OperationType
            builder.HasOne<OperationType>()
                .WithMany()
                .HasForeignKey(b => b.OperationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the foreign key relationship for Patient (via MedicalRecordNumber)
            builder.HasOne<Patient>()
                .WithMany()
                .HasForeignKey(b => b.MedicalRecordNumber)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the foreign key relationship for Staff (via LicenseNumber)
            builder.HasOne<Domain.Staffs.Staff>()  // Assuming Staff entity handles LicenseNumber
                .WithMany()
                .HasForeignKey(b => b.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the IsActive property
            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();  // Optional: enforce it to be required
        }
    }
}
