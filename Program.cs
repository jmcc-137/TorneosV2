using System.Threading.Tasks;
using Torneosv2.src.UI;
using Torneosv2.src.Shared.utils;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var saludo = new Bienvenida(); 
        saludo.MensajeBienvenida();
        var menuPrincipal = new MenuPrincipal();
        await menuPrincipal.RenderMenu();
    }
}