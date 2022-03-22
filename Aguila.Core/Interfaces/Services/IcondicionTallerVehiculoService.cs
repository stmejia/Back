using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using Aguila.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionTallerVehiculoService
    {
        Task<PagedList<condicionTallerVehiculo>> GetCondicionTallerVehiculo(condicionTallerVehiculoQueryFilter filter);
        Task<condicionTallerVehiculo> GetCondicionTallerVehiculo(int idCondicion);
        Task InsertCondicionTallerVehiculo(condicionTallerVehiculo condicionTallerVehiculo);
        Task<bool> UpdateCondicionTallerVehiculo(condicionTallerVehiculo condicionTallerVehiculo);
        Task<bool> DeleteCondicionTallerVehiculo(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
