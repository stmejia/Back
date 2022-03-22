using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IproductosService
    {
        PagedList<productos> GetProductos(productosQueryFilter filter);
        Task<productos> GetProducto(int id);
        Task InsertProducto(productos producto);
        Task<bool> UpdateProducto(productos producto);
        Task<bool> DeleteProducto(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
