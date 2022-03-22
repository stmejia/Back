using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IcondicionContenedorService
    {
        Task<PagedList<condicionContenedor>> GetCondicionContenedor(condicionContenedorQueryFilter filter);
        Task<condicionContenedor> GetCondicionContenedor(long idCondicion);
        Task<condicionContenedorDto> InsertCondicionContenedor(condicionContenedorDto condicionContenedorDto);
        Task<bool> UpdateCondicionContenedor(condicionContenedor condicionContenedor);
        Task<bool> DeleteCondicionContenedor(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        condicionContenedor ultima(int idActivo);
    }
}
