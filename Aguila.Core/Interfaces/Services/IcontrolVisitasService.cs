using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcontrolVisitasService
    {
        Task<controlVisitas> darSalida(string identificacion);
        Task<bool> DeleteVisita(long id);
        Task<controlVisitas> GetVisita(long id);
        Task<PagedList<controlVisitas>> GetVisitas(controlVisitasQueryFilter filter);
        Task InsertVisita(controlVisitas visita);
        Task<bool> UpdateVisita(controlVisitas visita);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<controlVisitas> visitaPorId(string identificacion);
        Task<controlVisitas> visitaPorIdGeneric(string identificacion);
        IEnumerable<controlVisitas> enPredio(DateTime fecha, int estacionTrabajo);
    }
}