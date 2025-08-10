using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Application.Interfaces;
using Torneosv2.src.Modules.Equipos.Domain.Entities;

namespace Torneosv2.src.Modules.Equipos.Application.Services;

public class EquipoService : IEquipoService
{
    private readonly IEquipoRepository _repo;

    public EquipoService(IEquipoRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Equipo>> ConsultarEquiposAsync()
    {
        return _repo.GetAllAsync()!;
    }

    public async Task RegistrarEquipoAsync(int id, string nombre, string pais)
    {
        var existe = await _repo.GetAllAsync();
        if (existe.Any(u => u?.Nombre == nombre))
            throw new Exception("El equipo ya existe.");

        var equipo = new Equipo
        {
            Id = id,
            Nombre = nombre,
            Pais = pais
        };
        _repo.Add(equipo);
        await _repo.SaveAsync(); // Asegúrate de guardar los cambios

    }
    public async Task ActualizarEquipoAsync(int id, string nuevoNombre, string nuevoPais)
    {
        var equipo = await _repo.GetByIdAsync(id);
        if (equipo == null)
            throw new Exception($"❌ Equipo con ID {id} no encontrado.");

        equipo.Nombre = nuevoNombre;
        equipo.Pais = nuevoPais;
        _repo.Update(equipo);
        await _repo.SaveAsync();
    }
    public async Task EliminatrEquipoAsync(int id)
    {
        var equipo = await _repo.GetByIdAsync(id);
        if (equipo == null)
            throw new Exception($"❌ Equipo con ID {id} no encontrado.");

        _repo.Remove(equipo);
        await _repo.SaveAsync();
    }

    public async Task<Equipo?> ObtenerEquipoPorIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public Task EliminarEquipoAsync(int id)
    {
        throw new NotImplementedException();
    }
}
