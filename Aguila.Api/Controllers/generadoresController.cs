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
using System.Security.Claims;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class generadoresController : ControllerBase
    {
        private readonly IgeneradoresService _generadoresService;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly ItipoGeneradoresService _tipoGeneradoresService;
        private readonly ItransportesService _transportesService;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IestadosService _estadosService;
        private readonly IempleadosService _empleadosService;
        private readonly IserviciosService _serviciosService;
        private readonly IrutasService _rutasService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public generadoresController(IgeneradoresService generadoresService, IMapper mapper, IPasswordService password,
                                     IactivoOperacionesService activoOperacionesService,
                                     ItipoGeneradoresService tipoGeneradoresService,
                                     ItransportesService transportesService,
                                     IactivoMovimientosActualService activoMovimientosActualService,
                                     IestadosService estadosService,
                                     IempleadosService empleadosService,
                                     IserviciosService serviciosService,
                                     IrutasService rutasService)
        {
            _generadoresService = generadoresService;
            _activoOperacionesService = activoOperacionesService;
            _tipoGeneradoresService = tipoGeneradoresService;
            _transportesService = transportesService;
            _activoMovimientosActualService = activoMovimientosActualService;
            _estadosService = estadosService;
            _empleadosService = empleadosService;
            _serviciosService = serviciosService;
            _rutasService = rutasService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los generadores registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<generadoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetGeneradores([FromQuery] generadoresQueryFilter filter)
        {
            var generadores = _generadoresService.GetGeneradores(filter);
            var generadoresDto = _mapper.Map<IEnumerable<generadoresDto>>(generadores);

            //foreach (var generador in generadoresDto)
            //{
            //    var currentActivoOperacion = await _activoOperacionesService.GetActivoOperacion(generador.idActivo);
            //    generador.activoOperacion = _mapper.Map<activoOperacionesDto>(currentActivoOperacion);

            //    var currentTransporte = await _transportesService.GetTransporte(currentActivoOperacion.idTransporte);
            //    generador.activoOperacion.transporte = _mapper.Map<transportesDto>(currentTransporte);

            //    var currentActivoMovimiento = await _activoMovimientosActualService.GetActivoMovimientoActual(generador.idActivo);
            //    var currentActivoMovimientoDto = _mapper.Map<activoMovimientosActualDto>(currentActivoMovimiento);

            //    //Get de objeto de activos
            //    var estado = await _estadosService.GetEstado(currentActivoMovimientoDto.idEstado);
            //    var empleado = await _empleadosService.GetEmpleado(currentActivoMovimientoDto.idPiloto);
            //    var servicio = await _serviciosService.GetServicio(currentActivoMovimientoDto.idServicio);
            //    var ruta = await _rutasService.GetRuta(currentActivoMovimientoDto.idRuta);

            //    //Set de objetos de activos
            //    generador.vEstado = estado.nombre;
            //    generador.vEmpleadoNombre = empleado.nombres;
            //    generador.vEmpleadoCodigo = empleado.codigo;
            //    generador.vServicio = servicio.nombre;
            //    generador.vRuta = ruta.nombre;
            //}

            var metadata = new Metadata
            {
                TotalCount = generadores.TotalCount,
                PageSize = generadores.PageSize,
                CurrentPage = generadores.CurrentPage,
                TotalPages = generadores.TotalPages,
                HasNextPage = generadores.HasNextPage,
                HasPreviousPage = generadores.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<generadoresDto>>(generadoresDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un generador por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<generadoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGenerador(int id)
        {
            var generadores = await _generadoresService.GetGenerador(id);

            if (generadores == null)
            {
                throw new AguilaException("Generador No Existente", 404);
            }

            var generadoresDto = _mapper.Map<generadoresDto>(generadores);
            var currentActivoOperacion = await _activoOperacionesService.GetActivoOperacion(generadoresDto.idActivo);
            generadoresDto.activoOperacion = _mapper.Map<activoOperacionesDto>(currentActivoOperacion);

            var response = new AguilaResponse<generadoresDto>(generadoresDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo generador
        /// </summary>
        /// <param name="generadorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<generadoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(generadoresDto generadorDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            var result = await _generadoresService.InsertGenerador(generadorDto, usuario);                    
            return Ok(result);
        }

        /// <summary>
        /// Actualiza un generador, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="generadorDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<generadoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, generadoresDto generadorDto)
        {
            
            generadorDto.idActivo = id;
            var result = await _generadoresService.UpdateGenerador(generadorDto);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un generador, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<generadoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _generadoresService.DeleteGenerador(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Generadores/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _generadoresService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
