using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionContenedorRepository : IRepository<condicionContenedor>
    {
        IQueryable<condicionContenedor> GetAllIncludes();
        Task<condicionContenedor> GetByIdIncludes(int id);
        condicionContenedor GetUltima(int idActivo);
    }
}
