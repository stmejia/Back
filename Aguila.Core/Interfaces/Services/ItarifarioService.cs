using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItarifarioService
    {
        PagedList<tarifario> GetTarifario(tarifarioQueryFilter filter);
        Task<tarifario> GetTarifario(int id);
        Task InsertTarifario(tarifario tarifario);
        Task<bool> UpdateTarifario(tarifario tarifario);
        Task<bool> DeleteTarifario(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
