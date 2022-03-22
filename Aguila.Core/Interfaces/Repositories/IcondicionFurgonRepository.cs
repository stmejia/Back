using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionFurgonRepository : IRepository<condicionFurgon>
    {
        IQueryable<condicionFurgon> GetAllIncludes();
        condicionFurgon GetUltima(int idActivo);
    }
}
