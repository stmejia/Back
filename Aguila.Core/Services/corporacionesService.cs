using Aguila.Core.Interfaces.Services;
using Aguila.Core.Entities;
using Aguila.Core.CustomEntities;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Services
{
    public class corporacionesService : IcorporacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public corporacionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<corporaciones> GetCorporaciones(corporacionesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var corporaciones = _unitOfWork.corporacionesRepository.GetAll();

            if (filter.codigo != null)
            {
                corporaciones = corporaciones.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.nombre != null)
            {
                corporaciones = corporaciones.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.propio != null)
            {
                corporaciones = corporaciones.Where(e => e.propio == filter.propio);
            }

            var pagedCorporaciones = PagedList<corporaciones>.create(corporaciones, filter.PageNumber, filter.PageSize);

            return pagedCorporaciones;
        }

        public async Task<corporaciones> GetCorporacion(int id)
        {
            return await _unitOfWork.corporacionesRepository.GetByID(id);
        }

        public async Task InsertCorporacion(corporaciones corporacion)
        {
            //Insertamos la fecha de ingreso del registro
            corporacion.id = 0;
            corporacion.fechaCreacion = DateTime.Now;

            await _unitOfWork.corporacionesRepository.Add(corporacion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateCorporacion(corporaciones corporacion)
        {
            var currentCorporacion = await _unitOfWork.corporacionesRepository.GetByID(corporacion.id);
            if (currentCorporacion == null)
            {
                throw new AguilaException("Corporacion no existente...");
            }

            //currentCorporacion.codigo = corporacion.codigo;
            currentCorporacion.nombre = corporacion.nombre;
            currentCorporacion.propio = corporacion.propio;

            _unitOfWork.corporacionesRepository.Update(currentCorporacion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteCorporacion(int id)
        {
            var currentCorporacion = await _unitOfWork.corporacionesRepository.GetByID(id);
            if (currentCorporacion == null)
            {
                throw new AguilaException("Corporacion no existente...");
            }

            await _unitOfWork.corporacionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
