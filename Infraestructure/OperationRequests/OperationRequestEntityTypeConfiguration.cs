using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.OperationRequests
{
    // Configures the entity properties and relationships for the <see cref="OperationRequest"/> entity.
    // This class implements the <IEntityTypeConfiguration{TEntity}> interface to define
    // how the <OperationRequest> entity maps to the database schema.
    
    internal class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest>
    {
        // Configures the entity of type <see cref="OperationRequest"/> using the provided <EntityTypeBuilder{TEntity}>.
        // This method sets up the primary key, properties, and relationships with other entities.
        /// <param name="builder">The <EntityTypeBuilder/> used to configure the entity.</param>
        /// 
        public void Configure(EntityTypeBuilder<OperationRequest> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.OwnsOne(b => b.DeadlineDate, deadlineDateBuilder =>
            {
                deadlineDateBuilder.Property(d => d.Date)
                    .HasColumnName("DeadlineDate")
                    .IsRequired();
            });

            builder.Property(b => b.Priority)
                .HasColumnName("Priority")
                .IsRequired();

            builder.Property(b => b.OperationTypeId)
                .HasColumnName("OperationTypeId")
                .IsRequired();

            builder.HasOne<OperationType>()
                .WithMany()
                .HasForeignKey(b => b.OperationTypeId);

            builder.Property(b => b.MedicalRecordNumber)
                .HasColumnName("MedicalRecordNumber")
                .IsRequired();

            builder.HasOne<Patient>()
                .WithMany()
                .HasForeignKey(b => b.MedicalRecordNumber);

            builder.Property(b => b.LicenseNumber)
                .HasColumnName("LicenseNumber")
                .IsRequired();

            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

        }
    }
}
