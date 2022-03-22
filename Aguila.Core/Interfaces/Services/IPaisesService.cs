using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface IpaisesService
    {
        PagedList<paises> GetPaises(paisesQueryFilter filter);
        Task<paises> GetPais(int id);
        Task InsertPais(paises pais);
        Task<bool> UpdatePais(paises pais);
        Task<bool> DeletePais(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
