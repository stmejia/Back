using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcorporacionesService
    {
        PagedList<corporaciones> GetCorporaciones(corporacionesQueryFilter filter);
        Task<corporaciones> GetCorporacion(int id);
        Task InsertCorporacion(corporaciones corporacion);
        Task<bool> UpdateCorporacion(corporaciones corporacion);
        Task<bool> DeleteCorporacion(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
