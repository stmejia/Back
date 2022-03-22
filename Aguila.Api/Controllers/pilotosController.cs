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
    public class pilotosController : ControllerBase
    {
        private readonly IpilotosService _pilotosService;
        private readonly IpilotosTiposService _pilotosTiposService;
        private readonly IempleadosService _empleadosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public pilotosController(IpilotosService pilotosService, IMapper mapper, IPasswordService passwordService,
                                 IpilotosTiposService pilotosTiposService,
                                 IempleadosService empleadosService)
        {
            _pilotosService = pilotosService;
            _pilotosTiposService = pilotosTiposService;
            _empleadosService = empleadosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los pilotos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPilotos([FromQuery] pilotosQueryFilter filter)
        {
            var pilotos = _pilotosService.GetPilotos(filter);
            var pilotosDto = _mapper.Map<IEnumerable<pilotosDto>>(pilotos);

            foreach (var piloto in pilotosDto)
            {
                //Get de objetos
                var empleados = await _empleadosService.GetEmpleado(piloto.idEmpleado);
                var tipoPiloto = await _pilotosTiposService.GetPilotoTipo(piloto.idTipoPilotos);
                var tipoPilotoDto = _mapper.Map<pilotosTiposDto>(tipoPiloto);

                //Set de objetos
                piloto.vNombreEmpleado = empleados.nombres;
                piloto.tipoPiloto = tipoPilotoDto;
            }

            var metadata = new Metadata
            {
                TotalCount = pilotos.TotalCount,
                PageSize = pilotos.PageSize,
                CurrentPage = pilotos.CurrentPage,
                TotalPages = pilotos.TotalPages,
                HasNextPage = pilotos.HasNextPage,
                HasPreviousPage = pilotos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<pilotosDto>>(pilotosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un piloto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPiloto(int id)
        {
            var pilotos = await _pilotosService.GetPiloto(id);
            var pilotosDto = _mapper.Map<pilotosDto>(pilotos);

            var response = new AguilaResponse<pilotosDto>(pilotosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo piloto
        /// </summary>
        /// <param name="pilotoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(pilotosDto pilotoDto)
        {
            var piloto = _mapper.Map<pilotos>(pilotoDto);
            await _pilotosService.InsertPiloto(piloto);

            pilotoDto = _mapper.Map<pilotosDto>(piloto);
            var response = new AguilaResponse<pilotosDto>(pilotoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un piloto, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pilotoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, pilotosDto pilotoDto)
        {
            var piloto = _mapper.Map<pilotos>(pilotoDto);
            piloto.id = id;

            var result = await _pilotosService.UpdatePiloto(piloto);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un piloto, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pilotosService.DeletePiloto(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Pilotos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _pilotosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
