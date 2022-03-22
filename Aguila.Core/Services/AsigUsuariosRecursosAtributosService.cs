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
    public class AsigUsuariosRecursosAtributosService : IAsigUsuariosRecursosAtributosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public AsigUsuariosRecursosAtributosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //devuelve todo el contenido de la tabla AsigUsuariosRecursosAtributos
        public PagedList<AsigUsuariosRecursosAtributos> GetAsigRecursosAtributos(AsigUsuariosRecursosAtributosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var recursosAtributos = _unitOfWork.AsigUsuariosRecursosAtributosRepository.GetAll();

            if (filter.UsuarioId != null)
            {
                recursosAtributos = recursosAtributos.Where(x => x.UsuarioId == filter.UsuarioId);
            }

            if(filter.EstacionTrabajoId != null)
            {
                recursosAtributos = recursosAtributos.Where(x => x.EstacionTrabajoId == filter.EstacionTrabajoId);
            }

            if (filter.ModuloId != null)
            {
                recursosAtributos = recursosAtributos.Where(x => x.ModuloId == filter.ModuloId);
            }

            if (filter.RecursoAtributosId != null)
            {
                recursosAtributos = recursosAtributos.Where(x => x.RecursoAtributosId == filter.RecursoAtributosId);
            }

            var pagedModulos = PagedList<AsigUsuariosRecursosAtributos>.create(recursosAtributos, filter.PageNumber, filter.PageSize);

            return pagedModulos;
        }

        //devuelve los recursos atributos asociados a un ID
        public async Task<AsigUsuariosRecursosAtributos> GetAsigUsuarioRecursoAtributo(long id)
        {
            return await _unitOfWork.AsigUsuariosRecursosAtributosRepository.GetByID(id);
        }

        

        //inserta un nuevo registro 
        public async Task insertAsigUsuarioRecursoAtributo(AsigUsuariosRecursosAtributos usuarioRecursoAtributo)
        {
            var usuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioRecursoAtributo.UsuarioId);
            if (usuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            var estacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(usuarioRecursoAtributo.EstacionTrabajoId);
            if (estacion == null)
            {
                throw new AguilaException("Estacion No Existente!....");
            }

            var modulo = await _unitOfWork.ModulosRepository.GetByID(usuarioRecursoAtributo.ModuloId);
            if (modulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            var recurso = await _unitOfWork.RecursosAtributosRepository.GetByID(usuarioRecursoAtributo.RecursoAtributosId);
            if (recurso == null)
            {
                throw new AguilaException("Recurso-Atributo No Existente!....");
            }

            //valida que no sea una asignacion duplicada
            var existeAsignacion = _unitOfWork.AsigUsuariosRecursosAtributosRepository.GetAll();
            existeAsignacion = existeAsignacion.Where(x => x.UsuarioId == usuarioRecursoAtributo.UsuarioId &&
                                                x.EstacionTrabajoId == usuarioRecursoAtributo.EstacionTrabajoId &&
                                                x.ModuloId == usuarioRecursoAtributo.ModuloId &&
                                                x.RecursoAtributosId == usuarioRecursoAtributo.RecursoAtributosId);

            if (existeAsignacion.Count() > 0)
            {
                throw new AguilaException("Asignacion de Recursos Ya Existente!....");
            }

            await _unitOfWork.AsigUsuariosRecursosAtributosRepository.Add(usuarioRecursoAtributo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> updateUsuarioRecursoAtributo(AsigUsuariosRecursosAtributos usuarioRecursoAtributo)
        {
            var currentRecursoAtributo = await _unitOfWork.AsigUsuariosRecursosAtributosRepository.GetByID(usuarioRecursoAtributo.Id);
            if (currentRecursoAtributo == null)
            {
                throw new AguilaException("Registro no Existe!....");
            }

            var usuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioRecursoAtributo.UsuarioId);
            if (usuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            var estacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(usuarioRecursoAtributo.EstacionTrabajoId);
            if (estacion == null)
            {
                throw new AguilaException("Estacion No Existente!....");
            }

            var modulo = await _unitOfWork.ModulosRepository.GetByID(usuarioRecursoAtributo.ModuloId);
            if (modulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            var recurso = await _unitOfWork.RecursosAtributosRepository.GetByID(usuarioRecursoAtributo.RecursoAtributosId);
            if (recurso == null)
            {
                throw new AguilaException("Recurso-Atributo No Existente!....");
            }

            //valida que no sea una asignacion duplicada
            var existeAsignacion =  _unitOfWork.AsigUsuariosRecursosAtributosRepository.GetAll();
            existeAsignacion = existeAsignacion.Where(x => x.UsuarioId == usuarioRecursoAtributo.UsuarioId &&
                                                x.EstacionTrabajoId == usuarioRecursoAtributo.EstacionTrabajoId &&
                                                x.ModuloId == usuarioRecursoAtributo.ModuloId &&
                                                x.RecursoAtributosId == usuarioRecursoAtributo.RecursoAtributosId);

            if (existeAsignacion.Count() > 0)
            {
                throw new AguilaException("Asignacion de Recursos Ya Existente!....");
            }

            currentRecursoAtributo.UsuarioId = usuarioRecursoAtributo.UsuarioId;
            currentRecursoAtributo.EstacionTrabajoId = usuarioRecursoAtributo.EstacionTrabajoId;
            currentRecursoAtributo.ModuloId = usuarioRecursoAtributo.ModuloId;
            currentRecursoAtributo.RecursoAtributosId = usuarioRecursoAtributo.RecursoAtributosId;

            _unitOfWork.AsigUsuariosRecursosAtributosRepository.Update(currentRecursoAtributo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> deleteAsigUsuarioRecursoAtributo(long id)
        {
            var recursoAtributoEliminar = await _unitOfWork.AsigUsuariosRecursosAtributosRepository.GetByID(id);
            if (recursoAtributoEliminar == null)
            {
                throw new AguilaException("Registro no Existe!....");
            }

            await _unitOfWork.AsigUsuariosRecursosAtributosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }
    }
}
