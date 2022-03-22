using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoActivosService
    {
        Task<bool> DeleteTipoActivo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<tipoActivos> GetTipoActivo(int id);
        PagedList<tipoActivos> GetTipoActivos(tipoActivosQueryFilter filter);
        Task InsertTipoActivo(tipoActivos tipo);
        Task<bool> UpdateTipoActivo(tipoActivos tipo);
    }
}