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
    public class productosService : IproductosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public productosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<productos> GetProductos(productosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var productos = _unitOfWork.productosRepository.GetAll();

            if (filter.codigo != null)
            {
                productos = productos.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.codigoQR != null)
            {
                productos = productos.Where(e => e.codigoQR.ToLower().Contains(filter.codigoQR.ToLower()));
            }

            if (filter.descripcion != null)
            {
                productos = productos.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            if (filter.bienServicio != null)
            {
                productos = productos.Where(e => e.bienServicio.ToLower().Contains(filter.bienServicio.ToLower()));
            }

            if (filter.idsubCategoria != null)
            {
                productos = productos.Where(e => e.idsubCategoria == filter.idsubCategoria);
            }

            if (filter.idEmpresa != null)
            {
                productos = productos.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.idMedida != null)
            {
                productos = productos.Where(e => e.idMedida == filter.idMedida);
            }

            if (filter.fechaBaja != null)
            {
                productos = productos.Where(e => e.fechaBaja == filter.fechaBaja);
            }

            var pagedProductos = PagedList<productos>.create(productos, filter.PageNumber, filter.PageSize);
            return pagedProductos;
        }

        public async Task<productos> GetProducto(int id)
        {
            return await _unitOfWork.productosRepository.GetByID(id);
        }

        public async Task InsertProducto(productos producto)
        {
            //Insertamos la fecha de ingreso del registro
            producto.id = 0;
            producto.fechaCreacion = DateTime.Now;

            await _unitOfWork.productosRepository.Add(producto);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateProducto(productos producto)
        {
            var currentProducto = await _unitOfWork.productosRepository.GetByID(producto.id);
            if (currentProducto == null)
            {
                throw new AguilaException("Producto no existente...");
            }

            currentProducto.codigo = producto.codigo;
            currentProducto.codigoQR = producto.codigoQR;
            currentProducto.descripcion = producto.descripcion;
            currentProducto.bienServicio = producto.bienServicio;
            currentProducto.idsubCategoria = producto.idsubCategoria;
            currentProducto.idEmpresa = producto.idEmpresa;
            currentProducto.idMedida = producto.idMedida;
            currentProducto.fechaBaja = producto.fechaBaja;

            _unitOfWork.productosRepository.Update(currentProducto);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteProducto(int id)
        {
            var currentProducto = await _unitOfWork.productosRepository.GetByID(id);
            if (currentProducto == null)
            {
                throw new AguilaException("Producto no existente...");
            }

            await _unitOfWork.productosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
