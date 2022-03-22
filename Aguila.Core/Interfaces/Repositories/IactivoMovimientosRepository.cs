using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IactivoMovimientosRepository : IRepository<activoMovimientos>
    {
        IQueryable<activoMovimientos> GetAllIncludes();
        IQueryable<activoMovimientos> ReporteMovimientosByUser(int idEmpresa, int idUsuario, DateTime fechaIni, DateTime fechaFin);
    }
}
