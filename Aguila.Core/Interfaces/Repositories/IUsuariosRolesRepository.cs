using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IUsuariosRolesRepository : IRepository<UsuariosRoles>
    {
        Task<UsuariosRoles> getUsuarioRol(long usuarioId, int rolId);
        Task<bool> deleteUsuarioRol(long usuarioID, int rolId);

    }
}
