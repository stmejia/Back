using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class detalleCondicionController : ControllerBase
    {
        private readonly IdetalleCondicionService _detalleCondicionService;
        private readonly IMapper _mapper;

        public detalleCondicionController(IdetalleCondicionService detalleCondicionService, IMapper mapper)
        {
            _detalleCondicionService = detalleCondicionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene el detalle de las condiciones registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<detalleCondicionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCondicionDetalle([FromQuery] detalleCondicionQueryFilter filter)
        {
            var detalleCondicion = _detalleCondicionService.GetCondicionDetalle(filter);
            var detalleCondicionDto = _mapper.Map<IEnumerable<detalleCondicionDto>>(detalleCondicion);

            var metadata = new Metadata
            {
                TotalCount = detalleCondicion.TotalCount,
                PageSize = detalleCondicion.PageSize,
                CurrentPage = detalleCondicion.CurrentPage,
                TotalPages = detalleCondicion.TotalPages,
                HasNextPage = detalleCondicion.HasNextPage,
                HasPreviousPage = detalleCondicion.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<detalleCondicionDto>>(detalleCondicionDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un detalle de las condiciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<detalleCondicionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionDetalle(long id)
        {
            var detalleCondicion = await _detalleCondicionService.GetCondicionDetalle(id);

            var detalleCondicionDto = _mapper.Map<detalleCondicionDto>(detalleCondicion);
            var response = new AguilaResponse<detalleCondicionDto>(detalleCondicionDto);

            return Ok(response);
        }

        /// <summary>
        /// Devuelve el historico 
        /// Todos los detalles creados por cada condición
        /// Se tiene que envíar el campo idCondicion
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("historial")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<detalleCondicionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoCondicionHistorial([FromQuery] condicionesHistorialQueryFilter filter)
        {
            var xCondicionTallerQueryFilter = new detalleCondicionQueryFilter()
            {
                idCondicion = filter.idCondicion,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var condicionTaller = _detalleCondicionService.GetCondicionDetalle(xCondicionTallerQueryFilter);
            var condicionTallerDto = _mapper.Map<IEnumerable<detalleCondicionDto>>(condicionTaller);

            var metadata = new Metadata
            {
                TotalCount = condicionTaller.TotalCount,
                PageSize = condicionTaller.PageSize,
                CurrentPage = condicionTaller.CurrentPage,
                TotalPages = condicionTaller.TotalPages,
                HasNextPage = condicionTaller.HasNextPage,
                HasPreviousPage = condicionTaller.HasPreviousPage,
            };


            var response = new AguilaResponse<IEnumerable<detalleCondicionDto>>(condicionTallerDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo detalle para la condicion
        /// </summary>
        /// <param name="detalleCondicionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<detalleCondicionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(detalleCondicionDto detalleCondicionDto)
        {
            if (detalleCondicionDto.idCondicion == null)
                throw new AguilaException("Condicion no existente");

            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            var detalleCondicion = _mapper.Map<detalleCondicion>(detalleCondicionDto);
            detalleCondicion.idUsuario = usuarioId;
            detalleCondicion.idUsuarioAutoriza = usuarioId;

            await _detalleCondicionService.InsertCondicionDetalle(detalleCondicion);
            detalleCondicionDto = _mapper.Map<detalleCondicionDto>(detalleCondicion);

            var response = new AguilaResponse<detalleCondicionDto>(detalleCondicionDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza el detalle de una condicion, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="detalleCondicionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<detalleCondicionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, detalleCondicionDto detalleCondicionDto)
        {
            var detalleCondicion = _mapper.Map<detalleCondicion>(detalleCondicionDto);

            var result = await _detalleCondicionService.UpdateCondicionDetalle(detalleCondicion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina el detalle de una condicion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<detalleCondicionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _detalleCondicionService.DeleteCondicionDetalle(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _detalleCondicionService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
