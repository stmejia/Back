using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IequipoRemolqueService
    {
        Task<PagedList<equipoRemolque>> GetEquipoRemolque(equipoRemolqueQueryFilter filter);
        Task<equipoRemolque> GetEquipoRemolque(int id);
        Task<equipoRemolqueDto> InsertEquipoRemolque(equipoRemolqueDto equipoRemolque, int usuario);
        Task<bool> UpdateEquipoRemolque(equipoRemolqueDto equipoRemolque);
        Task<bool> DeleteEquipoRemolque(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
