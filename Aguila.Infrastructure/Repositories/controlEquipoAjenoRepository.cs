using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class controlEquipoAjenoRepository : _BaseRepository<controlEquipoAjeno>, IcontrolEquipoAjenoRepository
    {
        public controlEquipoAjenoRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<controlEquipoAjeno> GetAllIncludes()
        {
            return _entities
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .AsQueryable();
        }

        public async Task<controlEquipoAjeno> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }
    }
}
