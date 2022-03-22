using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IclientesService
    {
        PagedList<clientes> GetClientes(clientesQueryFilter filter);
        Task<clientes> GetCliente(int id);
        Task InsertCliente(clientes cliente);
        Task<bool> UpdateCliente(clientes cliente);
        Task<bool> DeleteCliente(int id);
        Task<bool> inactivar(int id);
        bool existeCodigo(string codigo, byte empresa);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
