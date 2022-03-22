using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IreparacionesService
    {
        Task<bool> DeleteReparacion(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<reparaciones> GetReparacion(int id);
        PagedList<reparaciones> GetReparaciones(reparacionesQueryFilter filter);
        Task InsertReparacion(reparaciones reparacion);
        Task<bool> UpdateReparacion(reparaciones reparacion);
    }
}