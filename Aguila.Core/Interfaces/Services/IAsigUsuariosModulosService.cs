using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IAsigUsuariosModulosService
    {
        Task<bool> DeleteAsigUsuarioModulo(long usuarioId, byte moduloId);
        PagedList<AsigUsuariosModulos> GetAsigUsuariosModulos(AsigUsuariosModulosQueryFilter filter);
        Task InsertAsigUsuarioModulo(AsigUsuariosModulos usuarioModulo);
        Task<bool> DeleteAll(long userId);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        IQueryable<AsigUsuariosModulos> GetAsigUsuarioModulos(long id);
    }
}