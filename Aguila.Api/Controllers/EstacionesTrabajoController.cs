using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;




namespace Aguila.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionesTrabajoController : ControllerBase
    {
        private readonly IEstacionesTrabajoService _estacionesTrabajoService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public EstacionesTrabajoController(IEstacionesTrabajoService estacionesTrabajoService, IMapper mapper, IPasswordService passwordService)
        {
            _estacionesTrabajoService = estacionesTrabajoService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Estaciones de Trabajo, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<EstacionesTrabajoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEstacionesTrabajo([FromQuery] EstacionesTrabajoQueryFilter filter)
        {
            var estaciones =  _estacionesTrabajoService.GetEstacionesTrabajo(filter);
            var estacionesDto = _mapper.Map<IEnumerable<EstacionesTrabajoDto>>(estaciones);

            var metadata = new Metadata
            {
                TotalCount = estaciones.TotalCount,
                PageSize = estaciones.PageSize,
                CurrentPage = estaciones.CurrentPage,
                TotalPages = estaciones.TotalPages,
                HasNextPage = estaciones.HasNextPage,
                HasPreviousPage = estaciones.HasPreviousPage,

            };

            var response = new AguilaResponse<IEnumerable<EstacionesTrabajoDto>>(estacionesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Estacion de Trabajo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<EstacionesTrabajoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEstacionTrabajo(int id)
        {
            var estaciones = await _estacionesTrabajoService.GetEstacionTrabajo(id);
            var estacionesDto = _mapper.Map<EstacionesTrabajoDto>(estaciones);

            var response = new AguilaResponse<EstacionesTrabajoDto>(estacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Estacion de Trabajo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<EstacionesTrabajoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(EstacionesTrabajoDto estacionesTrabajoDto)
        {
            var estacion = _mapper.Map<EstacionesTrabajo>(estacionesTrabajoDto);
            await _estacionesTrabajoService.InsertEstacionTrabajo(estacion);

            estacionesTrabajoDto = _mapper.Map<EstacionesTrabajoDto>(estacion);
            var response = new AguilaResponse<EstacionesTrabajoDto>(estacionesTrabajoDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Estacion de Trabajo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estacionTrabajolDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(byte id, EstacionesTrabajoDto estacionTrabajolDTo)
        {
            var estacion = _mapper.Map<EstacionesTrabajo>(estacionTrabajolDTo);
            estacion.Id = id;

            var result = await _estacionesTrabajoService.updateEstacionTrabajo(estacion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Estacion de Trabajo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _estacionesTrabajoService.DeleteEstacionTrabajo(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/EstacionesTrabajo/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _estacionesTrabajoService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
