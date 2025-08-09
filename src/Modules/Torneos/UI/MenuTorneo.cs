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
            int op = int.Parse(Console.ReadLine()!);
            var _torneoMenuController = new TorneoMenuController(_service);
            switch (op)
            {
                case 1:
                    await _torneoMenuController.HandleCreateTorneoAsync();
                    break;
                case 2:
                    await _torneoMenuController.HandleSearchTorneoAsync ();
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


    

    private async Task ActualizarTorneo()
    {
        Console.Write("ID a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var existente = await _service.ObtenerTorneosPorSuIdAsync(id);
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

            await _service.ActualizarTorneo(id,nuevoNombre, nuevoPais, nuevaCiudad, NfechaInicio, NfechaFinal);
            Console.WriteLine("País actualizado.");



        }
    }
}
    
    


