using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using System.Collections.Generic;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionGenSetService
    {
        Task<PagedList<condicionGenSet>> GetCondicionGenSet(condicionGenSetQueryFilter filter);
        Task<condicionGenSet> GetCondicionGenSet(long idCondicion);
        Task<condicionGenSetDto> InsertCondicionGenSet(condicionGenSetDto condicionGenSetDto);
        Task<bool> UpdateCondicionGenSet(condicionGenSet condicionGenSet);
        Task<bool> DeleteCondicionGenSet(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionGenSet ultima(int idActivo);
        IEnumerable<condicionActivos> reporteCondicionesGeneradores(reporteCondicionesGeneradoresQueryFilter filter, int usuario);
    }
}
