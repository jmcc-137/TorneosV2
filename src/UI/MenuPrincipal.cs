using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.UI;
using Torneosv2.src.Modules.Torneos.UI;
using Torneosv2.src.Shared.Context;
using Torneosv2.src.Shared.Helpers;
using Torneosv2.src.Shared.utils;

namespace Torneosv2.src.UI
{
    public class MenuPrincipal
    {
        private readonly AppDbContext _context;

        public MenuPrincipal()
        {
            _context = DbContextFactory.Create();
        }
        public async Task RenderMenu()
        {

      var context = DbContextFactory.Create();



            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                ImpresorLento.Imprimir("╔════════════════════════════════════╗");
                ImpresorLento.Imprimir("║      ⚽  SISTEMA DE TORNEOS ⚽     ║");
                ImpresorLento.Imprimir("╚════════════════════════════════════╝");
                Console.ResetColor();
                ImpresorLento.Imprimir("0. 🏆  Crear Torneo");
                ImpresorLento.Imprimir("1. 🛡️  Registro de Equipos");
                ImpresorLento.Imprimir("2. 👟  Registro de Jugadores");
                ImpresorLento.Imprimir("3. 💰  Transferencias (Compra, Préstamo)");
                ImpresorLento.Imprimir("4. 📊  Estadísticas");
                ImpresorLento.Imprimir("5. ❌  Salir");

                Console.Write("Seleccione una opción: ");
                var input = Console.ReadLine();

                int op;
                if (!int.TryParse(input, out op))
                {
                    Console.WriteLine("Debe ingresar un número válido. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                switch (op)
                {
                    case 0:
                        await new MenuTorneo(_context).RenderMenu();
                        break;
                    case 1:
                        await new MenuEquipo(_context).RenderMenu();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }


            }
        }
    }
}