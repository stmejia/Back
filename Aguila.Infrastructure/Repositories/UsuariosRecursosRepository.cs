using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class UsuariosRecursosRepository : _BaseRepository<UsuariosRecursos>, IUsuariosRecursosRepository
    {
        public UsuariosRecursosRepository(AguilaDBContext context) : base(context) { }

        //public  IEnumerable<UsuariosRecursos> GetUsuarioRecursos(long id) {
        //    return  _entities.Where(x => x.usuario_id == id);
        //}

        public IQueryable<UsuariosRecursos> GetUsuarioRecursos(long id)
        {
            return _entities.Where(x => x.usuario_id == id);
        }

        public async Task<UsuariosRecursos> GetUsuarioRecursosIncludes(long id)
        {
            return await _entities.Where(e => e.usuario_id == id)
                .Include(e => e.Estacion)
                .Include(e => e.Recurso)
                .FirstOrDefaultAsync();
        }

        public IQueryable<UsuariosRecursos> GetAllIncludes(long id)
        {
            return _entities.Where(e => e.usuario_id == id)
                .Include(e => e.Estacion)
                .Include(e => e.Recurso)
                .AsQueryable();


        }
    }
}
