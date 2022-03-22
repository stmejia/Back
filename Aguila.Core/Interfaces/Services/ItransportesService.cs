using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItransportesService
    {
        PagedList<transportes> GetTransportes(transportesQueryFilter filter);
        Task<transportes> GetTransporte(int id);
        Task InsertTransporte(transportes transporte);
        Task<bool> UpdateTransporte(transportes transporte);
        Task<bool> DeleteTransporte(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
