using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IvehiculosService
    {
        Task<PagedList<vehiculos>> GetVehiculos(vehiculosQueryFilter filter);
        Task<vehiculos> GetVehiculo(int id);
        Task<vehiculosDto> InsertVehiculo(vehiculosDto vehiculoDto, int usuario);
        Task<bool> UpdateVehiculo(vehiculosDto vehiculo);
        Task<bool> DeleteVehiculo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
