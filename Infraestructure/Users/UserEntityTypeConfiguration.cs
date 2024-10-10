using DDDNetCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.OwnsOne(b => b.UserEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.UserEmailValue)
                    .HasColumnName("Email");
            });
            
            builder.OwnsOne(b => b.Username, nameBuilder =>
            {
                nameBuilder.Property(p => p.UsernameValue)
                    .HasColumnName("Name");
            });
            
             
            builder.Property(b => b.UserRole)
                .HasColumnName("Role")
                .HasConversion<string>();
        }
    }
}