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
    public class eventosControlEquipoRepository : _BaseRepository<eventosControlEquipo>, IeventosControlEquipoRepository
    {
        public eventosControlEquipoRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<eventosControlEquipo> GetAllIncludes()
        {
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.usuarioCreacion)
                .Include(e => e.usuarioRevisa)
                .Include(e => e.usuarioResuelve)
                .Include(e => e.usuarioAnula)
                .Include(e => e.estacionTrabajo)
                .AsQueryable();
        }

        public async Task<eventosControlEquipo> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.activoOperacion)
                .Include(e => e.usuarioCreacion)
                .Include(e => e.usuarioRevisa)
                .Include(e => e.usuarioResuelve)
                .Include(e => e.usuarioAnula)
                .Include(e => e.estacionTrabajo)
                .FirstOrDefaultAsync();
        }
    }
}
