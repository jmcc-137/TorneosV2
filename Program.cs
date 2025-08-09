
using Torneosv2.src.Modules.Torneos.UI;
using Torneosv2.src.Shared.Helpers;
using Torneosv2.src.Shared.utils;


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
    int op = int.Parse(Console.ReadLine()!);
    switch (op)
    {
        case 0:
            await new MenuTorneo(context).RenderMenu();
            break;
        case 5:
            return;
        default:
            Console.WriteLine("Opcion invalida");
            break;
    }


}