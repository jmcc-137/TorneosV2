using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Application.Interfaces;
using Torneosv2.src.Modules.TorneoEquipos.Application.Interface;
using Torneosv2.src.Modules.Torneos.Application.Interfaces;
using Torneosv2.src.Modules.Torneos.Domain.Entities;
using Torneosv2.src.Shared.utils;

namespace Torneosv2.src.Modules.Equipos.Application.Services
{
    public class EquipoMenuController : IEquipoMenuController
    {
        private readonly IEquipoService _equipoService;
        private readonly ITorneoEquipoService _torneoEquiposService;
        private readonly ITorneoService _torneoService;

        public EquipoMenuController(
            IEquipoService equipoService,
            ITorneoEquipoService torneoEquiposService,
            ITorneoService torneoService)
        {
            _equipoService = equipoService;
            _torneoEquiposService = torneoEquiposService;
            _torneoService = torneoService;
        }

        public async Task HandleRegistrarEquipoAsync()
        {
            Console.Clear();
            int Id = await IdGeneretor.GenerateUniqueIdAsync(
                async () => await _equipoService.ConsultarEquiposAsync(),
                Equipo => Equipo.Id
            );
            Console.WriteLine($"ID generado: {Id}");
            Console.Write("Ingrese el nombre del equipo: ");
            string? nombre = Console.ReadLine();
            Console.Write("Ingrese el país del equipo: ");
            string? pais = Console.ReadLine();
            await _equipoService.RegistrarEquipoAsync(Id, nombre!, pais!);
            Console.WriteLine("Equipo registrado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        public async Task HandleActualizarEquipoAsync()
        {
            Console.Clear();
            Console.WriteLine("ACTUALIZAR EQUIPO");
            Console.WriteLine("=================");

            Console.Write("Ingrese el ID del equipo a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            var equipo = await _equipoService.ObtenerEquipoPorIdAsync(id);
            if (equipo is null)
            {
                Console.WriteLine("Equipo no encontrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Equipo actual: ID:{equipo.Id}, Nombre: {equipo.Nombre}, País: {equipo.Pais}");
            Console.Write("Nuevo nombre (dejar vacío para no cambiar): ");
            string? nuevoNombre = Console.ReadLine();
            Console.Write("Nuevo país (dejar vacío para no cambiar): ");
            string? nuevoPais = Console.ReadLine();

            // Si el usuario deja vacío, mantener el valor anterior
            nuevoNombre = string.IsNullOrWhiteSpace(nuevoNombre) ? equipo.Nombre : nuevoNombre;
            nuevoPais = string.IsNullOrWhiteSpace(nuevoPais) ? equipo.Pais : nuevoPais;

            await _equipoService.ActualizarEquipoAsync(id, nuevoNombre, nuevoPais);
            Console.WriteLine("Equipo actualizado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        public async Task HandleEliminarEquipoAsync()
        {
            await _equipoService.ConsultarEquiposAsync();
            Console.Clear();
            Console.Write("ID de equipo a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID invalido");
                Console.ReadKey();
                return;
            }

            var existente = await _equipoService.ObtenerEquipoPorIdAsync(id);
            if (existente is null)
            {
                Console.WriteLine("Equipo no encontrado");
                Console.ReadKey();
                return;
            }
            else
            { 
                await _equipoService.EliminarEquipoAsync(id);
                Console.WriteLine("Equipo eliminado exitosamente.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

        }
        public async Task HandleObtenerEquipoPorIdAsync()
        {


            Console.Clear();
            Console.Write("ID:");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID invalido");
                Console.ReadKey();
                return;
            }
            var equipo = await _equipoService.ObtenerEquipoPorIdAsync(id);
            if (equipo is null)
            {
                Console.WriteLine("No encontrado");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Equipo: ID:{equipo.Id}, Nombre: {equipo.Nombre}, Pais: {equipo.Pais}");
            Console.WriteLine("precione cualquier tecla para continuar");
            Console.ReadKey();
           
        }
        public async Task HandleShowEquiposAsync()
        {
            Console.Clear();
            var equipos = await _equipoService.ConsultarEquiposAsync();
            if (equipos.Any())
            {
                Console.WriteLine("╔════╦════════════════════╦════════════════════╗");
                Console.WriteLine("║ ID ║      Nombre        ║       País         ║");
                Console.WriteLine("╠════╬════════════════════╬════════════════════╣");
                foreach (var u in equipos.OrderBy(e => e.Id))
                {
                    Console.WriteLine($"║ {u.Id,2} ║ {u.Nombre,-18} ║ {u.Pais,-18} ║");
                }
                Console.WriteLine("╚════╩════════════════════╩════════════════════╝");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No se ha agregado nada.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        public async Task HandleConsultarEquiposAsync()
        {
            Console.Clear();
            Console.WriteLine("LISTA DE EQUIPOS");
            Console.WriteLine("================");

            var equipos = await _equipoService.ConsultarEquiposAsync();
            if (!equipos.Any())
            {
                Console.WriteLine("No hay equipos registrados.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("╔════╦════════════════════╦════════════════════╗");
            Console.WriteLine("║ ID ║      Nombre        ║       País         ║");
            Console.WriteLine("╠════╬════════════════════╬════════════════════╣");
            foreach (var e in equipos.OrderBy(e => e.Id))
            {
                Console.WriteLine($"║ {e.Id,2} ║ {e.Nombre,-18} ║ {e.Pais,-18} ║");
            }
            Console.WriteLine("╚════╩════════════════════╩════════════════════╝");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        public async Task HandleAsignarEquipoAsync()
        {
            Console.Clear();
            Console.WriteLine("ASIGNAR EQUIPO A TORNEO");
            Console.WriteLine("=======================");

            var equipos = await _equipoService.ConsultarEquiposAsync();
            var asignaciones = await _torneoEquiposService.ObtenerTodasLasAsignacionesAsync();
            var equiposAsignados = asignaciones.Select(a => a.EquipoId).Distinct().ToList();
            var equiposDisponibles = equipos.Where(e => !equiposAsignados.Contains(e.Id)).ToList();

            if (!equiposDisponibles.Any())
            {
                Console.WriteLine("No hay equipos disponibles para asignar.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Equipos disponibles:");
            foreach (var e in equiposDisponibles)
                Console.WriteLine($"ID: {e.Id} - {e.Nombre} ({e.Pais})");

            Console.Write("Ingrese el ID del equipo a asignar: ");
            if (!int.TryParse(Console.ReadLine(), out int equipoId))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            var equipo = equiposDisponibles.FirstOrDefault(e => e.Id == equipoId);
            if (equipo == null)
            {
                Console.WriteLine("Equipo no encontrado o ya asignado.");
                Console.ReadKey();
                return;
            }

            var torneos = await _torneoEquiposService.ObtenerTorneosDisponiblesParaEquipoAsync(equipoId);
            if (!torneos.Any())
            {
                Console.WriteLine("No hay torneos disponibles para este equipo.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Torneos disponibles:");
            foreach (var t in torneos)
                Console.WriteLine($"ID: {t.Id} - {t.Nombre} ({t.Ciudad}, {t.Pais})");

            Console.Write("Ingrese el ID del torneo: ");
            if (!int.TryParse(Console.ReadLine(), out int torneoId))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            var torneo = torneos.FirstOrDefault(t => t.Id == torneoId);
            if (torneo == null)
            {
                Console.WriteLine("Torneo no encontrado.");
                Console.ReadKey();
                return;
            }

            await _torneoEquiposService.AsignarEquipoATorneoAsync(torneoId, equipoId);
            Console.WriteLine("Equipo asignado exitosamente.");
            Console.ReadKey();
        }

        public async Task HandleSalirEquipoAsync()
        {
            Console.Clear();
            Console.WriteLine("SALIR EQUIPO DE TORNEO");
            Console.WriteLine("======================");

            var asignaciones = await _torneoEquiposService.ObtenerTodasLasAsignacionesAsync();
            if (!asignaciones.Any())
            {
                Console.WriteLine("No hay equipos asignados a torneos.");
                Console.ReadKey();
                return;
            }

            var equipos = await _equipoService.ConsultarEquiposAsync();
            var equiposAsignados = equipos.Where(e => asignaciones.Any(a => a.EquipoId == e.Id)).ToList();

            Console.WriteLine("Equipos asignados:");
            foreach (var e in equiposAsignados)
                Console.WriteLine($"ID: {e.Id} - {e.Nombre} ({e.Pais})");

            Console.Write("Ingrese el ID del equipo a desasignar: ");
            if (!int.TryParse(Console.ReadLine(), out int equipoId))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            var asignacionesEquipo = asignaciones.Where(a => a.EquipoId == equipoId).ToList();
            if (!asignacionesEquipo.Any())
            {
                Console.WriteLine("Ese equipo no está asignado a ningún torneo.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Torneos asignados a este equipo:");
            foreach (var a in asignacionesEquipo)
            {
                // Usa el servicio de torneos para obtener el torneo por su ID
                var torneoActual = await _torneoService!.ObtenerTorneosPorSuIdAsync(a.TorneoId);
                if (torneoActual != null)
                    Console.WriteLine($"ID: {torneoActual.Id} - {torneoActual.Nombre} ({torneoActual.Ciudad}, {torneoActual.Pais})");
                else
                    Console.WriteLine($"ID: {a.TorneoId} - Torneo no encontrado");
            }

            Console.Write("Ingrese el ID del torneo del que desea salir: ");
            if (!int.TryParse(Console.ReadLine(), out int torneoId))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            if (!asignacionesEquipo.Any(a => a.TorneoId == torneoId))
            {
                Console.WriteLine("El equipo no está asignado a ese torneo.");
                Console.ReadKey();
                return;
            }

            await _torneoEquiposService.DesasignarEquipoDeTorneoAsync(torneoId, equipoId);
            Console.WriteLine("Equipo desasignado exitosamente.");
            Console.ReadKey();
        }
        

        // ✅ Crear método separado para mostrar equipos disponibles
        public async Task HandleShowEquiposDisponiblesAsync()
        {
            Console.Clear();
            Console.WriteLine("📋 EQUIPOS DISPONIBLES PARA ASIGNAR:");
            Console.WriteLine("".PadRight(50, '='));
            
            // Obtener todos los equipos
            var todosLosEquipos = await _equipoService.ConsultarEquiposAsync();
            
            if (!todosLosEquipos.Any())
            {
                Console.WriteLine("❌ No hay equipos registrados.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            // Obtener todas las asignaciones para saber qué equipos están ocupados
            var asignaciones = await _torneoEquiposService.ObtenerTodasLasAsignacionesAsync();
            var equiposAsignados = asignaciones.Select(a => a.EquipoId).Distinct().ToList();
            
            // Filtrar equipos disponibles (no asignados a ningún torneo)
            var equiposDisponibles = todosLosEquipos.Where(e => !equiposAsignados.Contains(e.Id)).ToList();
            
            if (!equiposDisponibles.Any())
            {
                Console.WriteLine("❌ No hay equipos disponibles para asignar.");
                Console.WriteLine("💡 Todos los equipos ya están asignados a torneos.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine($"Total de equipos disponibles: {equiposDisponibles.Count}");
            Console.WriteLine();
            
            foreach (var equipo in equiposDisponibles)
            {
                Console.WriteLine($"🏃 ID: {equipo.Id:D4} | {equipo.Nombre} ({equipo.Pais})");
            }
            
            Console.WriteLine("".PadRight(50, '='));
        }

        public async Task HandleShowEquiposAsignadosAsync()
        {
            Console.Clear();
            Console.WriteLine("📋 EQUIPOS ASIGNADOS A TORNEOS:");
            Console.WriteLine("".PadRight(70, '='));
            
            try
            {
                // Obtener todas las asignaciones
                var asignaciones = await _torneoEquiposService.ObtenerTodasLasAsignacionesAsync();
                
                if (!asignaciones.Any())
                {
                    Console.WriteLine("❌ No hay equipos asignados a ningún torneo.");
                    Console.WriteLine("💡 Puede comenzar asignando equipos desde el menú principal.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
                
                // Agrupar asignaciones por torneo
                var asignacionesPorTorneo = asignaciones.GroupBy(a => a.TorneoId);
                
                Console.WriteLine($"📊 Total de asignaciones: {asignaciones.Count()}");
                Console.WriteLine($"🏆 Torneos con equipos: {asignacionesPorTorneo.Count()}");
                Console.WriteLine();
                
                foreach (var grupo in asignacionesPorTorneo)
                {
                    var torneoId = grupo.Key;
                    var equiposDelTorneo = grupo.Select(a => a.EquipoId).ToList();
                    
                    // Obtener información del torneo
                    var torneo = await _torneoService!.ObtenerTorneosPorSuIdAsync(torneoId);
                    if (torneo == null)
                    {
                        Console.WriteLine($"⚠️  Torneo ID {torneoId} no encontrado");
                        continue;
                    }
                    
                    // Mostrar información del torneo
                    Console.WriteLine($"🏆 TORNEO: {torneo.Nombre}");
                    Console.WriteLine($"   📍 {torneo.Ciudad}, {torneo.Pais}");
                    Console.WriteLine($"   📅 {torneo.Ifecha:dd/MM/yyyy} - {torneo.Ffecha:dd/MM/yyyy}");
                    Console.WriteLine($"   👥 Equipos asignados: {equiposDelTorneo.Count}");
                    Console.WriteLine();
                    
                    // Mostrar equipos asignados a este torneo
                    foreach (var equipoId in equiposDelTorneo)
                    {
                        var equipo = await _equipoService.ObtenerEquipoPorIdAsync(equipoId);
                        if (equipo != null)
                        {
                            Console.WriteLine($"      🏃 {equipo.Nombre} ({equipo.Pais}) - ID: {equipo.Id}");
                        }
                        else
                        {
                            Console.WriteLine($"      ⚠️  Equipo ID {equipoId} no encontrado");
                        }
                    }
                    
                    Console.WriteLine("".PadRight(70, '-'));
                    Console.WriteLine();
                }
                
                Console.WriteLine("".PadRight(70, '='));
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al consultar asignaciones: {ex.Message}");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

    }
}