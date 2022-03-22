using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ISucursalService
    {
        Task<bool> DeleteSucursal(short id);
        PagedList<Sucursales> GetSucursales(SucursalQueryFilter filter);
        Task<Sucursales> GetSucursal(short id);
        Task InsertSucursal(Sucursales sucursal);
        Task<bool> updateSucursal(Sucursales sucursal);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}