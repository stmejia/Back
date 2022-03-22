using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IvehiculosRepository : IRepository<vehiculos>
    {
        IQueryable<vehiculos> GetAllIncludes();
        IQueryable<vehiculos> GetAllEstadoActual();
        IQueryable<vehiculos> reporteInventario(int idEmpresa, int idUsuario);
        bool esTipo(string tipo, int idActivo);
    }
}
