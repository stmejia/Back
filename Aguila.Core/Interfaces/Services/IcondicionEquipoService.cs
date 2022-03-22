using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using System.Collections.Generic;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionEquipoService
    {
        Task<PagedList<condicionEquipo>> GetCondicionEquipo(condicionEquipoQueryFilter filter);
        Task<condicionEquipo> GetCondicionEquipo(long idCondicion);
        Task<condicionEquipoDto> InsertCondicionEquipo(condicionEquipoDto condicionEquipoDto);
        Task<bool> UpdateCondicionEquipo(condicionEquipo condicionEquipo);
        Task<bool> DeleteCondicionEquipo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionEquipo ultima(int idActivo);
        void llenarCondicionLlantas(condicionEquipo condicionEquipo, ref condicionEquipoDto condicionEquipoDto);

        IEnumerable<condicionActivos> reporteCondicionesEquipos(reporteCondicionesEquipoQueryFilter filter, int usuario);
    }
}
