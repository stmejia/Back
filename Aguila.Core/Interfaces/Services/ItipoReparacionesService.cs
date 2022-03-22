using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoReparacionesService
    {
        Task<bool> DeleteTipoReparacion(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<tipoReparaciones> GetTipoReparacion(int id);
        PagedList<tipoReparaciones> GetTiposReparaciones(tipoReparacionesQueryFilter filter);
        Task InsertTipoReparacion(tipoReparaciones tipo);
        Task<bool> UpdateTipoReparacion(tipoReparaciones tipo);
    }
}