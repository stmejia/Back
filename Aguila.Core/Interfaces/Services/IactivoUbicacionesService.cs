using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IactivoUbicacionesService
    {
        PagedList<activoUbicaciones> GetActivoUbicaciones(activoUbicacionesQueryFilter filter);
        Task<activoUbicaciones> GetActivoUbicacion(int id);
        Task InsertActivoUbicacion(activoUbicaciones activoUbicacion);
        Task<bool> UpdateActivoUbicacion(activoUbicaciones activoUbicacion);
        Task<bool> DeleteActivoUbicacion(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
