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
    public class empleadosRepository : _BaseRepository<empleados>, IempleadosRepository
    {
        public empleadosRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<empleados> GetAllIncludes()
        {
            return _entities
                .Include(e => e.empresa)
                .Include(e => e.direccion)
                .Include(e => e.Fotos)
               .AsQueryable();
        }

        public async Task<empleados> GetByIdIncludes(int id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.empresa)
                .Include(e => e.direccion).ThenInclude(e => e.municipio).ThenInclude(e => e.departamento).ThenInclude(e => e.pais)
                .FirstOrDefaultAsync();
        }

        public async Task<empleados> GetEmpleadoByCuiIncludes(string cui)
        {
            return await _entities.Where(e => e.codigo.ToUpper().Trim().Equals(cui.ToUpper().Trim()))
                .Include(e => e.empresa)
                .Include(e => e.direccion)
                .FirstOrDefaultAsync();
        }
    }
}
