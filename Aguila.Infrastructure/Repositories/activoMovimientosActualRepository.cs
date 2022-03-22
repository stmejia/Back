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
    public class activoMovimientosActualRepository : _BaseRepository<activoMovimientosActual>, IactivoMovimientosActualRepository
    {
        public activoMovimientosActualRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<activoMovimientosActual> GetAllIncludes()
        {
            return _entities
                .Include(e => e.activoOperacion)
                    //.ThenInclude(a => a.equipoRemolque)
                .Include(e => e.estado)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.empleado)
                .AsQueryable();
        }

        public activoMovimientosActual GetActivoOperacionByCodigo(string codigo, int empresaId)
        {
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.estado)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.empleado)
                .Where(e => e.activoOperacion.codigo.ToUpper().Trim().Contains(codigo.ToUpper().Trim()) && e.activoOperacion.idEmpresa == empresaId)
                .FirstOrDefault();
        }

        public IQueryable<activoMovimientosActual> GetAllVehiculosEstadoActual()
        {
            return _entities
                //.Include(e => e.tipoVehiculos)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
                .Include(e => e.activoOperacion.movimientoActual.estado)
                .Include(e => e.activoOperacion.movimientoActual.empleado)
                .Include(e => e.activoOperacion.movimientoActual.ruta)
                .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
                .Include(e => e.activoOperacion.movimientoActual.usuario)
                .AsQueryable();
        }
    }
}
