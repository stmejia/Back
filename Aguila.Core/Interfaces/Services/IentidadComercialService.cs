using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface IentidadComercialService
    {
        PagedList<entidadComercial> GetEntidadComercial(entidadComercialQueryFilter filter);
        Task<entidadComercial> GetEntidadComercial(long id);
        Task InsertEntidadComercial(entidadComercial entidadComercial);
        Task<bool> UpdateEntidadComercial(entidadComercial entidadComercial);
        Task<bool> DeleteEntidadComercial(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        string getTipo(long idEntidad, byte idEmpresa);

    }
}
