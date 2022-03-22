using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IserviciosService
    {
        PagedList<servicios> GetServicios(serviciosQueryFilter filter);
        Task<servicios> GetServicio(int id);
        Task InsertServicio(servicios servicio);
        Task<bool> UpdateServicio(servicios servicio);
        Task<bool> DeleteServicio(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
