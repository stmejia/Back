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
    public class generadoresRepository : _BaseRepository<generadores>, IgeneradoresRepository
    {
        public generadoresRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<generadores> GetAllIncludes()
        {
            return _entities
                .Include(e => e.tipoGenerador)
                .Include(e => e.activoOperacion)
                    .ThenInclude(e => e.transporte)                
                .AsQueryable();
        }

        public  IQueryable<generadores> GetAllEstadoActual()
        {
            return _entities
                .Include(e => e.tipoGenerador)
                .Include(e => e.activoOperacion)
                    .ThenInclude(e => e.transporte)                
                .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
                .Include(e => e.activoOperacion.movimientoActual.estado)
                .Include(e => e.activoOperacion.movimientoActual.empleado)
                .Include(e => e.activoOperacion.movimientoActual.ruta)
                .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
                .Include(e => e.activoOperacion.movimientoActual.usuario)
                .AsQueryable();
        }

        public IQueryable<generadores> reporteInventario(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.tipoGenerador)
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
