using Aguila.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Aguila.Core.Interfaces.Repositories
{
    public interface IUsuariosRepository: IRepository<Usuarios>
    {
        Task<Usuarios> GetUsuarioByUserName(string username);
        Task<Usuarios> GetByIdIncludes(long Id);
        IQueryable<Usuarios> GetAllIncludes();

    }
}