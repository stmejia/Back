using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class EmpresaRepository : _BaseRepository<Empresas>, IEmpresaRepository
    {       
        public EmpresaRepository(AguilaDBContext context) : base(context) { }

        public async Task<Empresas> GetByIdWithImagenLogo(long Id)
        {
            return await _entities.Where(e => e.Id == Id)
                .Include(e => e.ImagenLogo)
                    .ThenInclude(i => i.Imagenes.Where(im => im.FchBorrada == null))
                .FirstOrDefaultAsync();
        }

        public IQueryable<Empresas> GetAllIncludes()
        {
            return _entities
                .Include(e => e.ImagenLogo)
                .AsQueryable();
        }

    }
}
