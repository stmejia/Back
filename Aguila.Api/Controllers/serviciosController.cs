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
using Aguila.Core.Exceptions;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class serviciosController : ControllerBase
    {
        private readonly IserviciosService _serviciosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public serviciosController(IserviciosService serviciosService, IMapper mapper, IPasswordService passwordService)
        {
            _serviciosService = serviciosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene todos los servicios registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<serviciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetServicios([FromQuery] serviciosQueryFilter filter)
        {
            var servicios = _serviciosService.GetServicios(filter);
            var serviciosDto = _mapper.Map<IEnumerable<serviciosDto>>(servicios);

            var metadata = new Metadata
            {
                TotalCount = servicios.TotalCount,
                PageSize = servicios.PageSize,
                CurrentPage = servicios.CurrentPage,
                TotalPages = servicios.TotalPages,
                HasNextPage = servicios.HasNextPage,
                HasPreviousPage = servicios.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<serviciosDto>>(serviciosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un servicio por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<serviciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDepartamentos(int id)
        {
            var servicios = await _serviciosService.GetServicio(id);
            var serviciosDto = _mapper.Map<serviciosDto>(servicios);

            if (servicios == null)
            {
                throw new AguilaException("Servicio No Existente", 404);
            }

            var response = new AguilaResponse<serviciosDto>(serviciosDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo servicio
        /// </summary>
        /// <param name="servicioDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<serviciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(serviciosDto servicioDto)
        {
            var servicio = _mapper.Map<servicios>(servicioDto);
            await _serviciosService.InsertServicio(servicio);

            servicioDto = _mapper.Map<serviciosDto>(servicio);
            var response = new AguilaResponse<serviciosDto>(servicioDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un servicio, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servicioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<serviciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, serviciosDto servicioDto)
        {
            var servicio = _mapper.Map<servicios>(servicioDto);
            servicio.id = id;

            var result = await _serviciosService.UpdateServicio(servicio);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un servicio, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<serviciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviciosService.DeleteServicio(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Servicios/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _serviciosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
