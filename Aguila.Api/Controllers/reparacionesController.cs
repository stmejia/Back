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
    public class reparacionesController : ControllerBase
    {
        private readonly IreparacionesService _reparacionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public reparacionesController(IreparacionesService reparacionesService, IMapper mapper, IPasswordService passwordService)
        {
            _reparacionesService = reparacionesService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Reparaciones, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<reparacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetReparaciones([FromQuery] reparacionesQueryFilter filter)
        {
            var reparaciones = _reparacionesService.GetReparaciones(filter);
            var reparacionesDto = _mapper.Map<IEnumerable<reparacionesDto>>(reparaciones);

            var metadata = new Metadata
            {
                TotalCount = reparaciones.TotalCount,
                PageSize = reparaciones.PageSize,
                CurrentPage = reparaciones.CurrentPage,
                TotalPages = reparaciones.TotalPages,
                HasNextPage = reparaciones.HasNextPage,
                HasPreviousPage = reparaciones.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<reparacionesDto>>(reparacionesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Reparacion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<reparacionesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetrReparacion(int id)
        {
            var reparacion = await _reparacionesService.GetReparacion(id);
            var reparacionDto = _mapper.Map<reparacionesDto>(reparacion);

            var response = new AguilaResponse<reparacionesDto>(reparacionDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Nueva Reparacion
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<reparacionesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(reparacionesDto reparacionDTo)
        {
            var reparacion = _mapper.Map<reparaciones>(reparacionDTo);

            await _reparacionesService.InsertReparacion(reparacion);

            reparacionDTo = _mapper.Map<reparacionesDto>(reparacion);
            var response = new AguilaResponse<reparacionesDto>(reparacionDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Reparacion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reparacionDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, reparacionesDto reparacionDTo)
        {
            var reparacion = _mapper.Map<reparaciones>(reparacionDTo);
            reparacion.id = id;

            var result = await _reparacionesService.UpdateReparacion(reparacion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Reparacion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _reparacionesService.DeleteReparacion(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/reparaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _reparacionesService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
