using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Torneos.Domain.Entities;

namespace Torneosv2.src.Modules.Torneos.Application.Interfaces;

public interface ITorneoService
{
    Task RegistrarTorneoAsync(int id, string nombre, string pais, string ciudad, DateTime Ifhecha, DateTime Ffecha);
    Task ActualizarTorneo(int id, string nuevoNombre, string nuevoPais, string nuevaCiudad, DateTime nuevaIfecha, DateTime nuevoFfecha);
    Task EliminarTorneo(int id);
    Task<Torneo?> ObtenerTorneosPorSuIdAsync(int id);
    Task<IEnumerable<Torneo>> ConsultarTorneosAsync();
}
