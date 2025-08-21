using Microsoft.EntityFrameworkCore;
using Torneosv2.src.Modules.TorneoEquipos.Domain.Entities;
using Torneosv2.src.Modules.TorneoEquipos.Application.Interfaces;
using Torneosv2.src.Shared.Context;

namespace Torneosv2.src.Modules.TorneoEquipos.Infrastructure.Repository
{
    public class TorneoEquiposRepository : ITorneoEquiposRepository
    {
        private readonly AppDbContext _context;

        public TorneoEquiposRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteAsignacionAsync(int torneoId, int equipoId)
        {
            return await _context.TorneoEquipos
                .AnyAsync(te => te.TorneoId == torneoId && te.EquipoId == equipoId);
        }

        public async Task AsignarEquipoAsync(int torneoId, int equipoId)
        {
            if (await ExisteAsignacionAsync(torneoId, equipoId))
            {
                throw new InvalidOperationException($"El equipo {equipoId} ya está asignado al torneo {torneoId}");
            }


            var asignacion = new Domain.Entities.TorneoEquipos
            {
                TorneoId = torneoId,
                EquipoId = equipoId
            };

            await _context.TorneoEquipos.AddAsync(asignacion);
            await _context.SaveChangesAsync();
        }

        public async Task DesasignarEquipoAsync(int torneoId, int equipoId)
        {
            var asignacion = await _context.TorneoEquipos
                .FirstOrDefaultAsync(te => te.TorneoId == torneoId && te.EquipoId == equipoId);

            if (asignacion != null)
            {
                _context.TorneoEquipos.Remove(asignacion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<int>> ObtenerEquiposAsignadosAsync(int torneoId)
        {
            return await _context.TorneoEquipos
                .Where(te => te.TorneoId == torneoId)
                .Select(te => te.EquipoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> ObtenerTorneosDelEquipoAsync(int equipoId)
        {
            return await _context.TorneoEquipos
                .Where(te => te.EquipoId == equipoId)
                .Select(te => te.TorneoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Entities.TorneoEquipos>> ObtenerTodasLasAsignacionesAsync()
        {
            return await _context.TorneoEquipos
                .Include(te => te.Torneo)
                .Include(te => te.Equipo)
                .ToListAsync();
        }

        public async Task<int> ContarEquiposEnTorneoAsync(int torneoId)
        {
            return await _context.TorneoEquipos
                .CountAsync(te => te.TorneoId == torneoId);
        }

        public async Task<int> ContarTorneosDelEquipoAsync(int equipoId)
        {
            return await _context.TorneoEquipos
                .CountAsync(te => te.EquipoId == equipoId);
        }
    }
}