using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IllantaActualService
    {
        PagedList<llantaActual> GetLlantaActual(llantaActualQueryFilter filter);
        Task<llantaActual> GetLlantaActual(int id);
        Task InsertLlantaActual(llantaActual llantaActual);
        Task<bool> UpdateLlantaActual(llantaActual llantaActual);
        Task<bool> DeleteLlantaActual(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
