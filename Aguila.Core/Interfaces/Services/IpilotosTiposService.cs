using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IpilotosTiposService
    {
        PagedList<pilotosTipos> GetPilotosTipos(pilotosTiposQueryFilter filter);
        Task<pilotosTipos> GetPilotoTipo(int id);
        Task InsertPilotoTipo(pilotosTipos pilotoTipo);
        Task<bool> UpdatePilotoTipo(pilotosTipos pilotoTipo);
        Task<bool> DeletePilotoTipo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
