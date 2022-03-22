using Aguila.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcontrolEquipoAjenoRepository : IRepository<controlEquipoAjeno>
    {
        IQueryable<controlEquipoAjeno> GetAllIncludes();
        Task<controlEquipoAjeno> GetByIdIncludes(long id);
    }
}