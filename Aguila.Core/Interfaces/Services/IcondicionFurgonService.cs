using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using System.Collections.Generic;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionFurgonService
    {
        Task<PagedList<condicionFurgon>> GetCondicionFurgon(condicionFurgonQueryFilter filter);
        Task<condicionFurgon> GetCondicionFurgon(long idCondicion);
        Task<condicionFurgonDto> InsertCondicionFurgon(condicionFurgonDto condicionFurgonDto);
        Task<bool> UpdateCondicionFurgon(condicionFurgon condicionFurgon);
        Task<bool> DeleteCondicionFurgon(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionFurgon ultima(int idActivo);
        void llenarCondicionLlantas(condicionFurgon condicionFurgon, ref condicionFurgonDto condicionFurgonDto);
        IEnumerable<condicionActivos> reporteCondicionesFurgones(reporteCondicionesFurgonesQueryFilter filter, int usuario);
    }
}
