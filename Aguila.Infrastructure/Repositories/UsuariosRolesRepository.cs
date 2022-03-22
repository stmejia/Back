using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class UsuariosRolesRepository: _BaseRepository<UsuariosRoles>, IUsuariosRolesRepository
    {
        public UsuariosRolesRepository(AguilaDBContext context) : base(context) { }

        //devulve una asignacion de Usuario Rol del id del usuario y el id del rol a consultar
        public async Task<UsuariosRoles> getUsuarioRol(long usuarioId, int rolId)
        {
            return await _entities.FindAsync(usuarioId, rolId); ;
        }

        //Elimina una asignacion de Usuario Rol por medio del id de usuario y id de Rol
        public async Task<bool> deleteUsuarioRol(long usuarioID, int rolId)
        {
            var currentUsuarioRol = await _entities.FindAsync(usuarioID, rolId);
            _entities.Remove(currentUsuarioRol);

            return true;
        }
    }
}
