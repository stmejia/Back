using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IEstacionesTrabajoService
    {
        Task<bool> DeleteEstacionTrabajo(int id);
        PagedList<EstacionesTrabajo> GetEstacionesTrabajo(EstacionesTrabajoQueryFilter filter);
        Task<EstacionesTrabajo> GetEstacionTrabajo(int id);
        Task InsertEstacionTrabajo(EstacionesTrabajo estacion);
        Task<bool> updateEstacionTrabajo(EstacionesTrabajo estacion);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}