using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class AsigUsuariosEstacionesTrabajoService : IAsigUsuariosEstacionesTrabajoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public AsigUsuariosEstacionesTrabajoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //lista todas las asignaciones de estaciones de trabajo a usuarios
        public PagedList<AsigUsuariosEstacionesTrabajo> GetUsuarioEstaciones(AsigUsuariosEstacionesTrabajoQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var usuarioEstaciones = _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.GetAll();
            
            if (filter.UsuarioId != null)
            {
                usuarioEstaciones = usuarioEstaciones.Where(x => x.UsuarioId == filter.UsuarioId);
            }

            if (filter.EstacionTrabajoId != null)
            {
                usuarioEstaciones = usuarioEstaciones.Where(x => x.EstacionTrabajoId == filter.EstacionTrabajoId);
            }
            

            var pagedUsuarioEstaciones = PagedList<AsigUsuariosEstacionesTrabajo>.create(usuarioEstaciones, filter.PageNumber, filter.PageSize);
            return pagedUsuarioEstaciones;
        }

        //devuelve la asignacion especifica de usuario estacion por medio del id de ambos
        public async Task<AsigUsuariosEstacionesTrabajo> GetUsuarioEstacion(long id)
        {
            return await _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.GetByID(id);
                       
        }

        public IQueryable<AsigUsuariosEstacionesTrabajo> GetEstacionesUsuarisIncludes(long id)
        {
            return _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.GetEstacionesUsuario(id);
        }

        //inserta un nuevo registro con la asignacion de usuario estacion de trabajo
        public async Task InsertAsigUsuarioEstacion(AsigUsuariosEstacionesTrabajo asigUsuariosEstacionesTrabajo)
        {

            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(asigUsuariosEstacionesTrabajo.UsuarioId);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            //valida que exista la estacion de trabajo
            var currentEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(asigUsuariosEstacionesTrabajo.EstacionTrabajoId);
            if (currentEstacion == null)
            {
                throw new AguilaException("Estacion No Existente!....");
            }

            //valida que la asigancion de estacion de trabajo no exista
            var existeAsignacion = _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.GetAll();
            existeAsignacion = existeAsignacion.Where(x => x.UsuarioId == asigUsuariosEstacionesTrabajo.UsuarioId && 
                                                           x.EstacionTrabajoId == asigUsuariosEstacionesTrabajo.EstacionTrabajoId);
            if (existeAsignacion.Count()>0)
            {
                throw new AguilaException("Estacion Ya Asginada a Usuario!....");
            }

            await _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.Add(asigUsuariosEstacionesTrabajo);
            await _unitOfWork.SaveChangeAsync();
        }

        // Elimina una estacion de trabajo asiganada  a un usuario por medio del Id de la asignacion
        public async Task<bool> DeleteAsigUsuarioEstacionTrabajo(long id)
        {
            var asigEstacionEliminar = await _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.GetByID(id);
            if (asigEstacionEliminar == null)
            {
                throw new AguilaException("Asignacion de Estacion de Trabajo No Existente!....");
            }

            await _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }

}
