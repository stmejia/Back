using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IempleadosService
    {
        Task<PagedList<empleados>> GetEmpleados(empleadosQueryFilter filter);
        Task<empleados> GetEmpleado(int id);
        Task InsertEmpleado(empleados empleado);
        Task<bool> UpdateEmpleado(empleados empleado);
        Task<bool> DeleteEmpleado(int id);
        Task<bool> existeCodigo(empleadosDto empleadoDto);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<empleados> GetEmpleadoByCui(string cui);
        IEnumerable<empleados> getAusencias(reporteAusenciasQueryFilter filter);
    }
}
