using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoVehiculosService
    {
        PagedList<tipoVehiculos> GetTipoVehiculos(tipoVehiculosQueryFilter filter);
        Task<tipoVehiculos> GetTipoVehiculo(int id);
        Task InsertTipoVehiculo(tipoVehiculos tipoVehiculo);
        Task<bool> UpdateTipoVehiculo(tipoVehiculos tipoVehiculo);
        Task<bool> DeleteTipoVehiculo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
