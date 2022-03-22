using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IllantaTiposService
    {
        PagedList<llantaTipos> GetLlantaTipos(llantaTiposQueryFilter filter);
        Task<llantaTipos> GetLlantaTipo(int id);
        Task InsertLlantaTipo(llantaTipos llantaTipo);
        Task<bool> UpdateLlantaTipo(llantaTipos llantaTipo);
        Task<bool> DeleteLlantaTipo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
