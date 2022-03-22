using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ImecanicosService
    {
        PagedList<mecanicos> GetMecanicos(mecanicosQueryFilter filter);
        Task<mecanicos> GetMecanico(int id);
        Task InsertMecanico(mecanicos mecanico);
        Task<bool> UpdateMecanico(mecanicos mecanico);
        Task<bool> DeleteMecanico(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
