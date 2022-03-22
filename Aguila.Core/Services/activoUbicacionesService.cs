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
    public class activoUbicacionesService : IactivoUbicacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public activoUbicacionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<activoUbicaciones> GetActivoUbicaciones(activoUbicacionesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var activoUbicaciones = _unitOfWork.activoUbicacionesRepository.GetAll();

            if (filter.idActivo != null)
            {
                activoUbicaciones = activoUbicaciones.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idUbicacion != null)
            {
                activoUbicaciones = activoUbicaciones.Where(e => e.idUbicacion == filter.idUbicacion);
            }

            if (filter.observaciones != null)
            {
                activoUbicaciones = activoUbicaciones.Where(e => e.observaciones.ToLower().Contains(filter.observaciones.ToLower()));
            }

            var pagedActivoUbicaciones = PagedList<activoUbicaciones>.create(activoUbicaciones, filter.PageNumber, filter.PageSize);
            return pagedActivoUbicaciones;
        }

        public async Task<activoUbicaciones> GetActivoUbicacion(int id)
        {
            return await _unitOfWork.activoUbicacionesRepository.GetByID(id);
        }

        public async Task InsertActivoUbicacion(activoUbicaciones activoUbicacion)
        {
            //Insertamos la fecha de ingreso del registro
            activoUbicacion.id = 0;
            activoUbicacion.fechaCreacion = DateTime.Now;

            await _unitOfWork.activoUbicacionesRepository.Add(activoUbicacion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateActivoUbicacion(activoUbicaciones activoUbicacion)
        {
            var currentActivoUbicacion = await _unitOfWork.activoUbicacionesRepository.GetByID(activoUbicacion.id);
            if (currentActivoUbicacion == null)
            {
                throw new AguilaException("Activo no existente...");
            }

            currentActivoUbicacion.idActivo = activoUbicacion.idActivo;
            currentActivoUbicacion.idUbicacion = activoUbicacion.idUbicacion;
            currentActivoUbicacion.observaciones = activoUbicacion.observaciones;

            _unitOfWork.activoUbicacionesRepository.Update(currentActivoUbicacion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteActivoUbicacion(int id)
        {
            var currentActivoUbicacion = await _unitOfWork.activoUbicacionesRepository.GetByID(id);
            if (currentActivoUbicacion == null)
            {
                throw new AguilaException("Activo no existente...");
            }

            await _unitOfWork.activoUbicacionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
