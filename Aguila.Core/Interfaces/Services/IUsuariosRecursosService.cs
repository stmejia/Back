using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IUsuariosRecursosService
    {
        Task<bool> DeleteUsuarioRecurso(long id);
        PagedList<UsuariosRecursos> GetUsuariosRecursos(UsuariosRecursosQueryFilter filter);
        Task InsertUsuarioRecurso(UsuariosRecursos usuarioRecurso);
        Task<bool> UpdateUsuarioRecurso(UsuariosRecursos usuarioRecurso);
        IEnumerable<UsuariosRecursos> GetUsuarioRecurso(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        IQueryable<UsuariosRecursos> GetUsuarioRecursoIncludes(long id);
    }
}