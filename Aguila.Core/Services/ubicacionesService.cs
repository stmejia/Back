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
    public class ubicacionesService : IubicacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public ubicacionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<ubicaciones> GetUbicaciones(ubicacionesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var ubicaciones = _unitOfWork.ubicacionesRepository.GetAll();

            if (filter.idMunicipio != null)
            {
                ubicaciones = ubicaciones.Where(e => e.idMunicipio == filter.idMunicipio);
            }

            if (filter.idEmpresa is null)
            {
                throw new AguilaException("Debe ingresar un id de Empresa");
            }
            else
            {
                ubicaciones = ubicaciones.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.codigo != null)
            {
                ubicaciones = ubicaciones.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.esPuerto != null)
            {
                ubicaciones = ubicaciones.Where(e => e.esPuerto == filter.esPuerto);
            }

            if (filter.lugar != null)
            {
                ubicaciones = ubicaciones.Where(e => e.lugar.ToLower().Contains(filter.lugar.ToLower()));
            }

            if (filter.codigoPostal != null)
            {
                ubicaciones = ubicaciones.Where(e => e.codigoPostal.ToLower().Contains(filter.codigoPostal.ToLower()));
            }

            if (filter.latitud != null)
            {
                ubicaciones = ubicaciones.Where(e => e.latitud == filter.latitud);
            }

            if (filter.longitud != null)
            {
                ubicaciones = ubicaciones.Where(e => e.longitud == filter.longitud);
            }

            var pagedUbicaciones = PagedList<ubicaciones>.create(ubicaciones, filter.PageNumber, filter.PageSize);
            return pagedUbicaciones;
        }

        public async Task<ubicaciones> GetUbicacion(int id)
        {
            return await _unitOfWork.ubicacionesRepository.GetByID(id);
        }

        public async Task InsertUbicacion(ubicaciones ubicacion)
        {
            //Insertamos la fecha de ingreso del registro
            ubicacion.id = 0;
            ubicacion.fechaCreacion = DateTime.Now;

            await _unitOfWork.ubicacionesRepository.Add(ubicacion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateUbicacion(ubicaciones ubicacion)
        {
            var currentUbicacion = await _unitOfWork.ubicacionesRepository.GetByID(ubicacion.id);
            if (currentUbicacion == null)
            {
                throw new AguilaException("Ubicación no existente...");
            }

            currentUbicacion.idMunicipio = ubicacion.idMunicipio;
            currentUbicacion.esPuerto = ubicacion.esPuerto;
            currentUbicacion.lugar = ubicacion.lugar;
            currentUbicacion.codigoPostal = ubicacion.codigoPostal;
            currentUbicacion.latitud = ubicacion.latitud;
            currentUbicacion.longitud = ubicacion.longitud;

            _unitOfWork.ubicacionesRepository.Update(currentUbicacion);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteUbicacion(int id)
        {
            var currentUbicacion = await _unitOfWork.ubicacionesRepository.GetByID(id);
            if (currentUbicacion == null)
            {
                throw new AguilaException("Ubicación no existente...");
            }

            await _unitOfWork.ubicacionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
