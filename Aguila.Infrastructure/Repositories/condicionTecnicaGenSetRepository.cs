using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class condicionTecnicaGenSetRepository : _BaseRepository<condicionTecnicaGenSet>, IcondicionTecnicaGenSetRepository
    {
        public condicionTecnicaGenSetRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<condicionTecnicaGenSet> GetAllIncludes()
        {
            return _entities
                .Include(e => e.condicionActivo).ThenInclude(e => e.activoOperacion)
                .Include(e => e.condicionActivo).ThenInclude(e => e.empleado)
                .Include(e => e.condicionActivo).ThenInclude(e => e.usuario)
                .AsQueryable();
        }

        public async Task<condicionTecnicaGenSet> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.idCondicionActivo == id)
                .Include(e => e.condicionActivo)
                .FirstOrDefaultAsync();
        }

        public condicionTecnicaGenSet GetUltima(int idActivo)
        {
            return _entities
                .Include(e => e.condicionActivo).ThenInclude(e => e.activoOperacion)
                .Include(e => e.condicionActivo).ThenInclude(e => e.empleado)
                .Include(e => e.condicionActivo).ThenInclude(e => e.usuario)
                .Where(c => c.condicionActivo.idActivo == idActivo)
                .OrderByDescending(e => e.condicionActivo.fecha)
                .FirstOrDefault();
        }
    }
}
