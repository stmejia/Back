using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;

namespace Aguila.Core.Interfaces.Services
{
    public interface IgeneradoresService
    {
        PagedList<generadores> GetGeneradores(generadoresQueryFilter filter);
        Task<generadores> GetGenerador(int id);
        Task<generadoresDto> InsertGenerador(generadoresDto generadorDto, int usuario);
        Task<bool> UpdateGenerador(generadoresDto generador);
        Task<bool> DeleteGenerador(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
