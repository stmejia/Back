using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionEquipoRepository : IRepository<condicionEquipo>
    {
        IQueryable<condicionEquipo> GetAllIncludes();
        condicionEquipo GetUltima(int idActivo);
    }
}
