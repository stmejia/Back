using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcontrolContratistasRepository : IRepository<controlContratistas>
    {
        IQueryable<controlContratistas> GetAllIncludes();
        Task<controlContratistas> GetByIdentificacion(string identificacion);
        Task<controlContratistas> GetByIdIncludes(long id);

        Task<controlContratistas> GetByIdentificacionGeneric(string identificacion);
    }
}
