using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IdetalleCondicionRepository : IRepository<detalleCondicion>
    {
        IQueryable<detalleCondicion> GetAllIncludes();
        Task<detalleCondicion> GetByIdIncludes(long id);
    }
}
