using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IcondicionActivosRepository: IRepository<condicionActivos>
    {
        IQueryable<condicionActivos> GetAllIncludes();
        IQueryable<condicionActivos> reporteCondicionesVehiculos(int idEmpresa, int idUsuario);
        IQueryable<condicionActivos> reporteCondicionesEquipos(int idEmpresa, int idUsuario);
        IQueryable<condicionActivos> reporteCondicionesFurgones(int idEmpresa, int idUsuario);
        IQueryable<condicionActivos> reporteCondicionesGeneradores(int idEmpresa, int idUsuario);
    }
}
