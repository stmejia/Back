using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IempleadosRepository : IRepository<empleados>
    {
        IQueryable<empleados> GetAllIncludes();
        Task<empleados> GetByIdIncludes(int id);
        Task<empleados> GetEmpleadoByCuiIncludes(string cui);
    }
}
