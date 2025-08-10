using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneosv2.src.Modules.Equipos.Domain.Entities
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }
}