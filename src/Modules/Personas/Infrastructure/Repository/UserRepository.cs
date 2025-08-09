using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneosv2.src.Shared.Context;

namespace Torneosv2.src.Modules.Personas.Infrastructure.Repository
{
    public class UserRepository
    {
        private AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}