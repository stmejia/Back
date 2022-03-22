using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionActivosService
    {
        Task<bool> DeleteCondicion(int id);
        PagedList<condicionActivos> GetCondiciones(condicionActivosQueryFilter filter);
        Task<JObject> GetCodicion(string id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task InsertCondicion(condicionActivosDto condicion, string user);
        Task<bool> UpdateCondicion(condicionActivos condicion);
        Task<condicionActivos> insert(condicionActivos condicionActivo);
    }
}