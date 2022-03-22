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
using System.Linq;
using System.Security.Claims;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class activoOperacionesController : ControllerBase
    {
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ItransportesService _transporteService;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IvehiculosService _vehiculosService;
        private readonly IequipoRemolqueService _equipoRemolqueService;

        public activoOperacionesController(IactivoOperacionesService activoOperacionesService, IMapper mapper, IPasswordService password,
                                           ItransportesService transporteService,
                                           IactivoMovimientosActualService activoMovimientosActualService,
                                           IvehiculosService vehiculosService,
                                           IequipoRemolqueService equipoRemolqueService)
        {
            _activoOperacionesService = activoOperacionesService;
            _mapper = mapper;
            _passwordService = password;
            _transporteService = transporteService;
            _activoMovimientosActualService = activoMovimientosActualService;
            _vehiculosService = vehiculosService;
            _equipoRemolqueService = equipoRemolqueService;
        }

        /// <summary>
        /// Obtiene los activos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoOperacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoOperaciones([FromQuery] activoOperacionesQueryFilter filter)
        {
            var activoOperaciones = await _activoOperacionesService.GetActivoOperaciones(filter);
            var activoOperacionesDto = _mapper.Map<IEnumerable<activoOperacionesDto>>(activoOperaciones);

            foreach(var activo in activoOperacionesDto)
            {
                var currentTransporte = await _transporteService.GetTransporte(activo.idTransporte);
                activo.transporte = _mapper.Map<transportesDto>(currentTransporte);
            }

            var metadata = new Metadata
            {
                TotalCount = activoOperaciones.TotalCount,
                PageSize = activoOperaciones.PageSize,
                CurrentPage = activoOperaciones.CurrentPage,
                TotalPages = activoOperaciones.TotalPages,
                HasNextPage = activoOperaciones.HasNextPage,
                HasPreviousPage = activoOperaciones.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<activoOperacionesDto>>(activoOperacionesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un activo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoOperacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoOperacion(int id)
        {
            var activoOperaciones = await _activoOperacionesService.GetActivoOperacion(id);
            var activoOperacionesDto = _mapper.Map<activoOperacionesDto>(activoOperaciones);

            var response = new AguilaResponse<activoOperacionesDto>(activoOperacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene los datos de un activo de operacion con su estado y ubicación actual
        /// El metodo incluye en su respuesta, nombre del piloto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="empresaId"></param>
        /// <returns></returns>
        [HttpGet("{codigo}/{empresaId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoOperacionCodigo(string codigo, int empresaId)
        {
            var activoOperaciones =  _activoMovimientosActualService.GetActivoMovimientoActualByCodigo(codigo, empresaId);

            if (activoOperaciones == null)
            {
                throw new AguilaException("El código del activo no es valido, ingrese uno existente", 400);
            }

            var activoOperacionesDto = _mapper.Map<activoMovimientosActualDto>(activoOperaciones);

            if (activoOperacionesDto.activoOperacion.categoria.ToLower() == "v")
            {
                var xVehiculo = await  _vehiculosService.GetVehiculo(activoOperacionesDto.idActivo);
                activoOperacionesDto.activoOperacion.placa = xVehiculo.placa;
            }

            if (activoOperacionesDto.activoOperacion.categoria.ToLower() == "e")
            {
                var xEquipo = await _equipoRemolqueService.GetEquipoRemolque(activoOperacionesDto.idActivo);
                activoOperacionesDto.activoOperacion.placa = xEquipo.placa;
            }

            var response = new AguilaResponse<activoMovimientosActualDto>(activoOperacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo activo
        /// </summary>
        /// <param name="activoOperacionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoOperacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoOperacionesDto activoOperacionDto)
        {
            var activoOperacion = _mapper.Map<activoOperaciones>(activoOperacionDto);
            await _activoOperacionesService.InsertActivoOperacion(activoOperacion);

            activoOperacionDto = _mapper.Map<activoOperacionesDto>(activoOperacion);
            var response = new AguilaResponse<activoOperacionesDto>(activoOperacionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un activo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activoOperacionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoOperacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, activoOperacionesDto activoOperacionDto)
        {
            var activoOperacion = _mapper.Map<activoOperaciones>(activoOperacionDto);
            activoOperacion.id = id;

            var result = await _activoOperacionesService.UpdateActivoOperacion(activoOperacion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un activo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoOperacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activoOperacionesService.DeleteActivoOperacion(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/ActivoOperaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _activoOperacionesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
