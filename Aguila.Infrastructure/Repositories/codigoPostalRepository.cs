using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class codigoPostalRepository: _BaseRepository<codigoPostal> , IcodigoPostalRepository
    {
        public codigoPostalRepository(AguilaDBContext context) : base(context) { }

        public IQueryable<codigoPostal> GetAllIncludes()
        {
            return _entities
                .Include(e => e.municipio)
                .AsQueryable();
        }

        public async Task<codigoPostal> GetByIdIncludes(int id)
        {
            return await _entities.Where(e => e.idMunicipio == id)
                .Include(e => e.municipio)
                .FirstOrDefaultAsync();
        }
    }
}
