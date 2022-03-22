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
    public class activoEstadosController : ControllerBase
    {
        private readonly IactivoEstadosService _activoEstadosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public activoEstadosController(IactivoEstadosService activoEstadosService, IMapper mapper, IPasswordService passwordService)
        {
            _activoEstadosService = activoEstadosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene todos los estados de activos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoEstadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoEstados([FromQuery] activoEstadosQueryFilter filter)
        {
            var activoEstados = _activoEstadosService.GetActivoEstados(filter);
            var activoEstadosDto = _mapper.Map<IEnumerable<activoEstadosDto>>(activoEstados);

            var metadata = new Metadata
            {
                TotalCount = activoEstados.TotalCount,
                PageSize = activoEstados.PageSize,
                CurrentPage = activoEstados.CurrentPage,
                TotalPages = activoEstados.TotalPages,
                HasNextPage = activoEstados.HasNextPage,
                HasPreviousPage = activoEstados.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<activoEstadosDto>>(activoEstadosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un Estado de activo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoEstadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoEstado(int id)
        {
            var activoEstados = await _activoEstadosService.GetActivoEstado(id);
            var activoEstadosDto = _mapper.Map<activoEstadosDto>(activoEstados);

            var response = new AguilaResponse<activoEstadosDto>(activoEstadosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo estado
        /// </summary>
        /// <param name="activoEstadoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoEstadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoEstadosDto activoEstadoDto)
        {
            var activoEstado = _mapper.Map<activoEstados>(activoEstadoDto);
            await _activoEstadosService.InsertActivoEstado(activoEstado);

            activoEstadoDto = _mapper.Map<activoEstadosDto>(activoEstado);
            var response = new AguilaResponse<activoEstadosDto>(activoEstadoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un estado de activo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activoEstadoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoEstadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, activoEstadosDto activoEstadoDto)
        {
            var activoEstado = _mapper.Map<activoEstados>(activoEstadoDto);
            activoEstado.id = id;

            var result = await _activoEstadosService.UpdateActivoEstado(activoEstado);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un estado de activo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoEstadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activoEstadosService.DeleteActivoEstado(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/ActivoEstados/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _activoEstadosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
