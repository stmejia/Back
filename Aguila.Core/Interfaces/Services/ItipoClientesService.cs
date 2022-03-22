using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoClientesService
    {
        PagedList<tipoClientes> GetTipoClientes(tipoClientesQueryFilter filter);
        Task<tipoClientes> GetTipoCliente(int id);
        Task InsertTipoCliente(tipoClientes tipoCliente);
        Task<bool> UpdateTipoCliente(tipoClientes tipoCliente);
        Task<bool> DeleteTipoCliente(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
