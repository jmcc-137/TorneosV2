using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Application.Services;
using Torneosv2.src.Modules.Equipos.Infrastructure.Repository;
using Torneosv2.src.Modules.TorneoEquipos.Application.Services;
using Torneosv2.src.Modules.TorneoEquipos.Infrastructure.Repository;
using Torneosv2.src.Modules.Torneos.Application.Services;
using Torneosv2.src.Modules.Torneos.Infrastructure.Repository;
using Torneosv2.src.Shared.Context;
using Torneosv2.src.Shared.utils;

namespace Torneosv2.src.Modules.Equipos.UI;


public class MenuEquipo
{
    private readonly AppDbContext _context;

    readonly EquipoRepository repo = null!;
    readonly EquipoService _service = null!;
    readonly TorneoEquiposRepository torneoEquiposRepo = null!;
    readonly TorneoEquiposService torneoEquiposService = null!;
    readonly TorneoRepository torneoRepo = null!;
    readonly TorneoService torneoService = null!;

    public MenuEquipo(AppDbContext context)
    {
        _context = context;
        repo = new EquipoRepository(_context);
        _service = new EquipoService(repo);

        // Instanciar repositorio y servicio de torneos
        torneoRepo = new TorneoRepository(_context);
        torneoService = new TorneoService(torneoRepo);

        torneoEquiposRepo = new TorneoEquiposRepository(_context);
        torneoEquiposService = new TorneoEquiposService(torneoEquiposRepo, torneoService, _service);
    }
    public async Task RenderMenu()
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();

            ImpresorLento.Imprimir("╔════════════════════════════════════════════════════════╗");
            ImpresorLento.Imprimir("║                 🏆  REGISTRO DE EQUIPOS  ⚽            ║");
            ImpresorLento.Imprimir("╠════════════════════════════════════════════════════════╣");
            ImpresorLento.Imprimir("║ 1.  1️⃣  Registrar Equipo                                ║");
            ImpresorLento.Imprimir("║ 2.  🔎  Buscar Equipo (ID)                             ║");
            ImpresorLento.Imprimir("║ 3.  ❌  Eliminar Equipo (ID)                           ║");
            ImpresorLento.Imprimir("║ 4.  ✏️  Actualizar Equipo                               ║");
            ImpresorLento.Imprimir("║ 5.  📋  Mostrar Equipos                                ║");
            ImpresorLento.Imprimir("║ 6.  🤝  Asignar Equipo                                 ║");
            ImpresorLento.Imprimir("║ 7.  🚪  Salir del Torneo                               ║");
            ImpresorLento.Imprimir("║ 8.  👀  Ver equipos asignados                          ║");
            ImpresorLento.Imprimir("║ 9.  🔙  Volver al Menú Principal                       ║");
            ImpresorLento.Imprimir("╚════════════════════════════════════════════════════════╝");

            Console.Write("Seleccione una opción: ");
            var input = Console.ReadLine();

            int op;
            if (!int.TryParse(input, out op))
            {
                Console.WriteLine("Debe ingresar un número válido. Presione cualquier tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            var _EquipoMenuController = new EquipoMenuController(_service, torneoEquiposService, torneoService);
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
                    await _EquipoMenuController.HandleSalirEquipoAsync();
                    break;
                case 8:
                    await _EquipoMenuController.HandleShowEquiposAsignadosAsync();
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    break;
            }
        }
    }
}