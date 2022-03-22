using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class UsuariosRepository : _BaseRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(AguilaDBContext context) : base(context) { }

        public async Task<Usuarios> GetUsuarioByUserName(string username)
        {            
            return await _entities.FirstOrDefaultAsync(x => x.Username.Equals (username));            
        }

        public async Task<Usuarios> GetByIdIncludes(long Id)
        {
            return await _entities.Where(e => e.Id == Id)
                .Include(e => e.EstacionesTrabajoAsignadas)
                    .ThenInclude(e => e.EstacionTrabajo)
                        .ThenInclude(s => s.Sucursal)
                            .ThenInclude(e => e.Empresa)
                .FirstOrDefaultAsync();

             //.Include(e => e.ImagenPerfil)
             //       .ThenInclude(i => i.Imagenes.Where(im => im.FchBorrada == null))

        }

        //public IEnumerable<Usuarios> GetAllIncludes()
        //{
        //    return _entities
        //        .Include(e => e.ImagenPerfil)
        //        .AsEnumerable();
        //}

        public IQueryable<Usuarios> GetAllIncludes()
        {
            return _entities
                .Include(e => e.ImagenPerfil)
                .AsQueryable();
        }

    }
}
