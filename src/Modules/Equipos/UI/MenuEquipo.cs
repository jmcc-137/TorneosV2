using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Application.Services;
using Torneosv2.src.Modules.Equipos.Infrastructure.Repository;
using Torneosv2.src.Shared.Context;
using Torneosv2.src.Shared.utils;

namespace Torneosv2.src.Modules.Equipos.UI;


public class MenuEquipo
{
    private readonly AppDbContext _context;

    readonly EquipoRepository repo = null!;
    readonly EquipoService _service = null!;

    public MenuEquipo(AppDbContext context)
    {
        _context = context;
        repo = new EquipoRepository(_context);
        _service = new EquipoService(repo);
    }
    public async Task RenderMenu()
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            
            ImpresorLento.Imprimir("╔════════════════════════════════════════════╗");
            ImpresorLento.Imprimir("║          REGISTRO DE EQUIPOS               ║");
            ImpresorLento.Imprimir("╠════════════════════════════════════════════╣");
            ImpresorLento.Imprimir("║ 1. Registrar Equipo                        ║");
            ImpresorLento.Imprimir("║ 2. Buscar Equipo(ID)                       ║");
            ImpresorLento.Imprimir("║ 3. Eliminar Equipo(ID)                     ║");
            ImpresorLento.Imprimir("║ 4. Actualizar Equipo                       ║");
            ImpresorLento.Imprimir("║ 5. Mostrar Equipos                         ║");
            ImpresorLento.Imprimir("║ 6. Asignar Equipo                          ║");
            ImpresorLento.Imprimir("║ 7. Salir del Torneo                        ║");
            ImpresorLento.Imprimir("║ 8. Volver al Menú Principal                ║");
            ImpresorLento.Imprimir("╚════════════════════════════════════════════╝");
            int op = int.Parse(Console.ReadLine()!);
            var _EquipoMenuController = new EquipoMenuController(_service);
            switch (op)
            {
                case 1:
                await _EquipoMenuController.HandleRegistrarEquipoAsync();
                    break;
                case 2:
                    await _EquipoMenuController.HandleObtenerEquipoPorIdAsync();
                    break;
                case 3:
                    await _EquipoMenuController.HandleEliminarEquipoAsync();
                    break;
                case 4:
                    await _EquipoMenuController.HandleActualizarEquipoAsync();
                    break;
                case 5:
                    await _EquipoMenuController.HandleShowEquiposAsync();
                    break;
                case 6:
                    await _EquipoMenuController.HandleAsignarEquipoAsync();
                    break;
                case 7:

                case 8:
                    return;
                default:
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    break;
            }
        }
    }
}