using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;

namespace Torneosv2.src.Shared.Configurations
{
    public class TorneoEquiposConfiguracion : IEntityTypeConfiguration<TorneoEquipos>
    {
        public void Configure(EntityTypeBuilder<TorneoEquipos> builder)
        {
            builder.ToTable("TorneoEquipos");
            builder.HasKey(te => new { te.TorneoId, te.EquipoId });

            builder.HasOne(te => te.Torneo)
                .WithMany(t => t.TorneoEquipos)
                .HasForeignKey(te => te.TorneoId);

            builder.HasOne(te => te.Equipo)
                .WithMany(e => e.TorneoEquipos)
                .HasForeignKey(te => te.EquipoId);

        }
        
    }
}