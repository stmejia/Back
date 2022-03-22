using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public RolesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Roles> GetRoles(RolesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var roles = _unitOfWork.RolesRepository.GetAll();

            if (filter.nombre != null)
            {
                roles = roles.Where(x => x.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            var pagedRoles = PagedList<Roles>.create(roles, filter.PageNumber, filter.PageSize);

            return pagedRoles;
        }

        public async Task<Roles> GetRol(int id)
        {
            return await _unitOfWork.RolesRepository.GetByID(id);
        }

        public async Task InsertRol(Roles rol)
        {
            rol.id = 0;
            await _unitOfWork.RolesRepository.Add(rol);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateRol(Roles rol)
        {
            var currentRol = await _unitOfWork.RolesRepository.GetByID(rol.id);
            if (currentRol == null)
            {
                throw new AguilaException("Rol No Existente!....");
            }

            currentRol.nombre = rol.nombre;

            _unitOfWork.RolesRepository.Update(currentRol);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteRol(int id)
        {
            var currentRol = await _unitOfWork.RolesRepository.GetByID(id);
            if (currentRol == null)
            {
                throw new AguilaException("Rol No Existente!....");
            }

            await _unitOfWork.RolesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
