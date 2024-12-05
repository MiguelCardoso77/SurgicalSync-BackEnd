using DDDNetCore.Domain.RoomTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infraestructure.RoomTypes;

/**
 * Configures the entity properties and relationships for the <see cref="RoomType"/> entity.
 * This class implements the <IEntityTypeConfiguration/> interface to define
 * how the <RoomType/> entity maps to the database schema.
 */
public class RoomTypeEntityTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        // Primary key configuration
        builder.HasKey(b => b.Id);
        
        // Use HasConversion for Id
        builder.Property(b => b.Id)
            .HasConversion(
                b => b.AsString(),
                b => new RoomTypeId(b))
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        // Configure owned Designation value object with HasConversion
        builder.OwnsOne(b => b.Designation, nameBuilder =>
        {
            nameBuilder.Property(p => p.Value)
                .HasConversion(
                    v => v,
                    v => v)
                .HasColumnName("RoomTypeDesignation")
                .IsRequired();
        });
        
        // Configure owned Description value object with HasConversion
        builder.OwnsOne(b => b.Description, nameBuilder =>
        {
            nameBuilder.Property(p => p.Value)
                .HasConversion(
                    v => v,
                    v => v)
                .HasColumnName("RoomTypeDescription")
                .IsRequired();
        });
    }

}