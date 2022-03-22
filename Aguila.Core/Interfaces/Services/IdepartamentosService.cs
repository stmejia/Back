using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IdepartamentosService
    {
        PagedList<departamentos> GetDepartamento(departamentosQueryFilter filter);
        Task<departamentos> GetDepartamento(int id);
        Task InsertDepartamento(departamentos dptos);
        Task<bool> UpdateDepartamento(departamentos dptos);
        Task<bool> DeleteDepartamento(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
