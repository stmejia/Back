using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IAsigUsuariosEstacionesTrabajoService
    {
        Task<bool> DeleteAsigUsuarioEstacionTrabajo(long id);
        Task<AsigUsuariosEstacionesTrabajo> GetUsuarioEstacion(long id);
        PagedList<AsigUsuariosEstacionesTrabajo> GetUsuarioEstaciones(AsigUsuariosEstacionesTrabajoQueryFilter filter);
        Task InsertAsigUsuarioEstacion(AsigUsuariosEstacionesTrabajo asigUsuariosEstacionesTrabajo);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        IQueryable<AsigUsuariosEstacionesTrabajo> GetEstacionesUsuarisIncludes(long id);
    }
}