using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItiposListaService
    {
        Task<bool> DeleteTipoLista(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<tiposLista> GetTipoLista(int id);
        PagedList<tiposLista> GetTiposLista(tiposListaQueryFilter filter);
        Task InsertTipoLista(tiposLista tipo);
        Task<bool> UpdateTipoLista(tiposLista tipo);
        PagedList<listas> GetLista(tiposListaQueryFilter filter);
    }
}