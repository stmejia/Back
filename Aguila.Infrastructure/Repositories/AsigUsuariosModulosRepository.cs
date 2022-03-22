using Aguila.Core.Entities;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Aguila.Core.Interfaces.Repositories;

namespace Aguila.Infrastructure.Repositories
{
    public class AsigUsuariosModulosRepository : _BaseRepository<AsigUsuariosModulos>, IAsigUsuariosModulosRepository
    {
       
        public AsigUsuariosModulosRepository(AguilaDBContext context) : base(context) { }
        
        //Metodos Personalizados del repositorio AsigUsuariosModulos

        //Elimina una asignacion de modulo a un usuario por medio del id de usuario y id de modulo
        public async Task<bool> DeleteAsigUsuarioModulo(long usuarioID, byte moduloID)
        {
            var currentUsuarioModulo = await _entities.FindAsync(usuarioID, moduloID);
            _entities.Remove(currentUsuarioModulo);
            
            return true;
        }

        //devulve una asignacion de modulo a un usuario por medio del id del usuario y el id del modulo a consultar
        public async Task<AsigUsuariosModulos> getAsigModuloUsuario(long usuarioId, byte moduloId)
        {            
            return await _entities.FindAsync(usuarioId, moduloId); ;
        }

        public  IQueryable<AsigUsuariosModulos> getAsigModulosUsuarioIncludes(long usuarioId)
        {
            return _entities.Where(e => e.UsuarioId == usuarioId)
                .Include(e => e.Modulo)
                .AsQueryable();
        }
    }
}
