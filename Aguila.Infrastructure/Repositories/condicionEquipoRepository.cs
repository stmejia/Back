using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Infrastructure.Repositories
{
    public class condicionEquipoRepository : _BaseRepository<condicionEquipo>, IcondicionEquipoRepository
    {
        public condicionEquipoRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<condicionEquipo> GetAllIncludes()
        {
            return _entities
                .Include(e => e.condicionActivo).ThenInclude(e => e.activoOperacion)
                .Include(e => e.condicionActivo).ThenInclude(e => e.empleado)
                .Include(e => e.condicionActivo).ThenInclude(e => e.usuario)
                .Include(e => e.condicionActivo).ThenInclude(e => e.estacionTrabajo)
                .AsQueryable();
        }

        public condicionEquipo GetUltima(int idActivo)
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
