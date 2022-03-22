using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IclienteServicioService
    {
        PagedList<clienteServicios> GetClienteServicios(clienteServiciosQueryFilter filter);
        Task<clienteServicios> GetClienteServicio(int id);
        Task InsertClienteServicio(clienteServicios clienteServicio);
        Task<bool> UpdateClienteServicio(clienteServicios clienteServicio);
        Task<bool> DeleteClienteServicio(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
