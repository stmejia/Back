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
    public class equipoRemolqueRepository : _BaseRepository<equipoRemolque>, IequipoRemolqueRepository
    {
        public equipoRemolqueRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<equipoRemolque> GetAllIncludes()
        {
            return _entities
                .Include(e => e.tipoEquipoRemolque)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.imagenTarjetaCirculacion)
                .AsQueryable();
        }

        public async Task<equipoRemolque> GetByIdIncludes(int id)
        {
            return await _entities.Where(e => e.idActivo == id)
                .Include(e => e.tipoEquipoRemolque)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
                .Include(e => e.activoOperacion.movimientoActual.estado)
                .Include(e => e.activoOperacion.movimientoActual.empleado)
                .Include(e => e.activoOperacion.movimientoActual.ruta)
                .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
                .Include(e => e.activoOperacion.movimientoActual.usuario)
                .FirstOrDefaultAsync();
        }

        public IQueryable<equipoRemolque> GetAllEstadoActual()
        {
            return _entities
                .Include(e => e.tipoEquipoRemolque)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
                .Include(e => e.activoOperacion.movimientoActual.estado)
                .Include(e => e.activoOperacion.movimientoActual.empleado)
                .Include(e => e.activoOperacion.movimientoActual.ruta)
                .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
                .Include(e => e.activoOperacion.movimientoActual.usuario)
                .AsQueryable();
        }

        public IQueryable<equipoRemolque> reporteInventario(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.tipoEquipoRemolque)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
                .Include(e => e.activoOperacion.movimientoActual.estado)
                .Include(e => e.activoOperacion.movimientoActual.empleado)
                .Include(e => e.activoOperacion.movimientoActual.ruta)
                .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
                .Include(e => e.activoOperacion.movimientoActual.usuario)
                .Where(e => e.activoOperacion.idEmpresa == idEmpresa
                 && e.activoOperacion.movimientoActual.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(a => a.UsuarioId == idUsuario))
                .AsQueryable();
        }       
    }
}
