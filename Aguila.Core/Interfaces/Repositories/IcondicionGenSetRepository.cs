using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionGenSetRepository : IRepository<condicionGenSet>
    {
        IQueryable<condicionGenSet> GetAllIncludes();
        Task<condicionGenSet> GetByIdIncludes(int id);
        condicionGenSet GetUltima(int idActivo);
    }
}
