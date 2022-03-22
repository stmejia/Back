using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IactivoMovimientosActualRepository : IRepository<activoMovimientosActual>
    {
        IQueryable<activoMovimientosActual> GetAllIncludes();
        activoMovimientosActual GetActivoOperacionByCodigo(string codigo, int empresaId);
        IQueryable<activoMovimientosActual> GetAllVehiculosEstadoActual();
    }
}
