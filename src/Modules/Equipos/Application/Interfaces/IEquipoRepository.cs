using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Modules.Equipos.Domain.Entities;

namespace Torneosv2.src.Modules.Equipos.Application.Interfaces
{
    public interface IEquipoRepository
    {
        Task<Equipo?> GetByIdAsync(int id);
        Task<IEnumerable<Equipo?>> GetAllAsync();
        void Add(Equipo entity);
        void Remove(Equipo entity);
        void Update(Equipo entity);
        Task SaveAsync();       
    }
}