using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneosv2.src.Modules.Equipos.Application.Interfaces;
using Torneosv2.src.Modules.Equipos.Domain.Entities;
using Torneosv2.src.Shared.Context;


namespace Torneosv2.src.Modules.Equipos.Infrastructure.Repository;


public class EquipoRepository : IEquipoRepository
{
    private readonly AppDbContext _context;

    public EquipoRepository(AppDbContext context)
    {
        _context = context;
    }
       public Task<Equipo?> GetByIdAsync(int id)
    {
        return _context.Equipos
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Equipo?>> GetAllAsync() =>
        await _context.Equipos.ToListAsync();

    public void Add(Equipo entity) =>
        _context.Equipos.Add(entity);

    public void Remove(Equipo entity) =>
        _context.Equipos.Remove(entity);

    public void Update(Equipo entity) =>
        _context.Equipos.Update(entity);
    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();

 
}
