using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IinvProductoBodegaService
    {
        PagedList<invProductoBodega> GetProductoBodegas(invProductoBodegaQueryFilter filter);
        Task<invProductoBodega> GetProductoBodega(int id);
        Task InsertProductoBodega(invProductoBodega invProductoBodega);
        Task<bool> UpdateProductoBodega(invProductoBodega invProductoBodega);
        Task<bool> DeleteProductoBodega(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
