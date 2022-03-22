using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IactivoGeneralesService
    {
        Task<bool> DeleteActivoGeneral(int id);
        Task<activoGenerales> GetActivoGeneral(int id);
        PagedList<activoGenerales> GetActivosGenerales(activoGeneralesQueryFilter filter);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task InsertActivoGeneral(activoGenerales activo);
        Task<bool> UpdateActivoGeneral(activoGenerales activo);
    }
}