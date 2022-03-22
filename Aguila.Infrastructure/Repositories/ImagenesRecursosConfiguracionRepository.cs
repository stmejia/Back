using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;

namespace Aguila.Infrastructure.Repositories
{
    public class ImagenesRecursosConfiguracionRepository : _BaseRepository<ImagenRecursoConfiguracion>, IImagenesRecursosConfiguracionRepository
  
    {
        public ImagenesRecursosConfiguracionRepository(AguilaDBContext context) : base(context) { }
    }
}
