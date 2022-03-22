using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
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
    public class tipoActivosController : ControllerBase
    {

        private readonly ItipoActivosService _tipoActivosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoActivosController(ItipoActivosService tipoActivosService, IMapper mapper, IPasswordService passwordService)
        {
            _tipoActivosService = tipoActivosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Tipos de Activo , enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTipoActivos([FromQuery] tipoActivosQueryFilter filter)
        {
            var tipos = _tipoActivosService.GetTipoActivos(filter);
            var tiposDto = _mapper.Map<IEnumerable<tipoActivosDto>>(tipos);

            var metadata = new Metadata
            {
                TotalCount = tipos.TotalCount,
                PageSize = tipos.PageSize,
                CurrentPage = tipos.CurrentPage,
                TotalPages = tipos.TotalPages,
                HasNextPage = tipos.HasNextPage,
                HasPreviousPage = tipos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<tipoActivosDto>>(tiposDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }


        /// <summary>
        /// Consulta de Tipo Activo , enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<tipoActivosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoActivo(int id)
        {
            var tipo = await _tipoActivosService.GetTipoActivo(id);
            var tipoDto = _mapper.Map<tipoActivosDto>(tipo);

            var response = new AguilaResponse<tipoActivosDto>(tipoDto);
            return Ok(response);
        }


        /// <summary>
        /// Crear Nuevo Tipo Activo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<tipoActivosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoActivosDto tipoDTo)
        {
            var tipo = _mapper.Map<tipoActivos>(tipoDTo);

            await _tipoActivosService.InsertTipoActivo(tipo);

            tipoDTo = _mapper.Map<tipoActivosDto>(tipo);
            var response = new AguilaResponse<tipoActivosDto>(tipoDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Tipo Activo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoActivosDto tipoDTo)
        {
            var tipo = _mapper.Map<tipoActivos>(tipoDTo);
            tipo.id = id;

            var result = await _tipoActivosService.UpdateTipoActivo(tipo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar tipo Activo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _tipoActivosService.DeleteTipoActivo(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/tipoActivos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _tipoActivosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
