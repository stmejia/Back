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
    public class activoGeneralesController : ControllerBase
    {
        private readonly IactivoGeneralesService _activoGeneralesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public activoGeneralesController(IactivoGeneralesService activoGeneralesService, IMapper mapper, IPasswordService passwordService)
        {
            _activoGeneralesService = activoGeneralesService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Activos Generales , enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoGeneralesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoGenerales([FromQuery] activoGeneralesQueryFilter filter)
        {
            var activos = _activoGeneralesService.GetActivosGenerales(filter);
            var activosDto = _mapper.Map<IEnumerable<activoGeneralesDto>>(activos);

            var metadata = new Metadata
            {
                TotalCount = activos.TotalCount,
                PageSize = activos.PageSize,
                CurrentPage = activos.CurrentPage,
                TotalPages = activos.TotalPages,
                HasNextPage = activos.HasNextPage,
                HasPreviousPage = activos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<activoGeneralesDto>>(activosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Activo General, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<activoGeneralesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoGeneral(int id)
        {
            var activo = await _activoGeneralesService.GetActivoGeneral(id);
            var activoDto = _mapper.Map<activoGeneralesDto>(activo);

            var response = new AguilaResponse<activoGeneralesDto>(activoDto);
            return Ok(response);
        }


        /// <summary>
        /// Crear Nuevo Activo General
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<activoGeneralesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoGeneralesDto tipoDTo)
        {
            var activo = _mapper.Map<activoGenerales>(tipoDTo);

            await _activoGeneralesService.InsertActivoGeneral(activo);

            tipoDTo = _mapper.Map<activoGeneralesDto>(activo);
            var response = new AguilaResponse<activoGeneralesDto>(tipoDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion Activo General, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, activoGeneralesDto activoDTo)
        {
            var activo = _mapper.Map<activoGenerales>(activoDTo);
            activo.id = id;

            var result = await _activoGeneralesService.UpdateActivoGeneral(activo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Activo General, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _activoGeneralesService.DeleteActivoGeneral(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/activoGenerales/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _activoGeneralesService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
