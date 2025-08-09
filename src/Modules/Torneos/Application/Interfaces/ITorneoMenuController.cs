using System.Threading.Tasks;
using Torneosv2.src.Modules.Torneos.Domain.Entities;


namespace Torneosv2.src.Modules.Torneos.Application.Interfaces
{
    /// <summary>
    /// Puerto para el controlador de menú de torneos (Use Case)
    /// </summary>
    public interface ITorneoMenuController
    {
        Task HandleCreateTorneoAsync();
        Task HandleShowTorneosAsync();
        Task HandleSearchTorneoAsync();
        Task HandleDeleteTorneoAsync();
        Task HandleUpdateTorneoAsync();
    }
}
