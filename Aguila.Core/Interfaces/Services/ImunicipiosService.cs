using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ImunicipiosService
    {
        PagedList<municipios> GetMunicipio(municipiosQueryFilter filter);
        Task<municipios> GetMunicipio(int id);
        Task InsertMunicipio(municipios municipio);
        Task<bool> UpdateMunicipio(municipios municipio);
        Task<bool> DeleteMunicipio(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);

    }
}
