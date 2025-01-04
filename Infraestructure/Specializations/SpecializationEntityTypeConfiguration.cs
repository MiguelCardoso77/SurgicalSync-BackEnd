using DDDNetCore.Domain.Specializations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = DDDNetCore.Domain.Specializations.Specialization;
using System;
using DDDNetCore.Domain.Specializations;

namespace DDDNetCore.Infraestructure.Specializations;

public class SpecializationEntityTypeConfiguration: IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasKey(s =>s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                s => s.AsString(),
                s => new SpecializationCode(s))
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        // Configure owned Designation value object with HasConversion
        builder.OwnsOne(b => b.Designation, nameBuilder =>
        {
            nameBuilder.Property(p => p.Value)
                .HasConversion(
                    v => v,
                    v => v)
                .HasColumnName("SpecializationDesignation")
                .IsRequired();
        });
        
        // Configure owned Description value object with HasConversion
        builder.OwnsOne(b => b.Description, nameBuilder =>
        {
            nameBuilder.Property(p => p.Value)
                .HasConversion(
                    v => v,
                    v => v)
                .HasColumnName("SpecializationDescription");
        });
    }
    


}