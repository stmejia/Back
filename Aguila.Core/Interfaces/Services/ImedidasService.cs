using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface ImedidasService
    {
        PagedList<medidas> GetMedidas(medidasQueryFilter filter);
        Task<medidas> GetMedida(int id);
        Task InsertMedida(medidas medida);
        Task<bool> UpdateMedida(medidas medida);
        Task<bool> DeleteMedida(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
