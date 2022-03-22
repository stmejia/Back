using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IAsigUsuariosRecursosAtributosService
    {
        Task<bool> deleteAsigUsuarioRecursoAtributo(long id);        
        PagedList<AsigUsuariosRecursosAtributos> GetAsigRecursosAtributos(AsigUsuariosRecursosAtributosQueryFilter filter);
        Task<AsigUsuariosRecursosAtributos> GetAsigUsuarioRecursoAtributo(long id);
        Task insertAsigUsuarioRecursoAtributo(AsigUsuariosRecursosAtributos usuarioRecursoAtributo);
        Task<bool> updateUsuarioRecursoAtributo(AsigUsuariosRecursosAtributos usuarioRecursoAtributo);
    }
}