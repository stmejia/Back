using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionCisternaService
    {
        Task<PagedList<condicionCisterna>> GetCondicionCisterna(condicionCisternaQueryFilter filter);
        Task<condicionCisterna> GetCondicionCisterna(long idCondicion);
        Task<condicionCisternaDto> InsertCondicionCisterna(condicionCisternaDto condicionCisternaDto);
        Task<bool> UpdateCondicionCisterna(condicionCisterna condicionCisterna);
        Task<bool> DeleteCondicionCisterna(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionCisterna ultima(int idActivo);
        void llenarCondicionLlantas(condicionCisterna condicionCisterna, ref condicionCisternaDto condicionCisternaDto);
    }
}
