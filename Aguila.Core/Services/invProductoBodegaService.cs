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
    public class invProductoBodegaService : IinvProductoBodegaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public invProductoBodegaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<invProductoBodega> GetProductoBodegas(invProductoBodegaQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var invProductoBodega = _unitOfWork.invProductoBodegaRepository.GetAll();

            if (filter.idProducto != null)
            {
                invProductoBodega = invProductoBodega.Where(e => e.idProducto == filter.idProducto);
            }

            if (filter.idBodega != null)
            {
                invProductoBodega = invProductoBodega.Where(e => e.idBodega == filter.idBodega);
            }

            if (filter.maximo != null)
            {
                invProductoBodega = invProductoBodega.Where(e => e.maximo == filter.maximo);
            }

            if (filter.minimo != null)
            {
                invProductoBodega = invProductoBodega.Where(e => e.minimo == filter.minimo);
            }

            var pagedProductoBodega = PagedList<Entities.invProductoBodega>.create(invProductoBodega, filter.PageNumber, filter.PageSize);
            return pagedProductoBodega;
        }

        public async Task<invProductoBodega> GetProductoBodega(int id)
        {
            return await _unitOfWork.invProductoBodegaRepository.GetByID(id);
        }

        public async Task InsertProductoBodega(invProductoBodega invProductoBodega)
        {
            invProductoBodega.id = 0;
            invProductoBodega.fechaCreacion = DateTime.Now;

            await _unitOfWork.invProductoBodegaRepository.Add(invProductoBodega);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateProductoBodega(invProductoBodega invProductoBodega)
        {
            var currentProductoBodega = await _unitOfWork.invProductoBodegaRepository.GetByID(invProductoBodega.id);
            if (currentProductoBodega == null)
            {
                throw new AguilaException("Producto no existente...");
            }

            currentProductoBodega.idProducto = invProductoBodega.idProducto;
            currentProductoBodega.idBodega = invProductoBodega.idBodega;
            currentProductoBodega.maximo = invProductoBodega.maximo;
            currentProductoBodega.minimo = invProductoBodega.minimo;

            _unitOfWork.invProductoBodegaRepository.Update(currentProductoBodega);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteProductoBodega(int id)
        {
            var currentProductoBodega = await _unitOfWork.invProductoBodegaRepository.GetByID(id);
            if (currentProductoBodega == null)
            {
                throw new AguilaException("Producto no existente...");
            }

            await _unitOfWork.invProductoBodegaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
