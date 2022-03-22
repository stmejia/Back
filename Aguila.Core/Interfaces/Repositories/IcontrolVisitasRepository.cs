using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcontrolVisitasRepository : IRepository<controlVisitas>
    {
        IQueryable<controlVisitas> GetAllIncludes();
        Task<controlVisitas> GetByIdIncludes(long id);
        Task<controlVisitas> GetByIdentificacion(string identificacion);
        Task<controlVisitas> GetByIdentificacionGeneric(string identificacion);
    }
}
