using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;
using Torneosv2.src.Modules.Torneos.Domain.Entities;
using Torneosv2.src.Modules.Equipos.Domain.Entities;

namespace Torneosv2.src.Modules.TorneoEquipos.Domain.Entities
{
    public class TorneoEquipos
    {
        public int TorneoId { get; set; }
        public int EquipoId { get; set; }
        public Torneo Torneo { get; set; } = null!;
        public Equipo Equipo { get; set; } = null!;

    }
}