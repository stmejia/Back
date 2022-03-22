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
    public class activoMovimientosRepository : _BaseRepository<activoMovimientos>, IactivoMovimientosRepository
    {
        public activoMovimientosRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<activoMovimientos> GetAllIncludes()
        {
            return _entities
                .Include(e=>e.activoOperacion)
                .Include(e=>e.estado)
                .Include(e=>e.estacionTrabajo)
                //.Include(e=>e.servicio)
                .Include(e=>e.ubicacion)
                .Include(e=>e.piloto)
                .Include(e=>e.ubicacion)
                .Include(e=>e.ruta)
                .Include(e=>e.usuario)
                .AsQueryable();
        }

        public IQueryable<activoMovimientos> ReporteMovimientosByUser(int idEmpresa, int idUsuario,DateTime fechaIni, DateTime fechaFin)
        {
            //Envia un listado de movimientos de activo por empresa y usuario, el usuario debe de tener asignadas las estaciones de trabajo a evaluar
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.estado)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.ubicacion)
                .Include(e => e.piloto)
                .Include(e => e.ubicacion)
                .Include(e => e.ruta)
                .Include(e => e.usuario)
                .Where(m => m.activoOperacion.idEmpresa == idEmpresa 
                    && m.fecha >= fechaIni && m.fecha <= fechaFin.AddDays(1)
                    && m.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(ae => ae.UsuarioId == idUsuario)
                ).AsQueryable();
        }






    }
}
