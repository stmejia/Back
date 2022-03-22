using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IequipoRemolqueRepository : IRepository<equipoRemolque>
    {
        IQueryable<equipoRemolque> GetAllIncludes();
        Task<equipoRemolque> GetByIdIncludes(int id);
        IQueryable<equipoRemolque> GetAllEstadoActual();
        IQueryable<equipoRemolque> reporteInventario(int idEmpresa, int idUsuario);        
    }
}
