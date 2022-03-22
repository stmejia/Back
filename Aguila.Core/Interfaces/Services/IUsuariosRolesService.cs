using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IUsuariosRolesService
    {
        Task<bool> DeleteUsuarioRol(long usuarioID, int rolId);
        PagedList<UsuariosRoles> GetUsuariosRoles(UsuariosRolesQueryFilter filter);
        Task InsertUsuarioRol(UsuariosRoles usuarioRol);
        Task<bool> DeleteAll(long userId);
    }
}