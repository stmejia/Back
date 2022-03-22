using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;

namespace Aguila.Infrastructure.Repositories
{
    public class productosRepository : _BaseRepository<productos>, IproductosRepository
    {
        public productosRepository(AguilaDBContext context) : base(context) { }
    }
}
