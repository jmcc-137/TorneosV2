using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneosv2.src.Modules.Torneos.Domain.Entities;

namespace Torneosv2.src.Shared.Configurations;

public class TorneoConfiguration : IEntityTypeConfiguration<Torneo>
{
    public void Configure(EntityTypeBuilder<Torneo> builder)
    {
        builder.ToTable("Torneo");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nombre)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.Pais)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.Ciudad)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.Ifecha)
            .IsRequired();
        builder.Property(u => u.Ffecha)
            .IsRequired();

        builder.HasMany(u => u.TorneoEquipos)
            .WithOne(te => te.Torneo)
            .HasForeignKey(te => te.TorneoId)
            .OnDelete(DeleteBehavior.Cascade);

    }
        
}
