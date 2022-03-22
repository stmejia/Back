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
    public class transportesService : ItransportesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public transportesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<transportes> GetTransportes(transportesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var transportes = _unitOfWork.transportesRepository.GetAll();

            if (filter.codigo != null)
            {
                transportes = transportes.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.nombre != null)
            {
                transportes = transportes.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.idProveedor != null)
            {
                transportes = transportes.Where(e => e.idProveedor == filter.idProveedor);
            }

            if (filter.propio != null)
            {
                transportes = transportes.Where(e => e.propio == filter.propio);
            }

            var pagedTransportes = PagedList<transportes>.create(transportes, filter.PageNumber, filter.PageSize);
            return pagedTransportes;
        }

        public async Task<transportes> GetTransporte(int id)
        {
            return await _unitOfWork.transportesRepository.GetByID(id);
        }

        public async Task InsertTransporte(transportes transporte)
        {
            //Insertamos la fecha de ingreso del registro
            transporte.id = 0;
            transporte.fechaCreacion = DateTime.Now;

            await _unitOfWork.transportesRepository.Add(transporte);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTransporte(transportes transporte)
        {
            var currentTransporte = await _unitOfWork.transportesRepository.GetByID(transporte.id);
            if (currentTransporte == null)
            {
                throw new AguilaException("Transporte no existente...");
            }

            currentTransporte.nombre = transporte.nombre;
            currentTransporte.idProveedor = transporte.idProveedor;
            currentTransporte.propio = transporte.propio;

            _unitOfWork.transportesRepository.Update(currentTransporte);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTransporte(int id)
        {
            var currentTransporte = await _unitOfWork.transportesRepository.GetByID(id);
            if (currentTransporte == null)
            {
                throw new AguilaException("Transporte no existente...");
            }

            await _unitOfWork.transportesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
