using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IpilotosService
    {
        PagedList<pilotos> GetPilotos(pilotosQueryFilter filter);
        Task<pilotos> GetPiloto(int id);
        Task InsertPiloto(pilotos piloto);
        Task<bool> UpdatePiloto(pilotos piloto);
        Task<bool> DeletePiloto(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
