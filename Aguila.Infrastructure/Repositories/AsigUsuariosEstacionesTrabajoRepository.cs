using Aguila.Core.Entities;
using Aguila.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Aguila.Core.Interfaces.Repositories;

namespace Aguila.Infrastructure.Repositories
{
    public class AsigUsuariosEstacionesTrabajoRepository : _BaseRepository<AsigUsuariosEstacionesTrabajo>, IAsigUsuariosEstacionesTrabajoRepository
    {
        
        public AsigUsuariosEstacionesTrabajoRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<AsigUsuariosEstacionesTrabajo> GetEstacionesUsuario(long id)
        {
            return _entities.Where(e => e.UsuarioId == id)
                .Include(e => e.EstacionTrabajo)
                .AsQueryable();
        }


    }
}
