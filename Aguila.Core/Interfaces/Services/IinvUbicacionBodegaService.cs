using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IinvUbicacionBodegaService
    {
        PagedList<invUbicacionBodega> GetInvUbicacionBodega(invUbicacionBodegaQueryFilter filter);
        Task<invUbicacionBodega> GetInvUbicacionBodega(int id);
        Task InsertInvUbicacionBodega(invUbicacionBodega invUbicacionBodega);
        Task<bool> UpdateInvUbicacionBodega(invUbicacionBodega invUbicacionBodega);
        Task<bool> DeleteInvUbicacionBodega(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
