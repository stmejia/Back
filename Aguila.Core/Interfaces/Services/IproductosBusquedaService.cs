using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IproductosBusquedaService
    {
        PagedList<productosBusqueda> GetProductosBusqueda(productosBusquedaQueryFilter filter);
        Task<productosBusqueda> GetProductoBusqueda(int id);
        Task InsertProductoBusqueda(productosBusqueda productoBusqueda);
        Task<bool> UpdateProductoBusqueda(productosBusqueda productoBusqueda);
        Task<bool> DeleteProductoBusqueda(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
