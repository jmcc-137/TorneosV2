using Torneosv2.src.Modules.Torneos.Application.Interfaces;
using Torneosv2.src.Modules.Equipos.Application.Interfaces;
using Torneosv2.src.Modules.Equipos.Domain.Entities;
using Torneosv2.src.Modules.Torneos.Domain.Entities;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;

using Torneosv2.src.Modules.TorneoEquipos.Application.Interfaces;
using Torneosv2.src.Modules.TorneoEquipos.Application.Interface;
using Torneosv2.src.Modules.Torneos.Infrastructure.Repository;
using Torneosv2.src.Modules.Torneos.Application.Services;

namespace Torneosv2.src.Modules.TorneoEquipos.Application.Services
{
    public class TorneoEquiposService : ITorneoEquipoService
    {
        private readonly ITorneoEquiposRepository _torneoEquiposRepository;
        private readonly ITorneoService _torneoService;
        private readonly IEquipoService _equipoService;

        public TorneoEquiposService(
            ITorneoEquiposRepository torneoEquiposRepository,
            ITorneoService torneoService,
            IEquipoService equipoService)
        {
            _torneoEquiposRepository = torneoEquiposRepository;
            _torneoService = torneoService;
            _equipoService = equipoService;
        }

        public async Task AsignarEquipoATorneoAsync(int torneoId, int equipoId)
        {

            var torneo = await _torneoService.ObtenerTorneosPorSuIdAsync(torneoId);
            if (torneo == null)
            {
                throw new ArgumentException($"No existe un torneo con ID {torneoId}");
            }


            var equipo = await _equipoService.ObtenerEquipoPorIdAsync(equipoId);
            if (equipo == null)
            {
                throw new ArgumentException($"No existe un equipo con ID {equipoId}");
            }

            // Validar que no esté ya asignado
            if (await _torneoEquiposRepository.ExisteAsignacionAsync(torneoId, equipoId))
            {
                throw new InvalidOperationException($"El equipo '{equipo.Nombre}' ya está asignado al torneo '{torneo.Nombre}'");
            }


            await _torneoEquiposRepository.AsignarEquipoAsync(torneoId, equipoId);
        }

        public async Task DesasignarEquipoDeTorneoAsync(int torneoId, int equipoId)
        {
            if (!await _torneoEquiposRepository.ExisteAsignacionAsync(torneoId, equipoId))
            {
                throw new InvalidOperationException($"El equipo {equipoId} no está asignado al torneo {torneoId}");
            }


            var torneo = await _torneoService.ObtenerTorneosPorSuIdAsync(torneoId);


            await _torneoEquiposRepository.DesasignarEquipoAsync(torneoId, equipoId);
        }

        public async Task<IEnumerable<Equipo>> ObtenerEquiposDisponiblesParaTorneoAsync(int torneoId) 
        {

            var todosLosEquipos = await _equipoService.ConsultarEquiposAsync();


            var equiposAsignados = await _torneoEquiposRepository.ObtenerEquiposAsignadosAsync(torneoId); 


            return todosLosEquipos.Where(e => !equiposAsignados.Contains(e.Id));
        }

        public async Task<IEnumerable<Torneo>> ObtenerTorneosDisponiblesParaEquipoAsync(int equipoId) 
        {
   
            var todosLosTorneos = await _torneoService.ConsultarTorneosAsync();
            
   
            var torneosAsignados = await _torneoEquiposRepository.ObtenerTorneosDelEquipoAsync(equipoId);
            

            return todosLosTorneos.Where(t => !torneosAsignados.Contains(t.Id));
        }

        public async Task<IEnumerable<TorneoEquipos.Domain.Entities.TorneoEquipos>> ObtenerTodasLasAsignacionesAsync()
        {
            return await _torneoEquiposRepository.ObtenerTodasLasAsignacionesAsync();
        }

        public async Task<bool> ValidarAsignacionAsync(int torneoId, int equipoId)
        {
            try
            {

                var torneo = await _torneoService.ObtenerTorneosPorSuIdAsync(torneoId);
                var equipo = await _equipoService.ObtenerEquipoPorIdAsync(equipoId);
                
                if (torneo == null || equipo == null)
                    return false;
                
                if (await _torneoEquiposRepository.ExisteAsignacionAsync(torneoId, equipoId))
                    return false;
                
                if (torneo.Ifecha < DateTime.Now.Date)
                    return false;
                
                await ValidarConflictosDeFechasAsync(torneoId, equipoId);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> ObtenerEstadisticasAsync()
        {
            var asignaciones = await _torneoEquiposRepository.ObtenerTodasLasAsignacionesAsync();
            var totalAsignaciones = asignaciones.Count();
            var equiposConAsignaciones = asignaciones.Select(a => a.EquipoId).Distinct().Count();
            var torneosConAsignaciones = asignaciones.Select(a => a.TorneoId).Distinct().Count();
            
            return $"📊 ESTADÍSTICAS DE ASIGNACIONES:\n" +
                   $"   Total de asignaciones: {totalAsignaciones}\n" +
                   $"   Equipos con asignaciones: {equiposConAsignaciones}\n" +
                   $"   Torneos con asignaciones: {torneosConAsignaciones}";
        }

        private async Task ValidarConflictosDeFechasAsync(int torneoId, int equipoId)
        {
            var torneosDelEquipo = await _torneoEquiposRepository.ObtenerTorneosDelEquipoAsync(equipoId);
            var torneoActual = await _torneoService.ObtenerTorneosPorSuIdAsync(torneoId);
            
            foreach (var torneoExistenteId in torneosDelEquipo)
            {
                var torneoExistente = await _torneoService.ObtenerTorneosPorSuIdAsync(torneoExistenteId);
                if (torneoExistente != null && 
                    FechasSeSolapan(torneoActual!.Ifecha, torneoActual.Ffecha, 
                                   torneoExistente.Ifecha, torneoExistente.Ffecha))
                {
                    throw new InvalidOperationException(
                        $"El equipo ya está asignado a un torneo con fechas que se solapan: '{torneoExistente.Nombre}' " +
                        $"({torneoExistente.Ifecha:dd/MM/yyyy} - {torneoExistente.Ffecha:dd/MM/yyyy})");
                }
            }
        }

        private bool FechasSeSolapan(DateTime inicio1, DateTime fin1, DateTime inicio2, DateTime fin2)
        {
            return inicio1 <= fin2 && inicio2 <= fin1;
        }
    }
}