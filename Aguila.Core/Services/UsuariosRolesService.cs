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
    public class UsuariosRolesService : IUsuariosRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IUsuariosRolesRepository _usuariosRolesRepository;
        private readonly PaginationOptions _paginationOptions;

        public UsuariosRolesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<UsuariosRoles> GetUsuariosRoles(UsuariosRolesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var usuariosRoles = _unitOfWork.UsuariosRolesRepository.GetAll();

            if (filter.usuario_id != null)
            {
                usuariosRoles = usuariosRoles.Where(x => x.usuario_id == filter.usuario_id);
            }

            if (filter.rol_id != null)
            {
                usuariosRoles = usuariosRoles.Where(x => x.rol_id == filter.rol_id);
            }
            var pagedRoles = PagedList<UsuariosRoles>.create(usuariosRoles, filter.PageNumber, filter.PageSize);

            return pagedRoles;
        }

        //inserta una nueva asignacion de Usuario Rol
        public async Task InsertUsuarioRol(UsuariosRoles usuarioRol)
        {
            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioRol.usuario_id);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            //valida que el Rol exista
            var currentRol = await _unitOfWork.RolesRepository.GetByID(usuarioRol.rol_id);
            if (currentRol == null)
            {
                throw new AguilaException("Rol No Existente!....");
            }

            //valida que la asignacion de Rol no exista
            var currentAsignacion = await _unitOfWork.UsuariosRolesRepository.getUsuarioRol(usuarioRol.usuario_id, usuarioRol.rol_id);
            if (currentAsignacion != null)
            {
                throw new AguilaException("Rol ya asignado a Usuario!....");
            }

            await _unitOfWork.UsuariosRolesRepository.Add(usuarioRol);
            await _unitOfWork.SaveChangeAsync();
        }

        //elimina una asignacion de Usuario Rol
        public async Task<bool> DeleteUsuarioRol(long usuarioID, int rolId)
        {
            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioID);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            //valida que el Rol exista
            var currentRol = await _unitOfWork.RolesRepository.GetByID(rolId);
            if (currentRol == null)
            {
                throw new AguilaException("Rol No Existente!....");
            }

            //valida que la asignacion de Rol no exista
            var currentAsignacion = await _unitOfWork.UsuariosRolesRepository.getUsuarioRol(usuarioID, rolId);
            if (currentAsignacion == null)
            {
                throw new AguilaException("Asignacion de Rol No Existente!....");
            }

            await _unitOfWork.UsuariosRolesRepository.deleteUsuarioRol(usuarioID, rolId);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteAll(long userId)
        {
            UsuariosRolesQueryFilter filter = new UsuariosRolesQueryFilter { usuario_id = userId };
            var asignaciones = GetUsuariosRoles(filter);

            foreach(var asignacion in asignaciones)
            {
                await _unitOfWork.UsuariosRolesRepository.deleteUsuarioRol(asignacion.usuario_id, asignacion.rol_id);
            }
            await _unitOfWork.SaveChangeAsync();

            return true;
        }
    }
}
