using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class UsuariosRecursosService : IUsuariosRecursosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public UsuariosRecursosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<UsuariosRecursos> GetUsuariosRecursos(UsuariosRecursosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var usuarioRecursos = _unitOfWork.UsuariosRecursosRepository.GetAll();

            if (filter.usuario_id != null)
            {
                usuarioRecursos = usuarioRecursos.Where(x => x.usuario_id == filter.usuario_id);
            }

            if (filter.estacionTrabajo_id != null)
            {
                usuarioRecursos = usuarioRecursos.Where(x => x.estacionTrabajo_id == filter.estacionTrabajo_id);
            }

            if (filter.recurso_id != null)
            {
                usuarioRecursos = usuarioRecursos.Where(x => x.recurso_id == filter.recurso_id);
            }

            var pagedUsuarioRecursos = PagedList<UsuariosRecursos>.create(usuarioRecursos, filter.PageNumber, filter.PageSize);

            return pagedUsuarioRecursos;
        }

        //retorna una los recursos asignados a un  Usuario  por medio de su Id
        public  IEnumerable<UsuariosRecursos> GetUsuarioRecurso(long id)
        {
            //return await _unitOfWork.UsuariosRecursosRepository.GetByID(id);
            return  _unitOfWork.UsuariosRecursosRepository.GetUsuarioRecursos(id);
        }

        public IQueryable<UsuariosRecursos> GetUsuarioRecursoIncludes(long id)
        {
            return _unitOfWork.UsuariosRecursosRepository.GetAllIncludes(id);
        }

        //Inserta una nueva asigancion de RecursoAtributo
        public async Task InsertUsuarioRecurso(UsuariosRecursos usuarioRecurso)
        {
            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioRecurso.usuario_id);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....", 404);
            }



            //valida que la Estacion de trabajo exista
            var currentEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(usuarioRecurso.estacionTrabajo_id);
            if (currentEstacion == null)
            {
                throw new AguilaException("Estacion de Trabajo No Existente!....", 404);
            }

            //valida que el Recurso exista
            var currentRecurso = await _unitOfWork.RecursosRepository.GetByID(usuarioRecurso.recurso_id);
            if (currentRecurso == null)
            {
                throw new AguilaException("Recurso No Existente!....", 404);
            }

            //valida que la asignacion no exista
            UsuariosRecursosQueryFilter filter = new UsuariosRecursosQueryFilter
            {
                usuario_id = usuarioRecurso.usuario_id,
                estacionTrabajo_id = usuarioRecurso.estacionTrabajo_id,
                recurso_id = usuarioRecurso.recurso_id

            };

            var currentAsginacion = GetUsuariosRecursos(filter);
            if (currentAsginacion.LongCount() > 0)
            {
                throw new AguilaException("Asignacion Duplicada!....",406);
            }



            //Elimina las opciones repetidas, espacios en blanco y entradas vacias.            
            string opciones = string.Join(",", usuarioRecurso.opcionesAsignadas
                                               .Split(',')
                                               .Select(x => x.Trim())
                                               .Where(x => !string.IsNullOrWhiteSpace(x))
                                               .Distinct()
                                               .ToArray());
            
            usuarioRecurso.opcionesAsignadas = opciones.ToString();
            usuarioRecurso.id = 0;

            await _unitOfWork.UsuariosRecursosRepository.Add(usuarioRecurso);
            await _unitOfWork.SaveChangeAsync();
        }

        //Actualiza una asignacion de UsuarioRecurso
        public async Task<bool> UpdateUsuarioRecurso(UsuariosRecursos usuarioRecurso)
        {
            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioRecurso.usuario_id);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            //valida que la Estacion de trabajo exista
            var currentEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(usuarioRecurso.estacionTrabajo_id);
            if (currentEstacion == null)
            {
                throw new AguilaException("Estacion de Trabajo No Existente!....");
            }

            //valida que el Recurso exista
            var currentRecurso = await _unitOfWork.RecursosRepository.GetByID(usuarioRecurso.recurso_id);
            if (currentRecurso == null)
            {
                throw new AguilaException("Recurso No Existente!....");
            }

            //valida que la asignacion  exista
            UsuariosRecursosQueryFilter filter = new UsuariosRecursosQueryFilter
            {
                usuario_id = usuarioRecurso.usuario_id,
                estacionTrabajo_id = usuarioRecurso.estacionTrabajo_id,
                recurso_id = usuarioRecurso.recurso_id

            };

            var currentAsginacion = await _unitOfWork.UsuariosRecursosRepository.GetByID(usuarioRecurso.id);
            if (currentAsginacion == null)
            {
                throw new AguilaException("Asignacion No Existente!....");
            }

            currentAsginacion.id = usuarioRecurso.id;
            currentAsginacion.estacionTrabajo_id = usuarioRecurso.estacionTrabajo_id;
            currentAsginacion.recurso_id = usuarioRecurso.recurso_id;
            currentAsginacion.usuario_id = usuarioRecurso.usuario_id;

            //Elimina las opciones repetidas, espacios en blanco y entradas vacias.            
            string opciones = string.Join(",", usuarioRecurso.opcionesAsignadas.Split(',')
                                               .Select(x => x.Trim())
                                               .Where(x => !string.IsNullOrWhiteSpace(x))
                                               .Distinct()
                                               .ToArray());

            usuarioRecurso.opcionesAsignadas = opciones;

            currentAsginacion.opcionesAsignadas = usuarioRecurso.opcionesAsignadas;

            _unitOfWork.UsuariosRecursosRepository.Update(currentAsginacion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteUsuarioRecurso(long id)
        {
            var asignacionEliminar = await _unitOfWork.UsuariosRecursosRepository.GetByID(id);
            if (asignacionEliminar == null)
            {
                throw new AguilaException("Asinacion no Existe!....");
            }

            await _unitOfWork.UsuariosRecursosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
