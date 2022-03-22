using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IempleadosIngresosService
    {
        Task<bool> DeleteVisita(long id);
        Task<empleadosIngresos> GetIngreso(long id);
        PagedList<empleadosIngresos> getIngresos(empleadosIngresosQueryFilter filter);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task InsertIngreso(empleadosIngresos ingreso, Boolean validar);
        Task<bool> UpdateIngreso(empleadosIngresos ingreso);
        //IEnumerable<empleados> getAusencias(reporteAusenciasQueryFilter filter);
    }
}