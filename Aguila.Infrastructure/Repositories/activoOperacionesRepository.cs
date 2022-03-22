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
    public class activoOperacionesRepository : _BaseRepository<activoOperaciones>, IactivoOperacionesRepository
    {
        public activoOperacionesRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<activoOperaciones> GetAllIncludes()
        {
            return _entities
                .Include(e => e.transporte)
                .Include(e => e.movimientoActual)
                .Include(e => e.Fotos)
                .AsQueryable();
        }

        public async Task<activoOperaciones> GetByIdIncludes(int id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.transporte)
                .Include(e => e.movimientoActual)
                .FirstOrDefaultAsync();
        }
    }
}
