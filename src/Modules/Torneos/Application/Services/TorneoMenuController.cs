using System;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Torneos.Domain.Entities;
using Torneosv2.src.Modules.Torneos.Application.Interfaces;
using Torneosv2.src.Shared.utils;

namespace Torneosv2.src.Modules.Torneos.Application.Services
{
    /// <summary>
    /// Controlador que orquesta los casos de uso del menú de torneos
    /// Cumple con Single Responsibility: solo maneja la lógica de coordinación
    /// </summary>
    public class TorneoMenuController : ITorneoMenuController
    {
        private readonly ITorneoService _torneoService;

        public TorneoMenuController(ITorneoService torneoService)
        {
            _torneoService = torneoService;
        }

        public async Task HandleCreateTorneoAsync()
        {
            // Implementación para registrar un torneo
                Console.Clear();

                int Id = await IdGeneretor.GenerateUniqueIdAsync(
                    async () => await _torneoService.ConsultarTorneosAsync(),
                      Torneo => Torneo.Id);
                Console.WriteLine($"ID generado: {Id}");
                Console.WriteLine("Ingrese el nombre:");
                string? nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el pais");
                string? pais = Console.ReadLine();
                Console.WriteLine("Ingrese la Ciudad");
                string? ciudad = Console.ReadLine();

                DateTime fechaInicio;
                DateTime fechaFinal;
                while (true)
                {
                    Console.WriteLine("Ingrese la fecha de Inicio: ");
                    if (DateTime.TryParse(Console.ReadLine(), out fechaInicio))
                    {
                        break;
                    }
                    Console.WriteLine("Formato de fecha invalido. Intentente de nuevo");
                }

                while (true)
                {
                    Console.WriteLine("Ingrese la fecha final: ");
                    if (DateTime.TryParse(Console.ReadLine(), out fechaFinal))
                    {
                        if (fechaFinal >= fechaInicio)
                        {
                            break;
                        }
                        Console.WriteLine("La fecha final debe ser mayor o igual a la fecha de inicio.");
                    }
                    else
                    {
                        Console.WriteLine("Formato de fecha invalido. Intentente de nuevo");
                    }

                }

                await _torneoService.RegistrarTorneoAsync(Id!, nombre!, pais!, ciudad!, fechaInicio!, fechaFinal!);
                Console.WriteLine("Agregado con exito");


        }

        public async Task HandleShowTorneosAsync()
        {
            // Implementación para mostrar torneos

                Console.Clear();
                var torneo = await _torneoService.ConsultarTorneosAsync();
                if (torneo.Any())
                {

                    var lista = await _torneoService.ConsultarTorneosAsync();
                    foreach (var u in lista)
                    {
                        Console.WriteLine($" ID:{u.Id}, Nombre: {u.Nombre}, Pais: {u.Pais}, Ciudad: {u.Ciudad},Fecha de inicio: {u.Ifecha}, Fecha final: {u.Ffecha}");
                    }
                    Console.WriteLine("precione cualquier tecla para continuar");
                    Console.ReadKey();

                }
                else
                {
                    Console.Write("No se Agregado nada");
                    Console.WriteLine("precione cualquier tecla para continuar");
                    Console.ReadKey();
                }
        }

        public async Task HandleSearchTorneoAsync()
        {
            // Implementación para buscar un torneo por ID
            Console.Clear();
            Console.Write("ID:");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID invalido");
                Console.ReadKey();
                return;
            }
            var torneo = await _torneoService.ObtenerTorneosPorSuIdAsync(id);
            if (torneo is null)
            {
                Console.WriteLine("No encontrado");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Torneo: ID:{torneo.Id}, Nombre: {torneo.Nombre}");
            Console.WriteLine("precione cualquier tecla para continuar");
            Console.ReadKey();
        }

        public async Task HandleDeleteTorneoAsync()
        {
            // Implementación para eliminar un torneo por ID
            Console.Clear();
            Console.Write("ID a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var existente = await _torneoService.ObtenerTorneosPorSuIdAsync(id);
            if (existente is null)
            {
                Console.WriteLine("Torneo no encontrado.");
                return;
            }

            await _torneoService.EliminarTorneo(id);
            Console.WriteLine("🗑️ Torneo eliminado.");
        }

        public async Task HandleUpdateTorneoAsync()
        {
            // Implementación para actualizar un torneo

            await HandleShowTorneosAsync();
            Console.WriteLine();
            Console.Write("ID a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var existente = await _torneoService.ObtenerTorneosPorSuIdAsync(id);
            if (existente is null)
            {
                Console.WriteLine("País no encontrado.");
                return;
            }

            Console.Write($"Nuevo nombre (actual: {existente.Nombre}): ");
            var nuevoNombre = Console.ReadLine();
            Console.Write($"Nuevo pais (actual: {existente.Pais}): ");
            var nuevoPais = Console.ReadLine()!;
            Console.Write($"Nueva ciudad (actual: {existente.Ciudad}): ");
            var nuevaCiudad = Console.ReadLine()!;

            DateTime NfechaInicio;
            DateTime NfechaFinal;
            while (true)
            {
                Console.Write($"Nueva fecha de inicio (actual: {existente.Ifecha}): ");
                if (DateTime.TryParse(Console.ReadLine(), out NfechaInicio))
                {
                    break;
                }
                Console.WriteLine("Formato de fecha invalido. Intentente de nuevo");
            }

            while (true)
            {
                Console.Write($"Nueva fecha final (actual: {existente.Ffecha}): ");
                if (DateTime.TryParse(Console.ReadLine(), out NfechaFinal))
                {
                    if (NfechaFinal >= NfechaInicio)
                    {
                        break;
                    }
                    Console.WriteLine("La fecha final debe ser mayor o igual a la fecha de inicio.");
                }
                else
                {
                    Console.WriteLine("Formato de fecha invalido. Intentente de nuevo");
                }

            }
            
            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                Console.WriteLine("El nombre es obligatorio.");
                return;
            }
            else
            {
                existente.Nombre = nuevoNombre;
                existente.Pais = nuevoPais;
                existente.Ciudad = nuevaCiudad;
                existente.Ifecha = NfechaInicio;
                existente.Ffecha = NfechaFinal;

                await _torneoService.ActualizarTorneo(id,nuevoNombre, nuevoPais, nuevaCiudad, NfechaInicio, NfechaFinal);
                Console.WriteLine("País actualizado.");



            }
            }
    }
}
