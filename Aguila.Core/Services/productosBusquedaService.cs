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
    public class productosBusquedaService : IproductosBusquedaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public productosBusquedaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<productosBusqueda> GetProductosBusqueda(productosBusquedaQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var productosBusqueda = _unitOfWork.productosBusquedaRepository.GetAll();

            if (filter.codigo != null)
            {
                productosBusqueda = productosBusqueda.Where(e => e.codigo.ToLower().Contains(filter.codigo));
            }

            if (filter.descripcion != null)
            {
                productosBusqueda = productosBusqueda.Where(e => e.descripcion.ToLower().Contains(filter.descripcion));
            }

            if (filter.idProducto != null)
            {
                productosBusqueda = productosBusqueda.Where(e => e.idProducto == filter.idProducto);
            }

            var pagedProductosBusqueda = PagedList<productosBusqueda>.create(productosBusqueda, filter.PageNumber, filter.PageSize);
            return pagedProductosBusqueda;
        }

        public async Task<productosBusqueda> GetProductoBusqueda(int id)
        {
            return await _unitOfWork.productosBusquedaRepository.GetByID(id);
        }

        public async Task InsertProductoBusqueda(productosBusqueda productoBusqueda)
        {
            //Insertamos la fecha de ingreso del registro
            productoBusqueda.id = 0;
            productoBusqueda.fechaCreacion = DateTime.Now;

            await _unitOfWork.productosBusquedaRepository.Add(productoBusqueda);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateProductoBusqueda(productosBusqueda productoBusqueda)
        {
            var currentProductoBusqueda = await _unitOfWork.productosBusquedaRepository.GetByID(productoBusqueda.id);
            if (currentProductoBusqueda == null)
            {
                throw new AguilaException("Producto no existente...");
            }

            currentProductoBusqueda.descripcion = productoBusqueda.descripcion;
            currentProductoBusqueda.idProducto = productoBusqueda.idProducto;

            _unitOfWork.productosBusquedaRepository.Update(currentProductoBusqueda);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteProductoBusqueda(int id)
        {
            var currentProductoBusqueda = await _unitOfWork.productosBusquedaRepository.GetByID(id);
            if (currentProductoBusqueda == null)
            {
                throw new AguilaException("Producto no existente...");
            }

            await _unitOfWork.productosBusquedaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
