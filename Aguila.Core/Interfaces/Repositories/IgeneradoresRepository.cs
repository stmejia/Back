using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IgeneradoresRepository : IRepository<generadores>
    {
        IQueryable<generadores> GetAllIncludes();
        IQueryable<generadores> GetAllEstadoActual();
        IQueryable<generadores> reporteInventario(int idEmpresa, int idUsuario);
    }
}
