using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IactivoEstadosService
    {
        PagedList<activoEstados> GetActivoEstados(activoEstadosQueryFilter filter);
        Task<activoEstados> GetActivoEstado(int id);
        Task InsertActivoEstado(activoEstados activoEstado);
        Task<bool> UpdateActivoEstado(activoEstados activoEstado);
        Task<bool> DeleteActivoEstado(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
