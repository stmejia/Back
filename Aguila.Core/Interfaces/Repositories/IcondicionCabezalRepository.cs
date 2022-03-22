using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionCabezalRepository : IRepository<condicionCabezal>
    {
        condicionCabezal GetUltima(int idActivo);
        IQueryable<condicionCabezal> GetAllIncludes();
    }
}
