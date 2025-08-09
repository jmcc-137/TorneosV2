using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneosv2.src.Modules.Personas.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}