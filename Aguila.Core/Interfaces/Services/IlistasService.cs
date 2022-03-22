using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IlistasService
    {
        Task<bool> DeleteLista(int id);
        Task<listas> GetLista(int id);
        PagedList<listas> GetListas(listasQueryFilter filter);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task InserLista(listas lista);
        Task<bool> UpdateLista(listas lista);
    }
}