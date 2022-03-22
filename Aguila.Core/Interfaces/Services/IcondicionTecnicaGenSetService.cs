using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionTecnicaGenSetService
    {
        Task<PagedList<condicionTecnicaGenSet>> GetCondicionTecnicaGenSet(condicionTecnicaGenSetQueryFilter filter);
        Task<condicionTecnicaGenSet> GetCondicionTecnicaGenSet(long idCondicion);
        Task<condicionTecnicaGenSetDto> InsertCondicionTecnicaGenSet(condicionTecnicaGenSetDto condicionTecnicaGenSetDto);
        Task<bool> UpdateCondicionTecnicaGenSet(condicionTecnicaGenSet condicionTecnicaGenSet);
        Task<bool> DeleteCondicionTecnicaGenSet(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionTecnicaGenSet ultima(int idActivo);
    }
}
