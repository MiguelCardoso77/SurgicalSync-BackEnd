

using System;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infraestructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.Users
{
    /**
     * Configures the entity properties and relationships for the <see cref="User"/> entity.
     * This class implements the <IEntityTypeConfiguration/> interface to define
     * how the <User/> entity maps to the database schema.
     */
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        /**
         * Configures the entity of type <User/> using the provided <EntityTypeBuilder/>.
         * This method sets up the primary key, properties, and relationships with other entities.
         * <param name="builder">The <EntityTypeBuilder/> used to configure the entity.</param>
         */
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key configuration
            builder.HasKey(b => b.Id);
            builder.Property(e=> e.Id).HasConversion(new EntityIdValueConverter<UserId>());
            
            builder.OwnsOne(b => b.UserEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.Value)
                    .HasColumnName("UserEmail")
                    .IsRequired();

                emailBuilder.HasIndex(p => p.Value).IsUnique(); 
            });
            
            // Configure owned Username value object
            builder.OwnsOne(b => b.Username, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value).HasColumnName("Name");
                nameBuilder.Property(p => p.Value).HasConversion<string>();
                nameBuilder.Property(p => p.Value).IsRequired();
            });
            
            // Configure owned Role value object
            builder.Property(b => b.UserRole)
                .HasConversion(
                    b => b.ToString(),
                    b => (UserRole)Enum.Parse(typeof(UserRole), b))
                .HasColumnName("Role")
                .IsRequired();
        }
    }
}