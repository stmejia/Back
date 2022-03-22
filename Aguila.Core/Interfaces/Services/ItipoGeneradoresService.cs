using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoGeneradoresService
    {
        PagedList<tipoGeneradores> GetTipoGeneradores(tipoGeneradoresQueryFilter filter);
        Task<tipoGeneradores> GetTipoGenerador(int id);
        Task InsertTipoGenerador(tipoGeneradores tipoGenerador);
        Task<bool> UpdateTipoGenerador(tipoGeneradores tipoGenerador);
        Task<bool> DeleteTipoGenerador(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
