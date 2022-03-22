using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class controlContratistasRepository : _BaseRepository<controlContratistas>, IcontrolContratistasRepository
    {
        public controlContratistasRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<controlContratistas> GetAllIncludes()
        {
            return _entities
                .Include(u => u.usuario)
                .Include(u => u.estacion)
                .Include(d => d.DPI)
                .AsQueryable();
        }

        public async Task<controlContratistas> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }

        public async Task<controlContratistas> GetByIdentificacion(string identificacion)
        {
            return await _entities.Where(e => e.identificacion.ToLower().Trim().Equals(identificacion) && e.salida == null).OrderByDescending(e => e.ingreso)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }

        public async Task<controlContratistas> GetByIdentificacionGeneric(string identificacion)
        {
            return await _entities.Where(e => e.identificacion.ToLower().Trim().Equals(identificacion)).OrderByDescending(e => e.ingreso)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }
    }
}
