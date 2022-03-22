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
    public class direccionesService : IdireccionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public direccionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<direcciones> GetDirecciones(direccionesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var direcciones = _unitOfWork.direccionesRepository.GetAll();

            if (filter.idMunicipio != null)
            {
                direcciones = direcciones.Where(e => e.idMunicipio == filter.idMunicipio);
            }

            if (filter.colonia != null)
            {
                direcciones = direcciones.Where(e => e.colonia.ToLower().Contains(filter.colonia.ToLower()));
            }

            if (filter.zona != null)
            {
                direcciones = direcciones.Where(e => e.zona.ToLower().Contains(filter.zona.ToLower()));
            }

            if (filter.codigoPostal != null)
            {
                direcciones = direcciones.Where(e => e.codigoPostal.ToLower().Contains(filter.codigoPostal.ToLower()));
            }

            if (filter.direccion != null)
            {
                direcciones = direcciones.Where(e => e.direccion.ToLower().Contains(filter.direccion.ToLower()));
            }

            var pagedDirecciones = PagedList<direcciones>.create(direcciones, filter.PageNumber, filter.PageSize);

            return pagedDirecciones;
        }

        public async Task<direcciones> GetDireccion(long id)
        {
            return await _unitOfWork.direccionesRepository.GetByID(id);
        }

        public async Task InsertDireccion(direcciones direccion)
        {
            //Insertamos la fecha de ingreso del registro
            direccion.id = 0;
            direccion.fechaCreacion = DateTime.Now;

            await _unitOfWork.direccionesRepository.Add(direccion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateDireccion(direcciones direccion)
        {
            var currentDireccion = await _unitOfWork.direccionesRepository.GetByID(direccion.id);
            if (currentDireccion == null)
            {
                throw new AguilaException("Dirección no existente...");
            }

            currentDireccion.idMunicipio = direccion.idMunicipio;
            currentDireccion.colonia = direccion.colonia;
            currentDireccion.zona = direccion.zona;
            currentDireccion.codigoPostal = direccion.codigoPostal;
            currentDireccion.direccion = direccion.direccion;

            _unitOfWork.direccionesRepository.Update(currentDireccion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteDireccion(long id)
        {
            var currentDireccion = await _unitOfWork.direccionesRepository.GetByID(id);
            if (currentDireccion == null)
            {
                throw new AguilaException("Dirección no existente...");
            }

            await _unitOfWork.direccionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
