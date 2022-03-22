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
    public class controlVisitasRepository : _BaseRepository<controlVisitas> , IcontrolVisitasRepository
    {
        public controlVisitasRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<controlVisitas> GetAllIncludes()
        {
            return _entities
                .Include(e =>e.usuario)
                .Include(e => e.estacion)
                .Include(e => e.DPI)
                .AsQueryable();
        }

        public async Task<controlVisitas> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e=>e.usuario)
                .Include(e=>e.estacion)
                .FirstOrDefaultAsync();
        }

        public async Task<controlVisitas> GetByIdentificacion(string identificacion)
        {
            return await _entities.Where(e => e.identificacion.ToLower().Trim().Equals(identificacion) && e.salida == null).OrderByDescending(e=>e.ingreso)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }

        public async Task<controlVisitas> GetByIdentificacionGeneric(string identificacion)
        {
            return await _entities.Where(e => e.identificacion.ToLower().Trim().Equals(identificacion)).OrderByDescending(e => e.ingreso)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }

    }
}
