using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Personas.Infrastructure.Repository;
using Torneosv2.src.Modules.Torneos.Application.Services;
using Torneosv2.src.Modules.Torneos.Domain.Entities;
using Torneosv2.src.Shared.utils;
using Torneosv2.src.Modules.Torneos.Infrastructure.Repository;
using Torneosv2.src.Shared.Context;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using System.Reflection.Metadata;

namespace Torneosv2.src.Modules.Torneos.UI;

public class MenuTorneo
{
    private readonly AppDbContext _context;
    readonly TorneoRepository repo = null!;
    readonly TorneoService _service = null!;

    public MenuTorneo(AppDbContext context)
    {
        _context = context;
        repo = new TorneoRepository(context);
        _service = new TorneoService(repo);
    }
    public async Task RenderMenu()
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            ImpresorLento.Imprimir("╔════════════════════════════════╗");
            ImpresorLento.Imprimir("║      MENÚ DE TORNEOS           ║");
            ImpresorLento.Imprimir("╠════════════════════════════════╣");
            ImpresorLento.Imprimir("║ 1. ➕  Agregar Torneo          ║");
            ImpresorLento.Imprimir("║ 2. 🔍  Buscar Torneo(ID)       ║");
            ImpresorLento.Imprimir("║ 3. ❌  Eliminar Torneo(ID)     ║");
            ImpresorLento.Imprimir("║ 4. ✏️  Actualizar Torneo        ║");
            ImpresorLento.Imprimir("║ 5. 👀  Mostrar Torneos         ║");
            ImpresorLento.Imprimir("║ 6. 🔙  Volver al Menú Principal║");
            ImpresorLento.Imprimir("╚════════════════════════════════╝");
            
            var _torneoMenuController = new TorneoMenuController(_service);
            int op;
            Console.Write("Seleccione una opción: ");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out op))
            {
                    Console.WriteLine("Debe ingresar un número válido. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
            }
            switch (op)
            {
                case 1:
                    await _torneoMenuController.HandleCreateTorneoAsync();
                    break;
                case 2:
                    await _torneoMenuController.HandleSearchTorneoAsync();
                    break;
                case 3:
                    await _torneoMenuController.HandleDeleteTorneoAsync();
                    break;
                case 4:
                    await _torneoMenuController.HandleUpdateTorneoAsync();
                    break;
                case 5:
                    await _torneoMenuController.HandleShowTorneosAsync();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }

        }
    }
}