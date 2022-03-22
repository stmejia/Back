using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IdetalleCondicionService
    {
        PagedList<detalleCondicion> GetCondicionDetalle(detalleCondicionQueryFilter filter);
        Task<detalleCondicion> GetCondicionDetalle(long id);
        Task InsertCondicionDetalle(detalleCondicion detalleCondicion);
        Task<bool> UpdateCondicionDetalle(detalleCondicion detalleCondicion);
        Task<bool> DeleteCondicionDetalle(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}
