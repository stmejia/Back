using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aguila.Infrastructure.Repositories
{
    public class EstacionesTrabajoRepository : _BaseRepository<EstacionesTrabajo> , IEstacionesTrabajoRepository
    {
        public EstacionesTrabajoRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<EstacionesTrabajo> GetAllSucursalEmpresaIncludes()
        {
            return _entities.Include(e => e.Sucursal)
                    .ThenInclude(e => e.Empresa)
                    .AsQueryable();
        }

    }
}
