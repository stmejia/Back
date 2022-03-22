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
    public class estadosController : ControllerBase
    {
        private readonly IestadosService _estadosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public estadosController(IestadosService estadosService, IMapper mapper, IPasswordService passwordService)
        {
            _estadosService = estadosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los estados registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEstados([FromQuery] estadosQueryFilter filter)
        {
            var estados = _estadosService.GetEstados(filter);
            var estadosDto = _mapper.Map<IEnumerable<estadosDto>>(estados);

            var metadata = new Metadata
            {
                TotalCount = estados.TotalCount,
                PageSize = estados.PageSize,
                CurrentPage = estados.CurrentPage,
                TotalPages = estados.TotalPages,
                HasNextPage = estados.HasNextPage,
                HasPreviousPage = estados.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un estado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEstado(int id)
        {
            var estados = await _estadosService.GetEstado(id);
            var estadosDto = _mapper.Map<estadosDto>(estados);

            var response = new AguilaResponse<estadosDto>(estadosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo estado
        /// </summary>
        /// <param name="estadoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(estadosDto estadoDto)
        {
            //if (_estadosService.existeDato(estadoDto.codigo, estadoDto.idEmpresa, estadoDto.numeroOrden))
            //{
            //    throw new AguilaException("No se pueden duplicar los datos: CODIGO Y NUMERO ORDEN en una misma empresa...", 406);
            //}

            var estado = _mapper.Map<estados>(estadoDto);
            await _estadosService.InsertEstado(estado);

            estadoDto = _mapper.Map<estadosDto>(estado);
            var response = new AguilaResponse<estadosDto>(estadoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un estado, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estadoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, estadosDto estadoDto)
        {
            var estado = _mapper.Map<estados>(estadoDto);
            estado.id = id;

            var result = await _estadosService.UpdateEstado(estado);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un estado, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _estadosService.DeleteEstado(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Estados/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _estadosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
