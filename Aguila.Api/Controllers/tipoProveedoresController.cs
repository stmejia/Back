using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using Aguila.Core.Entities;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class tipoProveedoresController : ControllerBase
    {
        private readonly ItipoProveedoresService _tipoProvedoresService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public tipoProveedoresController(ItipoProveedoresService tipoProvedoresService, IMapper mapper, IPasswordService passwordService)
        {
            _tipoProvedoresService = tipoProvedoresService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Tipos de Proveedores , enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoProveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public IActionResult GetTipoPoveedores([FromQuery] tipoProveedoresQueryFilter filter)
        {
            var tipos = _tipoProvedoresService.GetTiposProveedores(filter);
            var tiposDto = _mapper.Map<IEnumerable<tipoProveedoresDto>>(tipos);

            var metadata = new Metadata
            {
                TotalCount = tipos.TotalCount,
                PageSize = tipos.PageSize,
                CurrentPage = tipos.CurrentPage,
                TotalPages = tipos.TotalPages,
                HasNextPage = tipos.HasNextPage,
                HasPreviousPage = tipos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<tipoProveedoresDto>>(tiposDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un Tipo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoProveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoProveedor(int id)
        {
            var tipos = await _tipoProvedoresService.GetTipoProveedor(id);
            var tiposDto = _mapper.Map<tipoProveedoresDto>(tipos);

            if (tipos == null)
            {
                throw new AguilaException("Tipo No Existente", 404);
            }

            var response = new AguilaResponse<tipoProveedoresDto>(tiposDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo
        /// </summary>
        /// <param name="tipoProveedorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoProveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoProveedoresDto tipoProveedorDto)
        {
            var tipos = _mapper.Map<tipoProveedores>(tipoProveedorDto);
            await _tipoProvedoresService.InsertTipoProveedor(tipos);

            tipoProveedorDto = _mapper.Map<tipoProveedoresDto>(tipos);
            var response = new AguilaResponse<tipoProveedoresDto>(tipoProveedorDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un proveedor, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoproveedorDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoProveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoProveedoresDto tipoproveedorDto)
        {
            var tipos = _mapper.Map<tipoProveedores>(tipoproveedorDto);
            tipos.id = id;

            var result = await _tipoProvedoresService.UpdateTipoProveedor(tipos);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un proveedor, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoProveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoProvedoresService.DeleteTipoProveedor(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/TipoProveedores/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tipoProvedoresService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
