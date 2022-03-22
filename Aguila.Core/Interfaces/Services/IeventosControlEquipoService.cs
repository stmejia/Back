using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IeventosControlEquipoService
    {
        Task<bool> AnularEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario);
        Task<bool> DeleteEventoControl(long id);
        Task<eventosControlEquipo> GetEventoControl(long id);
        PagedList<eventosControlEquipo> GetEventosControl(eventosControlEquipoQueryFilter filter);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<eventosControlEquipoDto> InsertEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario);
        Task<bool> ResolverEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario);
        Task<bool> RevisarEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario);
        Task<bool> UpdateEventoControl(eventosControlEquipoDto eventoControlDto);
    }
}