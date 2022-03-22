using Aguila.Core.Entities;
using Aguila.Infrastructure.Data;
using Aguila.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    class empleadosIngresosRepository : _BaseRepository<empleadosIngresos>, IempleadosIngresosRepository
    {
        public empleadosIngresosRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<empleadosIngresos> GetAllIncludes()
        {
            return _entities
                .Include(e=>e.empleado)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .AsQueryable();
        }

        public async Task<empleadosIngresos> GetByIdIncludes(long id)
        {
            return await _entities.Where(e => e.id == id)
                .Include(e => e.empleado)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }

        public async Task<empleadosIngresos> GetByIdEmpleadoIncludes(int id)
        {
            return await _entities.Where(e => e.idEmpleado == id)
                .Include(e => e.empleado)
                .Include(e => e.usuario)
                .Include(e => e.estacion)
                .FirstOrDefaultAsync();
        }

        
    }
}
