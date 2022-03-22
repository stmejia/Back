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
    public class invProductoBodegaController : ControllerBase
    {
        private readonly IinvProductoBodegaService _invProductoBodegaService;
        private readonly IproductosService _productosService;
        private readonly IEstacionesTrabajoService _estacionesTrabajoService;
        private readonly ISucursalService _sucursalService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public invProductoBodegaController(IinvProductoBodegaService invProductoBodegaService, IMapper mapper, IPasswordService passwordService,
                                          IproductosService productosService,
                                          IEstacionesTrabajoService estacionesTrabajoService,
                                          ISucursalService sucursalService)
        {
            _invProductoBodegaService = invProductoBodegaService;
            _productosService = productosService;
            _estacionesTrabajoService = estacionesTrabajoService;
            _sucursalService = sucursalService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los inventarios registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invProductoBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductosBodega([FromQuery] invProductoBodegaQueryFilter filter)
        {
            var invProductoBodega = _invProductoBodegaService.GetProductoBodegas(filter);
            var invProductoBodegaDto = _mapper.Map<IEnumerable<invProductoBodegaDto>>(invProductoBodega);

            foreach (var productoBodega in invProductoBodegaDto)
            {
                //Get de objetos
                var estacionTrabajo = await _estacionesTrabajoService.GetEstacionTrabajo(productoBodega.idBodega);
                var estacionTrabajoDto = _mapper.Map<EstacionesTrabajoDto>(estacionTrabajo);
                var sucursal = await _sucursalService.GetSucursal(estacionTrabajo.SucursalId);
                var sucursalDto = _mapper.Map<SucursalDto>(sucursal);

                //Set de objetos
                productoBodega.estacionTrabajo = estacionTrabajoDto;
                productoBodega.estacionTrabajo.Sucursal = sucursalDto;
            }

            var metadata = new Metadata
            {
                TotalCount = invProductoBodega.TotalCount,
                PageSize = invProductoBodega.PageSize,
                CurrentPage = invProductoBodega.CurrentPage,
                TotalPages = invProductoBodega.TotalPages,
                HasNextPage = invProductoBodega.HasNextPage,
                HasPreviousPage = invProductoBodega.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<invProductoBodegaDto>>(invProductoBodegaDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invProductoBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductoBodega(int id)
        {
            var invProductoBodega = await _invProductoBodegaService.GetProductoBodega(id);
            var invProductoBodegaDto = _mapper.Map<invProductoBodegaDto>(invProductoBodega);

            var response = new AguilaResponse<invProductoBodegaDto>(invProductoBodegaDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo inventario
        /// </summary>
        /// <param name="invProductoBodegaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invProductoBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(invProductoBodegaDto invProductoBodegaDto)
        {
            var invProductoBodega = _mapper.Map<invProductoBodega>(invProductoBodegaDto);
            var producto = await _productosService.GetProducto(invProductoBodegaDto.idProducto);
            var estacionTrabajo = await _estacionesTrabajoService.GetEstacionTrabajo(invProductoBodegaDto.idBodega);

            if (producto == null)
            {
                throw new AguilaException("Producto No Existente", 404);
            }

            if (estacionTrabajo == null)
            {
                throw new AguilaException("Estación de trabajo No Existente", 404);
            }

            await _invProductoBodegaService.InsertProductoBodega(invProductoBodega);

            invProductoBodegaDto = _mapper.Map<invProductoBodegaDto>(invProductoBodega);
            var response = new AguilaResponse<invProductoBodegaDto>(invProductoBodegaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un invetario, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invProductoBodegaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invProductoBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, invProductoBodegaDto invProductoBodegaDto)
        {
            var invProductoBodega = _mapper.Map<invProductoBodega>(invProductoBodegaDto);
            invProductoBodega.id = id;

            var producto = await _productosService.GetProducto(invProductoBodegaDto.idProducto);
            var estacionTrabajo = await _estacionesTrabajoService.GetEstacionTrabajo(invProductoBodegaDto.idBodega);

            if (producto == null)
            {
                throw new AguilaException("Producto No Existente", 404);
            }

            if (estacionTrabajo == null)
            {
                throw new AguilaException("Estación de trabajo No Existente", 404);
            }

            var result = await _invProductoBodegaService.UpdateProductoBodega(invProductoBodega);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un inventario, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invProductoBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invProductoBodegaService.DeleteProductoBodega(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/InvProductoBodega/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _invProductoBodegaService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
