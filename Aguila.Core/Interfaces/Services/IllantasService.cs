using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IllantasService
    {
        PagedList<llantas> GetLlantas(llantasQueryFilter filter);
        Task<llantas> GetLlanta(int id);
        Task InsertLlanta(llantas llanta);
        Task<bool> UpdateLlanta(llantas llanta);
        Task<bool> DeleteLlanta(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
