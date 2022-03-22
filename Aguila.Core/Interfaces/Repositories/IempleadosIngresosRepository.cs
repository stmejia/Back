using Aguila.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IempleadosIngresosRepository : IRepository<empleadosIngresos>
    {
        IQueryable<empleadosIngresos> GetAllIncludes();
        Task<empleadosIngresos> GetByIdEmpleadoIncludes(int id);
        Task<empleadosIngresos> GetByIdIncludes(long id);
    }
}