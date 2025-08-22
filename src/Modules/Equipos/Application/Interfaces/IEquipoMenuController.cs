using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneosv2.src.Modules.Equipos.Application.Interfaces
{
    public interface IEquipoMenuController
    {
        Task HandleRegistrarEquipoAsync();
        Task HandleActualizarEquipoAsync();
        Task HandleEliminarEquipoAsync();
        Task HandleObtenerEquipoPorIdAsync();
        Task HandleConsultarEquiposAsync();
        Task HandleAsignarEquipoAsync();
        Task HandleShowEquiposAsync();
  
        Task HandleSalirEquipoAsync();
    }
}