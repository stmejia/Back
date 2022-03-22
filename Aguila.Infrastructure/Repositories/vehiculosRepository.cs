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
    public class vehiculosRepository : _BaseRepository<vehiculos>, IvehiculosRepository
    {
        public vehiculosRepository(AguilaDBContext context) : base(context) { }

        //public IEnumerable<vehiculos> GetAllIncludes()
        //{
        //    return _entities
        //        .Include(e => e.tipoVehiculos)
        //        .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
        //        .Include(e => e.imagenTarjetaCirculacion)
        //        .AsEnumerable();
        //}

        public IQueryable<vehiculos> GetAllIncludes()
        {
            return _entities
                .Include(e => e.tipoVehiculos)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.imagenTarjetaCirculacion)
                .AsQueryable();
        }

        //public IEnumerable<vehiculos> GetAllEstadoActual()
        //{
        //    return _entities
        //        .Include(e => e.tipoVehiculos)
        //        .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
        //        .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
        //        .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
        //        .Include(e => e.activoOperacion.movimientoActual.estado)
        //        .Include(e => e.activoOperacion.movimientoActual.empleado)
        //        .Include(e => e.activoOperacion.movimientoActual.ruta)
        //        .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
        //        .Include(e => e.activoOperacion.movimientoActual.usuario)
        //        .AsEnumerable();
        //}

        public IQueryable<vehiculos> GetAllEstadoActual()
        {
            return _entities
                .Include(e => e.tipoVehiculos)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
                .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
                .Include(e => e.activoOperacion.movimientoActual.estado)
                .Include(e => e.activoOperacion.movimientoActual.empleado)
                .Include(e => e.activoOperacion.movimientoActual.ruta)
                .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
                .Include(e => e.activoOperacion.movimientoActual.usuario)
                .AsQueryable();
        }

        //public IEnumerable<vehiculos> reporteInventario(int idEmpresa, int idUsuario)
        //{
        //    return _entities
        //        .Include(e => e.tipoVehiculos)
        //        .Include(e => e.activoOperacion).ThenInclude(e => e.transporte)
        //        .Include(e => e.activoOperacion.movimientoActual).ThenInclude(e => e.servicio)
        //        .Include(e => e.activoOperacion.movimientoActual.estado)
        //        .Include(e => e.activoOperacion.movimientoActual.empleado)
        //        .Include(e => e.activoOperacion.movimientoActual.ruta)
        //        .Include(e => e.activoOperacion.movimientoActual.estacionTrabajo)
        //        .Include(e => e.activoOperacion.movimientoActual.usuario)
        //        .Where(e => e.activoOperacion.idEmpresa == idEmpresa
        //         && e.activoOperacion.movimientoActual.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(a => a.UsuarioId == idUsuario))
        //        .AsEnumerable();
        //}

        public IQueryable<vehiculos> reporteInventario(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.tipoVehiculos)
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

        public bool esTipo(string tipo, int idActivo)
        {
            var xVehiculo = _entities
                .Include(v => v.tipoVehiculos)
                .Where(v => v.idActivo == idActivo)
                .FirstOrDefault();
            //&& v.tipoVehiculos.prefijo.ToString().ToUpper().Contains(tipo.ToUpper().Trim())
            if (xVehiculo == null)
                return false;

            return xVehiculo.tipoVehiculos.prefijo.ToString().ToUpper().Contains(tipo.ToUpper().Trim());

        }
    }
}
