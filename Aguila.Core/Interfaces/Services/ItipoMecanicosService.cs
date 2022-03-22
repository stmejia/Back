using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoMecanicosService
    {
        PagedList<tipoMecanicos> GetTipoMecanicos(tipoMecanicosQueryFilter filter);
        Task<tipoMecanicos> GetTipoMecanico(int id);
        Task InsertTipoMecanico(tipoMecanicos tipoMecanico);
        Task<bool> UpdateTipoMecanico(tipoMecanicos tipoMecanico);
        Task<bool> DeleteTipoMecanico(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
