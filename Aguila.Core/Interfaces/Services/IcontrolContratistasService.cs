using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using System.Collections.Generic;
using System;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcontrolContratistasService
    {
        Task<PagedList<controlContratistas>> GetControlContratistas(controlContratistasQueryFilter filter);
        Task<controlContratistas> GetControlContratistas(long id);
        Task InsertControlContratistas(controlContratistas xControlContratistas);
        Task<bool> UpdateControlContratistas(controlContratistas controlContratistas);
        Task<bool> DeleteControlContratistas(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<controlContratistas> darSalida(string identificacion);
        Task<controlContratistas> contratistaPorId(string identificacion);
        Task<controlContratistas> contratistaPorIdGeneric(string identificacion);
        IEnumerable<controlContratistas> enPredio(DateTime fecha, int estacionTrabajo);
    }
}
