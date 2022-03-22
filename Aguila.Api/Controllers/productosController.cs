using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class productosController : ControllerBase
    {
        private readonly IproductosService _productosService;
        private readonly IinvCategoriaService _invCategoriaService;
        private readonly IinvSubCategoriaService _invSubCategoriaService;
        private readonly ImedidasService _medidasService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public productosController(IproductosService productosService, IMapper mapper, IPasswordService passwordService,
                                   IinvCategoriaService invCategoriaService,
                                   IinvSubCategoriaService invSubCategoriaService,
                                   ImedidasService medidasService)
        {
            _productosService = productosService;
            _invCategoriaService = invCategoriaService;
            _invSubCategoriaService = invSubCategoriaService;
            _medidasService = medidasService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los productos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<productosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductos([FromQuery] productosQueryFilter filter)
        {
            var productos = _productosService.GetProductos(filter);
            var productosDto = _mapper.Map<IEnumerable<productosDto>>(productos);

            foreach (var producto in productosDto)
            {
                //Get de objetos
                var medidas = await _medidasService.GetMedida(producto.idMedida);
                var subCategoria = await _invSubCategoriaService.GetInvSubCategoria(producto.idsubCategoria);
                var categoria = await _invCategoriaService.GetInvCategoria(subCategoria.idInvCategoria);

                //Set de objetos
                producto.nombreMedida = medidas.nombre;
                producto.descSubCategoria = subCategoria.descripcion;
                producto.descCategoria = categoria.descripcion;
            }

            var metadata = new Metadata
            {
                TotalCount = productos.TotalCount,
                PageSize = productos.PageSize,
                CurrentPage = productos.CurrentPage,
                TotalPages = productos.TotalPages,
                HasNextPage = productos.HasNextPage,
                HasPreviousPage = productos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<productosDto>>(productosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<productosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsesor(int id)
        {
            var productos = await _productosService.GetProducto(id);
            var productosDto = _mapper.Map<productosDto>(productos);

            if (productos == null)
            {
                throw new AguilaException("Producto no existente", 404);
            }

            //Get Id's
            var productosCategoria = await _invSubCategoriaService.GetInvSubCategoria(productos.idsubCategoria);
            var categoria = await _invCategoriaService.GetInvCategoria(productosCategoria.idInvCategoria);

            //Set de los id de cada objeto
            productosDto.idCategoria = categoria.id;

            var response = new AguilaResponse<productosDto>(productosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        /// <param name="productoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<productosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(productosDto productoDto)
        {
            var producto = _mapper.Map<productos>(productoDto);
            await _productosService.InsertProducto(producto);

            productoDto = _mapper.Map<productosDto>(producto);
            var response = new AguilaResponse<productosDto>(productoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un producto, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<productosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, productosDto productoDto)
        {
            var producto = _mapper.Map<productos>(productoDto);
            producto.id = id;

            var result = await _productosService.UpdateProducto(producto);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un producto, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<productosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productosService.DeleteProducto(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Productos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _productosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
