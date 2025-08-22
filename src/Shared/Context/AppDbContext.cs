using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneosv2.src.Modules.Equipos.Domain.Entities;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;
using Torneosv2.src.Modules.Torneos.Domain.Entities;

namespace Torneosv2.src.Shared.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Torneo> Torneos => Set<Torneo>();
    public DbSet<Equipo> Equipos => Set<Equipo>();

    public DbSet<TorneoEquipos> TorneoEquipos => Set<TorneoEquipos>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
