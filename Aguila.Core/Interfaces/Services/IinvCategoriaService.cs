using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IinvCategoriaService
    {
        PagedList<invCategoria> GetInvCategoria(invCategoriaQueryFilter filter);
        Task<invCategoria> GetInvCategoria(int id);
        Task InsertInvCategoria(invCategoria invCategoria);
        Task<bool> UpdateInvCategoria(invCategoria invCategoria);
        Task<bool> DeleteInvCategoria(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
