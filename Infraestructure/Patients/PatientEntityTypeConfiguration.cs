using DDDNetCore.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.Patients
{
    /**
     * Configures the entity type for the Patient class in the Entity Framework Core context.
     *
     * This class implements the IEntityTypeConfiguration interface to provide
     * a fluent API for configuring the Patient entity, defining the mapping
     * of its properties to database columns, and specifying ownership of value objects.
     */
    internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        /**
         * Configures the Patient entity.
         *
         * This method sets the primary key for the Patient entity and configures
         * the properties of the Patient and its value objects (PatientName, BirthDate,
         * Gender, PhoneNumber, EmergencyContact, UserEmail, MedicalConditions, and
         * AppointmentHistory) to map to specific columns in the database.
         *
         * @param builder The builder used to configure the Patient entity.
         */
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(b => b.Id);

            builder.OwnsOne(b => b.PatientName, nameBuilder =>
            {
                nameBuilder.Property(p => p.PatientNameValue)
                    .HasColumnName("PatientName");
            });

            builder.OwnsOne(b => b.BirthDate, birthDateBuilder =>
            {
                birthDateBuilder.Property(p => p.BirthDateValue)
                    .HasColumnName("BirthDate");
            });

            builder.OwnsOne(b => b.Gender, genderBuilder =>
            {
                genderBuilder.Property(p => p.GenderValue)
                    .HasColumnName("Gender");
            });

            builder.OwnsOne(b => b.PhoneNumber, phoneNumberBuilder =>
            {
                phoneNumberBuilder.Property(p => p.PhoneNumberValue)
                    .HasColumnName("PhoneNumber");
            });

            builder.OwnsOne(b => b.EmergencyContact, emergencyContactBuilder =>
            {
                emergencyContactBuilder.Property(p => p.EmergencyContactValue)
                    .HasColumnName("EmergencyContact");
            });

            builder.OwnsOne(b => b.UserEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.UserEmailValue)
                    .HasColumnName("Email");
            });

            builder.OwnsMany(b => b.MedicalConditions, medicalConditionsBuilder =>
            {
                medicalConditionsBuilder.Property(p => p.MedicalConditionsValue)
                    .HasColumnName("MedicalConditions");
            });

            builder.OwnsMany(b => b.AppointmentHistory, appointmentHistoryBuilder =>
            {
                appointmentHistoryBuilder.Property(p => p.AppointmentHistoryValue)
                    .HasColumnName("AppointmentHistory");
            });
        }
    }
}