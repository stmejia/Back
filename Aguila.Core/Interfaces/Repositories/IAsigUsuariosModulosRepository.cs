using Aguila.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IAsigUsuariosModulosRepository: IRepository<AsigUsuariosModulos>
    {
        Task<bool> DeleteAsigUsuarioModulo(long usuarioID, byte moduloID);
        Task<AsigUsuariosModulos> getAsigModuloUsuario(long usuarioId, byte moduloId);
        IQueryable<AsigUsuariosModulos> getAsigModulosUsuarioIncludes(long usuarioId);
    }
}