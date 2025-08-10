using System.Threading.Tasks;
using Torneosv2.src.UI;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var menuPrincipal = new MenuPrincipal();
        await menuPrincipal.RenderMenu();
    }
}