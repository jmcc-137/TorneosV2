using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Torneosv2.src.Shared.utils
{
    public class IdGeneretor
    {
        private static readonly Random _randon = new Random();
        private static readonly object _lock = new object();
        public static int GenerateId(int min = 1000, int max = 9999)
        {
            lock (_lock)
            {
                return _randon.Next(min, max);
            }
        }
        public static int GenerateUniqueId(IEnumerable<int> existingIds)
        {
            var existingSet = existingIds.ToHashSet() ?? new HashSet<int>();
            int newId;
            int attempts = 0;
            const int maxAttempts = 9000;

            lock (_lock)
            {
                do
                {
                    newId = _randon.Next(1000, 9999);
                    attempts++;
                    if (attempts >= maxAttempts)
                    {
                        throw new InvalidOperationException("No se pudo generar un ID único después de múltiples intentos.");
                    }
                } while (existingSet.Contains(newId));
            }
            return newId;
        }

        public static async Task<int> GenerateUniqueIdAsync<T>(
            Func<Task<IEnumerable<T>>> exixtingIdsProvider,
            Func<T, int> idSelector)
        {
            var existingEntities = await exixtingIdsProvider();
            var existingIds = existingEntities.Select(idSelector);

            return GenerateUniqueId(existingIds);
        } 
        

    }
    
}
