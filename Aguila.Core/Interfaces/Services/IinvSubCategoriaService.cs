using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IinvSubCategoriaService
    {
        PagedList<invSubCategoria> GetInvSubCategoria(invSubCategoriaQueryFilter filter);
        Task<invSubCategoria> GetInvSubCategoria(int id);
        Task InsertInvSubCategoria(invSubCategoria invSubCategoria);
        Task<bool> UpdateInvSubCategoria(invSubCategoria invSubCategoria);
        Task<bool> DeleteInvSubCategoria(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
