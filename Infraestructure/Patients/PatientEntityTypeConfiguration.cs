using DDDNetCore.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.Patients
{
    internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
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