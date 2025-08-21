using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Application.Interfaces;
using Torneosv2.src.Modules.Torneos.Application.Interfaces;
using Torneosv2.src.Shared.utils;

namespace Torneosv2.src.Modules.Equipos.Application.Services
{
    public class EquipoMenuController : IEquipoMenuController
    {
        private readonly IEquipoService _equipoService;

        public EquipoMenuController(IEquipoService equipoService)
        {
            _equipoService = equipoService;
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
            // Logic to handle updating a team
            // Example: await _equipoService.ActualizarEquipoAsync(id, nuevoNombre, nuevoPais);
        }
        public async Task HandleEliminarEquipoAsync()
        {
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
        public async Task HandleConsultarEquiposAsync()
        {

        }
        public async Task HandleAsignarEquipoAsync()
        {
            // Logic to handle assigning a team
            // Example: await _equipoService.AsignarEquipoAsync(id, torneoId);
        }
        public async Task HandleSalirEquipoAsync()
        {
            // Logic to handle exiting a team
            // Example: await _equipoService.SalirEquipoAsync(id);
        }

        public async Task HandleShowEquiposAsync()
        {
            Console.Clear();
            var equipos = await _equipoService.ConsultarEquiposAsync();
            if (equipos.Any())
            {

                var lista = await _equipoService.ConsultarEquiposAsync();
                foreach (var u in lista)
                {
                    Console.WriteLine($" ID:{u.Id}, Nombre: {u.Nombre}, Pais: {u.Pais}");
                }
                Console.WriteLine(" presione cualquier tecla para continuar");
                Console.ReadKey();

            }
            else
            {
                Console.Write("No se Agregado nada");
                Console.WriteLine(" presione cualquier tecla para continuar");
                Console.ReadKey();
            }
        }


    }
}