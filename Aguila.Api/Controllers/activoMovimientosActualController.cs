using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Interfaces.Trafico.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using Aguila.Interfaces.Trafico.QueryFilters;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class activoMovimientosActualController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;

        public activoMovimientosActualController(IactivoMovimientosActualService activoMovimientosActualService, IMapper mapper)
        {
            _activoMovimientosActualService = activoMovimientosActualService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene los movimientos actuales del activo registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoMovimientosActual([FromQuery] activoMovimientosActualQueryFilter filter)
        {
            var activoMovimientosActual = _activoMovimientosActualService.GetActivoMovimientosActual(filter);
            var activoMovimientosActualDto = _mapper.Map<IEnumerable<activoMovimientosActualDto>>(activoMovimientosActual);

            var metadata = new Metadata
            {
                TotalCount = activoMovimientosActual.TotalCount,
                PageSize = activoMovimientosActual.PageSize,
                CurrentPage = activoMovimientosActual.CurrentPage,
                TotalPages = activoMovimientosActual.TotalPages,
                HasNextPage = activoMovimientosActual.HasNextPage,
                HasPreviousPage = activoMovimientosActual.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<activoMovimientosActualDto>>(activoMovimientosActualDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene los movimientos actuales de un activo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoMovimientosActual(int id)
        {
            var activoMovimientosActual = await _activoMovimientosActualService.GetActivoMovimientoActual(id);
            var activoMovimientosActualDto = _mapper.Map<activoMovimientosActualDto>(activoMovimientosActual);

            if (activoMovimientosActual == null)
            {
                throw new AguilaException("Movimiento Actual No Existente", 404);
            }

            var response = new AguilaResponse<activoMovimientosActualDto>(activoMovimientosActualDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo movimiento actual para cada activo
        /// </summary>
        /// <param name="activoMovimientoActualDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoMovimientosActualDto activoMovimientoActualDto)
        {
            var activoMovimientoActual = _mapper.Map<activoMovimientosActual>(activoMovimientoActualDto);
            await _activoMovimientosActualService.InsertActivoMovimientoActual(activoMovimientoActual);

            activoMovimientoActualDto = _mapper.Map<activoMovimientosActualDto>(activoMovimientoActual);
            var response = new AguilaResponse<activoMovimientosActualDto>(activoMovimientoActualDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un movimiento actual, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activoMovimientoActualDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, activoMovimientosActualDto activoMovimientoActualDto)
        {
            var activoMovimientoActual = _mapper.Map<activoMovimientosActual>(activoMovimientoActualDto);
            activoMovimientoActual.idActivo = id;

            var result = await _activoMovimientosActualService.UpdateActivoMovimientoActual(activoMovimientoActual);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un movimiento actual
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activoMovimientosActualService.DeleteActivoMovimientoActual(id);
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
            var recurso = await _activoMovimientosActualService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
