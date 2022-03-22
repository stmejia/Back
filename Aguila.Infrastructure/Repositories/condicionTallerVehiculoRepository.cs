using System;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aguila.Infrastructure.Repositories
{
    public class condicionTallerVehiculoRepository : _BaseRepository<condicionTallerVehiculo>, IcondicionTallerVehiculoRepository
    {
        public condicionTallerVehiculoRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<condicionTallerVehiculo> GetAllIncludes()
        {
            return _entities
                .Include(e => e.vehiculos).ThenInclude(a => a.activoOperacion)
                .Include(e => e.empleados)
                .Include(e => e.estacionesTrabajo)
                .Include(e => e.usuarios)
                .AsQueryable();
        }

        public async Task<condicionTallerVehiculo> GetByIdIncludes(int idCondicion)
        {
            return await _entities.Where(e => e.id == idCondicion)
                .Include(e => e.vehiculos).ThenInclude(a => a.activoOperacion)
                .Include(e => e.empleados)
                .FirstOrDefaultAsync();
        }
    }
}
