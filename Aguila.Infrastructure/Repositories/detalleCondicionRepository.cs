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
    public class detalleCondicionRepository : _BaseRepository<detalleCondicion>, IdetalleCondicionRepository
    {
        public detalleCondicionRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<detalleCondicion> GetAllIncludes()
        {
            return _entities
                .Include(e => e.usuarios)
                .Include(e => e.usuarioAutoriza)
                .Include(e => e.reparaciones)
                .AsQueryable();
        }

        public async Task<detalleCondicion> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.usuarios)
                .Include(e => e.usuarioAutoriza)
                .Include(e => e.reparaciones)
                .FirstOrDefaultAsync();
        }
    }
}
