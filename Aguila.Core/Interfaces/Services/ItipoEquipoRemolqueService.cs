using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface ItipoEquipoRemolqueService
    {
        PagedList<tipoEquipoRemolque> GetTipoEquipoRemolque(tipoEquipoRemolqueQueryFilter filter);
        Task<tipoEquipoRemolque> GetTipoEquipoRemolque(int id);
        Task InsertTipoEquipoRemolque(tipoEquipoRemolque tipoEquipoRemolque);
        Task<bool> UpdateTipoEquipoRemolque(tipoEquipoRemolque tipoEquipoRemolque);
        Task<bool> DeleteTipoEquipoRemolque(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
