using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using System.Collections.Generic;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionCabezalService
    {
       Task<PagedList<condicionCabezal>> GetCondicionCabezal(condicionCabezalQueryFilter filter);
        Task<condicionCabezal> GetCondicionCabezal(long idCondicion);
        Task<condicionCabezalDto> InsertCondicionCabezal(condicionCabezalDto condicionCabezalDto);
        Task<bool> UpdateCondicionCabezal(condicionCabezal condicionCabezal);
        Task<bool> DeleteCondicionCabezal(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionCabezal ultima(int idActivo);
        void llenarCondicionLlantas(condicionCabezal condicionCabezal, ref condicionCabezalDto condicionCabezalDto);
        IEnumerable<condicionActivos> reporteCondicionesVehiculos(reporteCondicionesCabezalesQueryFilter filter, int usuario);
    }
}
