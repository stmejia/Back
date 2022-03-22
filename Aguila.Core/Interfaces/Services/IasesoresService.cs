using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IasesoresService
    {
        PagedList<asesores> GetAsesores(asesoresQueryFilter filter);
        Task<asesores> GetAsesor(int id);
        Task InsertAsesor(asesores asesor);
        Task<bool> UpdateAsesor(asesores asesor);
        Task<bool> DeleteAsesor(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
