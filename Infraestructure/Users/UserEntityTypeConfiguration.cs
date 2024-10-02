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
            
            builder.OwnsOne(b => b.Password, passwordBuilder =>
            {
                passwordBuilder.Property(p => p.PasswordValue)
                    .HasColumnName("Password");
            });
            
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
            
            builder.OwnsOne(b => b.UserRole, roleBuilder =>
            {
                roleBuilder.Property(p => p.RoleValue)
                    .HasColumnName("Role");
            });
        }
    }
}