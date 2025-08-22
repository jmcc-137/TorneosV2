using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Domain.Entities;
using Torneosv2.src.Modules.Torneos.Domain.Entities;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;

namespace Torneosv2.src.Modules.TorneoEquipos.Application.Interface
{
    public interface ITorneoEquipoService
    {
        Task AsignarEquipoATorneoAsync(int torneoId, int equipoId);
        Task DesasignarEquipoDeTorneoAsync(int torneoId, int equipoId);
        Task<IEnumerable<Equipo>> ObtenerEquiposDisponiblesParaTorneoAsync(int torneoId);
        Task<IEnumerable<Torneo>> ObtenerTorneosDisponiblesParaEquipoAsync(int equipoId);
        Task<IEnumerable<TorneoEquipos.Domain.Entities.TorneoEquipos>> ObtenerTodasLasAsignacionesAsync();
        Task<bool> ValidarAsignacionAsync(int torneoId, int equipoId);
        Task<string> ObtenerEstadisticasAsync();
    }
}