using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;

namespace Aguila.Core.Interfaces.Services
{
    public interface IentidadesComercialesDireccionesService
    {
        PagedList<entidadesComercialesDirecciones> GetEntidadComercialDireccion(entidadesComercialesDireccionesQueryFilter filter);
        Task<entidadesComercialesDirecciones> GetEntidadComercialDireccion(long id);
        Task InsertEntidadComercialDireccion(entidadesComercialesDirecciones entidadComercialDireccion);
        Task<bool> UpdateEntidadComercialDireccion(entidadesComercialesDirecciones entidadComercialDireccion);
        Task<bool> DeleteEntidadComercialDireccion(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
