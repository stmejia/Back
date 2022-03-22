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
    public class entidadesComercialesDireccionesService : IentidadesComercialesDireccionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public entidadesComercialesDireccionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<entidadesComercialesDirecciones> GetEntidadComercialDireccion(entidadesComercialesDireccionesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var entidadComercialDireccion = _unitOfWork.entidadesComercialesDireccionesRepository.GetAll();

            if (filter.idEntidadComercial != null)
            {
                entidadComercialDireccion = entidadComercialDireccion.Where(e => e.idEntidadComercial == filter.idEntidadComercial);
            }

            if (filter.idDireccion != null)
            {
                entidadComercialDireccion = entidadComercialDireccion.Where(e => e.idDireccion == filter.idDireccion);
            }

            if (filter.descripcion != null)
            {
                entidadComercialDireccion = entidadComercialDireccion.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }
                       

            var pagedEntidadComercialDireccion = PagedList<entidadesComercialesDirecciones>.create(entidadComercialDireccion, filter.PageNumber, filter.PageSize);

            return pagedEntidadComercialDireccion;
        }

        public async Task<entidadesComercialesDirecciones> GetEntidadComercialDireccion(long id)
        {
            return await _unitOfWork.entidadesComercialesDireccionesRepository.GetByID(id);
        }

        public async Task InsertEntidadComercialDireccion(entidadesComercialesDirecciones entidadComercialDireccion)
        {
            //Insertamos la fecha de ingreso del registro
            entidadComercialDireccion.id = 0;
            entidadComercialDireccion.fechaCreacion = DateTime.Now;
            //entidadComercialDireccion.direccion.fechaCreacion = DateTime.Now;

            await _unitOfWork.entidadesComercialesDireccionesRepository.Add(entidadComercialDireccion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateEntidadComercialDireccion(entidadesComercialesDirecciones entidadComercialDireccion)
        {
            var currentEntidadComercialDireccion = await _unitOfWork.entidadesComercialesDireccionesRepository.GetByID(entidadComercialDireccion.id);
            if (currentEntidadComercialDireccion == null)
            {
                throw new AguilaException("Entidad no existente...");
            }

            currentEntidadComercialDireccion.idEntidadComercial = entidadComercialDireccion.idEntidadComercial;
            currentEntidadComercialDireccion.idDireccion = entidadComercialDireccion.idDireccion;
            currentEntidadComercialDireccion.descripcion = entidadComercialDireccion.descripcion;

            _unitOfWork.entidadesComercialesDireccionesRepository.Update(currentEntidadComercialDireccion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteEntidadComercialDireccion(long id)
        {
            var currentEntidadComercialDireccion = await _unitOfWork.entidadesComercialesDireccionesRepository.GetByID(id);
            if (currentEntidadComercialDireccion == null)
            {
                throw new AguilaException("Entidad no existente...");
            }

            await _unitOfWork.entidadesComercialesDireccionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
