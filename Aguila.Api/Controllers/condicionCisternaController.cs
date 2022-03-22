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
using Aguila.Core.Enumeraciones;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class condicionCisternaController : ControllerBase
    {
        private readonly IcondicionCisternaService _condicionCisternaService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IestadosService _estadosService;

        public condicionCisternaController(IcondicionCisternaService condicionCisternaService, IMapper mapper, IPasswordService passwordService,
                                           IestadosService estadosService)
        {
            _condicionCisternaService = condicionCisternaService;
            _mapper = mapper;
            _passwordService = passwordService;
            _estadosService = estadosService;
        }

        /// <summary>
        /// Obtiene las condiciones de cisterna registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCisternaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionCisterna([FromQuery] condicionCisternaQueryFilter filter)
        {
            var condicionCisterna = await _condicionCisternaService.GetCondicionCisterna(filter);
            var condicionCisternaDto = _mapper.Map<IEnumerable<condicionCisternaDto>>(condicionCisterna);

            var metadata = new Metadata
            {
                TotalCount = condicionCisterna.TotalCount,
                PageSize = condicionCisterna.PageSize,
                CurrentPage = condicionCisterna.CurrentPage,
                TotalPages = condicionCisterna.TotalPages,
                HasNextPage = condicionCisterna.HasNextPage,
                HasPreviousPage = condicionCisterna.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionCisternaDto>>(condicionCisternaDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de las cisterna por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCisternaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionCisterna(long idCondicion)
        {
            var condicionCisterna = await _condicionCisternaService.GetCondicionCisterna(idCondicion);

            if (condicionCisterna == null)
            {
                throw new AguilaException("Condicion No Existente", 404);
            }

            var condicionCisternaDto = _mapper.Map<condicionCisternaDto>(condicionCisterna);
            _condicionCisternaService.llenarCondicionLlantas(condicionCisterna, ref condicionCisternaDto);

            var response = new AguilaResponse<condicionCisternaDto>(condicionCisternaDto);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la ultima condicion del activo solicitado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("ultima/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCisternaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCabezal(int idActivo)
        {
            var condicionCisterna = _condicionCisternaService.ultima(idActivo);

            if (condicionCisterna == null)
            {
                throw new AguilaException("No hay ninguna condicion", 404);
            }

            var condicionCisternaDto = _mapper.Map<condicionCisternaDto>(condicionCisterna);
            _condicionCisternaService.llenarCondicionLlantas(condicionCisterna, ref condicionCisternaDto);

            var response = new AguilaResponse<condicionCisternaDto>(condicionCisternaDto);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el listado de estados para la condicion tanto de ingreso como salida
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("estados/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoEstadoCondicion(int idEmpresa)
        {
            var xEventosCondicionCisterna = new List<string>
            {
                ControlActivosEventos.CondicionIngreso.ToString(),
                ControlActivosEventos.CondicionSalida.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionCisterna);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de cisterna
        /// </summary>
        /// <param name="condicionCisternaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCisternaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionCisternaDto condicionCisternaDto)
        {
            var xCondicionCisternaDto = await _condicionCisternaService.InsertCondicionCisterna(condicionCisternaDto);

            var response = new AguilaResponse<condicionCisternaDto>(xCondicionCisternaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion de cisterna, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionCisternaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCisternaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionCisternaDto condicionCisternaDto)
        {
            var condicionCisterna = _mapper.Map<condicionCisterna>(condicionCisternaDto);
            //condicionCisterna.id = id;

            var result = await _condicionCisternaService.UpdateCondicionCisterna(condicionCisterna);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condicion de cisterna
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCisternaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionCisternaService.DeleteCondicionCisterna(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/CondicionCisterna/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _condicionCisternaService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }


}
