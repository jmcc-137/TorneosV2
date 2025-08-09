using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}