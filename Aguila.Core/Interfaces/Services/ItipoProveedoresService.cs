using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoProveedoresService
    {
        Task<bool> DeleteTipoProveedor(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<tipoProveedores> GetTipoProveedor(int id);
        PagedList<tipoProveedores> GetTiposProveedores(tipoProveedoresQueryFilter filter);
        Task InsertTipoProveedor(tipoProveedores tipo);
        Task<bool> UpdateTipoProveedor(tipoProveedores tipo);
    }
}