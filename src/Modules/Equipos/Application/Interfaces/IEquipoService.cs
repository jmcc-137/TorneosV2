using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Domain.Entities;

namespace Torneosv2.src.Modules.Equipos.Application.Interfaces;
    public interface IEquipoService
    {
        Task RegistrarEquipoAsync(int id, string nombre, string pais);
        Task ActualizarEquipoAsync(int id, string nuevoNombre, string nuevoPais);
        Task EliminarEquipoAsync(int id);
        Task<Equipo?> ObtenerEquipoPorIdAsync(int id);
        Task<IEnumerable<Equipo>> ConsultarEquiposAsync();
    }
