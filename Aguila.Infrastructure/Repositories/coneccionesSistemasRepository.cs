using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Infrastructure.Data;

namespace Aguila.Infrastructure.Repositories
{    
    public class coneccionesSistemasRepository : _BaseRepository<coneccionesSistemas>, IconeccionesSistemasRepository
    {
        public coneccionesSistemasRepository(AguilaDBContext context) : base(context) { }
    }
}
