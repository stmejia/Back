using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionCisternaRepository : IRepository<condicionCisterna>
    {
        IQueryable<condicionCisterna> GetAllIncludes();
        condicionCisterna GetUltima(int idActivo);
    }
}
