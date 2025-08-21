using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;
using TorneoEquiposEntity = Torneosv2.src.Modules.TorneoEquipos.Domain.Entities.TorneoEquipos;

namespace Torneosv2.src.Modules.TorneoEquipos.Application.Interfaces
{
    public interface ITorneoEquiposRepository
    {
        Task<bool> ExisteAsignacionAsync(int torneoId, int equipoId);
        Task AsignarEquipoAsync(int torneoId, int equipoId);
        Task DesasignarEquipoAsync(int torneoId, int equipoId);
        Task<IEnumerable<int>> ObtenerEquiposAsignadosAsync(int torneoId);
        Task<IEnumerable<int>> ObtenerTorneosDelEquipoAsync(int equipoId);
        Task<IEnumerable<TorneoEquiposEntity>> ObtenerTodasLasAsignacionesAsync();
        Task<int> ContarEquiposEnTorneoAsync(int torneoId);
        Task<int> ContarTorneosDelEquipoAsync(int equipoId);
    }
}