using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IproveedoresService
    {
        PagedList<proveedores> GetProveedores(proveedoresQueryFilter filter);
        Task<proveedores> GetProveedor(long id);
        Task InsertProveedor(proveedores proveedor);
        Task<bool> UpdateProveedor(proveedores proveedor);
        Task<bool> DeleteProveedor(long id);
        Task<bool> inactivar(int id);
        bool existeCodigo(string codigo, byte empresa);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
