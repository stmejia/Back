using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcontrolEquipoAjenoService
    {
        Task<bool> DeleteVisita(long id);
        Task<controlEquipoAjeno> GetAjeno(long id);
        PagedList<controlEquipoAjeno> GetEquiposEjanos(controlEquipoAjenoQueryFilter filter);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task InsertAjeno(controlEquipoAjeno ajeno);
        Task<bool> UpdateAjeno(controlEquipoAjeno ajeno);
        Task<bool> ingresarPropios(controlGaritaDto control, long usuarioId);
    }
}