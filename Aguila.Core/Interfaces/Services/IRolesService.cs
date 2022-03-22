using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IRolesService
    {
        Task<bool> DeleteRol(int id);
        Task<Roles> GetRol(int id);
        PagedList<Roles> GetRoles(RolesQueryFilter filter);
        Task InsertRol(Roles rol);
        Task<bool> UpdateRol(Roles rol);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}