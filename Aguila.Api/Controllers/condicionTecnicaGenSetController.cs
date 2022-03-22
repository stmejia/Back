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
    public class condicionTecnicaGenSetController : ControllerBase
    {
        private readonly IcondicionTecnicaGenSetService _condicionTecnicaGenSetService;
        private readonly IMapper _mapper;
        private readonly IestadosService _estadosService;
        private readonly IPasswordService _passwordService;

        public condicionTecnicaGenSetController(IcondicionTecnicaGenSetService condicionTecnicaGenSetService, IMapper mapper, IPasswordService passwordService,
                                                IestadosService estadosService)
        {
            _condicionTecnicaGenSetService = condicionTecnicaGenSetService;
            _mapper = mapper;
            _passwordService = passwordService;
            _estadosService = estadosService;
        }

        /// <summary>
        /// Obtiene las condiciones tecnicas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTecnicaGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionTecnicaGenSet([FromQuery] condicionTecnicaGenSetQueryFilter filter)
        {
            var condicionTecnicaGenSet = await _condicionTecnicaGenSetService.GetCondicionTecnicaGenSet(filter);
            var condicionTecnicaGenSetDto = _mapper.Map<IEnumerable<condicionTecnicaGenSetDto>>(condicionTecnicaGenSet);

            var metadata = new Metadata
            {
                TotalCount = condicionTecnicaGenSet.TotalCount,
                PageSize = condicionTecnicaGenSet.PageSize,
                CurrentPage = condicionTecnicaGenSet.CurrentPage,
                TotalPages = condicionTecnicaGenSet.TotalPages,
                HasNextPage = condicionTecnicaGenSet.HasNextPage,
                HasPreviousPage = condicionTecnicaGenSet.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionTecnicaGenSetDto>>(condicionTecnicaGenSetDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene las condiciones tecnicas por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTecnicaGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionTecnicaGenSet(long idCondicion)
        {
            var condicionTecnicaGenSet = await  _condicionTecnicaGenSetService.GetCondicionTecnicaGenSet(idCondicion);

            if (condicionTecnicaGenSet == null)
            {
                throw new AguilaException("Condicion Tecnica No Existente", 404);
            }

            var condicionTecnicaGenSetDto = _mapper.Map<condicionTecnicaGenSetDto>(condicionTecnicaGenSet);

            var response = new AguilaResponse<condicionTecnicaGenSetDto>(condicionTecnicaGenSetDto);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la ultima condicion del activo solicitado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("ultima/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCabezalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCabezal(int idActivo)
        {
            var condicionTecnicaGenSet = _condicionTecnicaGenSetService.ultima(idActivo);

            if (condicionTecnicaGenSet == null)
            {
                throw new AguilaException("No hay ninguna condicion", 404);
            }

            var condicionTecnicaGenSetDto = _mapper.Map<condicionTecnicaGenSetDto>(condicionTecnicaGenSet);

            var response = new AguilaResponse<condicionTecnicaGenSetDto>(condicionTecnicaGenSetDto);
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
            var xEventosCondicionVehiculo = new List<string>
            {
                ControlActivosEventos.CondicionGeneradorEstructura.ToString(),
                ControlActivosEventos.CondicionGeneradorTecnica.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionVehiculo);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion tecnica
        /// </summary>
        /// <param name="condicionTecnicaGenSetDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTecnicaGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionTecnicaGenSetDto condicionTecnicaGenSetDto)
        {
            //var condicionEquipo = _mapper.Map<condicionEquipo>(condicionEquipoDto);
            await _condicionTecnicaGenSetService.InsertCondicionTecnicaGenSet(condicionTecnicaGenSetDto);

            //condicionEquipoDto = _mapper.Map<condicionEquipoDto>(condicionEquipo);
            var response = new AguilaResponse<condicionTecnicaGenSetDto>(condicionTecnicaGenSetDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion tecnica, eviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionTecnicaGenSetDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTecnicaGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionTecnicaGenSetDto condicionTecnicaGenSetDto)
        {
            var condicionTecnicaGenSet = _mapper.Map<condicionTecnicaGenSet>(condicionTecnicaGenSetDto);
            //condicionEquipo.id = id;

            var result = await _condicionTecnicaGenSetService.UpdateCondicionTecnicaGenSet(condicionTecnicaGenSet);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condicion tecnica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionTecnicaGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionTecnicaGenSetService.DeleteCondicionTecnicaGenSet(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/CondicionTecnicaGenSet/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _condicionTecnicaGenSetService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
