using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public class ImagenesRecursosRepository : _BaseRepository<ImagenRecurso>, IImagenesRecursosRepository

    {
        public ImagenesRecursosRepository(AguilaDBContext context) : base(context) { }

        public async Task<ImagenRecurso> GetByIdWithConfiguracion(Guid Id)
        {
            return await _entities.Where(e => e.Id == Id)
                    .Include(c => c.ImagenRecursoConfiguracion)
                    .Include(imgs => imgs.Imagenes)
                    .FirstOrDefaultAsync();
        }


    }
}
