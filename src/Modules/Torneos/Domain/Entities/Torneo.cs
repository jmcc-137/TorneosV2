using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneoEquiposEntity = Torneosv2.src.Modules.TorneoEquipos.Domain.Entities.TorneoEquipos;
namespace Torneosv2.src.Modules.Torneos.Domain.Entities
{
    public class Torneo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        public string Ciudad { get; set; } = string.Empty;
        public DateTime Ifecha { get; set; }
        public DateTime Ffecha { get; set; }

        public ICollection<TorneoEquiposEntity>? TorneoEquipos { get; set; }

    }
}