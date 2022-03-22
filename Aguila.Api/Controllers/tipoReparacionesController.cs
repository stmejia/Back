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
    public class tipoReparacionesController : ControllerBase
    {
        private readonly ItipoReparacionesService _tipoReparacionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoReparacionesController(ItipoReparacionesService tipoReparacionesService,IMapper mapper, IPasswordService passwordService)
        {
            _tipoReparacionesService = tipoReparacionesService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de tipos de Reparaciones, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoReparacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTiposReparacion([FromQuery] tipoReparacionesQueryFilter filter)
        {
            var tipos = _tipoReparacionesService.GetTiposReparaciones(filter);
            var tiposDto = _mapper.Map<IEnumerable<tipoReparacionesDto>>(tipos);

            var metadata = new Metadata
            {
                TotalCount = tipos.TotalCount,
                PageSize = tipos.PageSize,
                CurrentPage = tipos.CurrentPage,
                TotalPages = tipos.TotalPages,
                HasNextPage = tipos.HasNextPage,
                HasPreviousPage = tipos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<tipoReparacionesDto>>(tiposDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de tipo de Reparacion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<tipoReparacionesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoReparacion(int id)
        {
            var tipo = await _tipoReparacionesService.GetTipoReparacion(id);
            var tipoDto = _mapper.Map<tipoReparacionesDto>(tipo);

            var response = new AguilaResponse<tipoReparacionesDto>(tipoDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Nuevo Tipo Reparacion
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<tipoReparacionesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoReparacionesDto tipoDTo)
        {
            var tipo = _mapper.Map<tipoReparaciones>(tipoDTo);

            await _tipoReparacionesService.InsertTipoReparacion(tipo);

            tipoDTo = _mapper.Map<tipoReparacionesDto>(tipo);
            var response = new AguilaResponse<tipoReparacionesDto>(tipoDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion tipo de Reparacion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoReparacionesDto tipoDTo)
        {
            var tipo = _mapper.Map<tipoReparaciones>(tipoDTo);
            tipo.id = id;

            var result = await _tipoReparacionesService.UpdateTipoReparacion(tipo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar tipo de Reparacion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _tipoReparacionesService.DeleteTipoReparacion(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/tipoReparaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _tipoReparacionesService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
