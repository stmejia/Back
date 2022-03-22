using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionTecnicaGenSetRepository : IRepository<condicionTecnicaGenSet>
    {
        IQueryable<condicionTecnicaGenSet> GetAllIncludes();
        Task<condicionTecnicaGenSet> GetByIdIncludes(long id);
        condicionTecnicaGenSet GetUltima(int idActivo);
    }
}
