using DDDNetCore.Domain.Users;
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
            
            // Configure owned Email value object
            builder.OwnsOne(b => b.UserEmail, emailBuilder =>
            {
                emailBuilder.Property(p => p.Value)
                    .HasColumnName("Email");
            });
            
            // Configure owned Username value object
            builder.OwnsOne(b => b.Username, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value)
                    .HasColumnName("Name");
            });
            
            // Configure owned Role value object
            builder.Property(b => b.UserRole)
                .HasColumnName("Role")
                .HasConversion<string>();
        }
    }
}