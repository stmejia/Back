using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IubicacionesService
    {
        PagedList<ubicaciones> GetUbicaciones(ubicacionesQueryFilter filter);
        Task<ubicaciones> GetUbicacion(int id);
        Task InsertUbicacion(ubicaciones ubicacion);
        Task<bool> UpdateUbicacion(ubicaciones ubicacion);
        Task<bool> DeleteUbicacion(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
