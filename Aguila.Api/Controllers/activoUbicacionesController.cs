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
    public class activoUbicacionesController : ControllerBase
    {
        private readonly IactivoUbicacionesService _activoUbicacionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public activoUbicacionesController(IactivoUbicacionesService activoUbicacionesService, IMapper mapper, IPasswordService passwordService)
        {
            _activoUbicacionesService = activoUbicacionesService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los activos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoUbicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoUbicaciones([FromQuery] activoUbicacionesQueryFilter filter)
        {
            var activoUbicaciones = _activoUbicacionesService.GetActivoUbicaciones(filter);
            var activoUbicacionesDto = _mapper.Map<IEnumerable<activoUbicacionesDto>>(activoUbicaciones);

            var metadata = new Metadata
            {
                TotalCount = activoUbicaciones.TotalCount,
                PageSize = activoUbicaciones.PageSize,
                CurrentPage = activoUbicaciones.CurrentPage,
                TotalPages = activoUbicaciones.TotalPages,
                HasNextPage = activoUbicaciones.HasNextPage,
                HasPreviousPage = activoUbicaciones.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<activoUbicacionesDto>>(activoUbicacionesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un activo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoUbicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoUbicacion(int id)
        {
            var activoUbicaciones = await _activoUbicacionesService.GetActivoUbicacion(id);
            var activoUbicacionesDto = _mapper.Map<activoUbicacionesDto>(activoUbicaciones);

            var response = new AguilaResponse<activoUbicacionesDto>(activoUbicacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo activo
        /// </summary>
        /// <param name="activoUbicacionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoUbicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoUbicacionesDto activoUbicacionDto)
        {
            var activoUbicacion = _mapper.Map<activoUbicaciones>(activoUbicacionDto);
            await _activoUbicacionesService.InsertActivoUbicacion(activoUbicacion);

            activoUbicacionDto = _mapper.Map<activoUbicacionesDto>(activoUbicacion);
            var response = new AguilaResponse<activoUbicacionesDto>(activoUbicacionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un activo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activoUbicacionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoUbicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, activoUbicacionesDto activoUbicacionDto)
        {
            var activoUbicacion = _mapper.Map<activoUbicaciones>(activoUbicacionDto);
            activoUbicacion.id = id;

            var result = await _activoUbicacionesService.UpdateActivoUbicacion(activoUbicacion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un activo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoUbicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activoUbicacionesService.DeleteActivoUbicacion(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/activoUbicaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _activoUbicacionesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
