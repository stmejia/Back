using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class condicionTallerVehiculoController : ControllerBase
    {
        private readonly IcondicionTallerVehiculoService _condicionTallerVehiculoService;
        private readonly IMapper _mapper;

        public condicionTallerVehiculoController(IcondicionTallerVehiculoService condicionTallerVehiculoService, IMapper mapper)
        {
            _condicionTallerVehiculoService = condicionTallerVehiculoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene las condiciones de taller registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTallerVehiculoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionTallerVehiculo([FromQuery] condicionTallerVehiculoQueryFilter filter)
        {
            var condicionTaller = await _condicionTallerVehiculoService.GetCondicionTallerVehiculo(filter);
            var condicionTallerDto = _mapper.Map<IEnumerable<condicionTallerVehiculoDto>>(condicionTaller);

            var metadata = new Metadata
            {
                TotalCount = condicionTaller.TotalCount,
                PageSize = condicionTaller.PageSize,
                CurrentPage = condicionTaller.CurrentPage,
                TotalPages = condicionTaller.TotalPages,
                HasNextPage = condicionTaller.HasNextPage,
                HasPreviousPage = condicionTaller.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionTallerVehiculoDto>>(condicionTallerDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de los vehiculos en taller por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTallerVehiculoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionTallerVehiculo(int idCondicion)
        {
            var condicionTaller = await _condicionTallerVehiculoService.GetCondicionTallerVehiculo(idCondicion);

            if (condicionTaller == null)
                throw new AguilaException("Condicion No Existente");

            var condicionTallerDto = _mapper.Map<condicionTallerVehiculoDto>(condicionTaller);
            var response = new AguilaResponse<condicionTallerVehiculoDto>(condicionTallerDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de taller
        /// </summary>
        /// <param name="condicionTallerVehiculoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTallerVehiculoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionTallerVehiculoDto condicionTallerVehiculoDto)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            var condicionTaller = _mapper.Map<condicionTallerVehiculo>(condicionTallerVehiculoDto);
            condicionTaller.idUsuario = usuarioId;

            await _condicionTallerVehiculoService.InsertCondicionTallerVehiculo(condicionTaller);
            condicionTallerVehiculoDto = _mapper.Map<condicionTallerVehiculoDto>(condicionTaller);

            var response = new AguilaResponse<condicionTallerVehiculoDto>(condicionTallerVehiculoDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion de taller, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionTallerVehiculoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTallerVehiculoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionTallerVehiculoDto condicionTallerVehiculoDto)
        {
            var condicionTaller = _mapper.Map<condicionTallerVehiculo>(condicionTallerVehiculoDto);

            var result = await _condicionTallerVehiculoService.UpdateCondicionTallerVehiculo(condicionTaller);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condición de taller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTallerVehiculoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionTallerVehiculoService.DeleteCondicionTallerVehiculo(id);
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
            var recurso = await _condicionTallerVehiculoService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
