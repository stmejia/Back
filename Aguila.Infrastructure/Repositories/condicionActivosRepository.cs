using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aguila.Infrastructure.Repositories
{
    public class condicionActivosRepository : _BaseRepository<condicionActivos>, IcondicionActivosRepository
    {
        public condicionActivosRepository(AguilaDBContext context) : base(context){ }

        public IQueryable<condicionActivos> GetAllIncludes()
        {
            return _entities
                .Include(u => u.usuario)
                .Include(a => a.activoOperacion).ThenInclude(f => f.Fotos)             
                .Include(e => e.estacionTrabajo)
                .Include(em => em.empleado)
                .Include(ub=>ub.ubicacionEntrega)
                .Include(f => f.ImagenFirmaPiloto)
                .Include(f => f.Fotos)
               .AsQueryable();    
        }

        public IQueryable<condicionActivos> reporteCondicionesVehiculos(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.empleado)
                .Include(e => e.usuario)
                .Include(e=>e.estado)
                .Where(e => e.activoOperacion.idEmpresa == idEmpresa
                 && e.tipoCondicion.ToLower().Trim().Equals("cabezal") && e.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(a => a.UsuarioId == idUsuario))
                .AsQueryable();
        }

        public IQueryable<condicionActivos> reporteCondicionesEquipos(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.empleado)
                .Include(e => e.usuario)
                .Include(e => e.estado)
                .Where(e => e.activoOperacion.idEmpresa == idEmpresa
                 && e.tipoCondicion.ToLower().Trim().Equals("equipo") && e.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(a => a.UsuarioId == idUsuario))
                .AsQueryable();
        }

        public IQueryable<condicionActivos> reporteCondicionesFurgones(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.empleado)
                .Include(e => e.usuario)
                .Include(e => e.estado)
                .Where(e => e.activoOperacion.idEmpresa == idEmpresa
                 && e.tipoCondicion.ToLower().Trim().Equals("furgon") && e.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(a => a.UsuarioId == idUsuario))
                .AsQueryable();
        }

        public IQueryable<condicionActivos> reporteCondicionesGeneradores(int idEmpresa, int idUsuario)
        {
            return _entities
                .Include(e => e.activoOperacion)
                .Include(e => e.estacionTrabajo)
                .Include(e => e.empleado)
                .Include(e => e.usuario)
                .Include(e => e.estado)
                .Where(e => e.activoOperacion.idEmpresa == idEmpresa
                 && e.tipoCondicion.ToLower().Trim().Equals("generador") && e.estacionTrabajo.AsigUsuariosEstacionesTrabajo.Any(a => a.UsuarioId == idUsuario))
                .AsQueryable();
        }

    }


}
