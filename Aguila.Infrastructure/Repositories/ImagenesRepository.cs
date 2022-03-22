using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;

namespace Aguila.Infrastructure.Repositories
{
    public class ImagenesRepository : _BaseRepository<Imagen>, IImagenesRepository

    {
        public ImagenesRepository(AguilaDBContext context) : base(context) { }
    }
}

