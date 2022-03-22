using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public interface IUsuariosRecursosRepository : IRepository<UsuariosRecursos>
    {

        IQueryable<UsuariosRecursos> GetUsuarioRecursos(long id);
        Task<UsuariosRecursos> GetUsuarioRecursosIncludes(long id);
        IQueryable<UsuariosRecursos> GetAllIncludes(long id);
    }
}
