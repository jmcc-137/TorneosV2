using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Torneos.Application.Interfaces;
using Torneosv2.src.Modules.Torneos.Domain.Entities;

namespace Torneosv2.src.Modules.Torneos.Application.Services;

public class TorneoService : ITorneoService
{
    private readonly ITorneoRepository _repo;

    public TorneoService(ITorneoRepository repo)
    {
        _repo = repo;
    }
    public Task<IEnumerable<Torneo>> ConsultarTorneosAsync()
    {
        return _repo.GetAllAsync()!;
    }

    public async Task RegistrarTorneoAsync(int id, string nombre, string pais, string ciudad, DateTime ifecha, DateTime ffecha)
    {
        var existe = await _repo.GetAllAsync();
        if (existe.Any(u => u?.Nombre == nombre))
            throw new Exception("El torneo ya existe.");

        var torneo = new Torneo
        {
            Id = id,
            Nombre = nombre,
            Pais = pais,
            Ciudad = ciudad,
            Ifecha = ifecha,
            Ffecha = ffecha,
        };

        _repo.Add(torneo);
        _repo.Update(torneo);

    }

    public async Task ActualizarTorneo(int id, string nuevoNombre, string nuevoPais, string nuevaCiudad, DateTime nuevaIfecha, DateTime nuevaFfecha)
    {
        var torneo = await _repo.GetByIdAsync(id);

        if (torneo == null)
            throw new Exception($"❌ Torneo con ID {id} no encontrado.");
        

        torneo.Nombre = nuevoNombre;
        torneo.Pais = nuevoPais;
        torneo.Ciudad = nuevaCiudad;

        _repo.Update(torneo);
        await _repo.SaveAsync();
    }

    public async Task EliminarTorneo(int id)
    {
        var torneo = await _repo.GetByIdAsync(id);
        if (torneo == null)
            throw new Exception($"❌ Usuario con ID {id} no encontrado.");
        _repo.Remove(torneo);
        await _repo.SaveAsync();
        
    }

    public async Task<Torneo?> ObtenerTorneosPorSuIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public Task<IEnumerable<Torneo>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task RegistrarTorneoAsync(string nombre)
    {
        throw new NotImplementedException();
    }
}
