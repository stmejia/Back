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
using System.Security.Claims;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class condicionEquipoController : ControllerBase
    {
        private readonly IcondicionEquipoService _condicionEquipoService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionEquipoController(IcondicionEquipoService condicionEquipoService, IMapper mapper, IPasswordService passwordService,
                                         IestadosService estadosService, IImagenesRecursosService imagenesRecursosService)
        {
            _condicionEquipoService = condicionEquipoService;
            _mapper = mapper;
            _passwordService = passwordService;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las condiciones de equipo registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionEquipo([FromQuery] condicionEquipoQueryFilter filter)
        {
            var condicionEquipo = await _condicionEquipoService.GetCondicionEquipo(filter);
            var condicionEquipoDto = _mapper.Map<IEnumerable<condicionEquipoDto>>(condicionEquipo);

            var metadata = new Metadata
            {
                TotalCount = condicionEquipo.TotalCount,
                PageSize = condicionEquipo.PageSize,
                CurrentPage = condicionEquipo.CurrentPage,
                TotalPages = condicionEquipo.TotalPages,
                HasNextPage = condicionEquipo.HasNextPage,
                HasPreviousPage = condicionEquipo.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionEquipoDto>>(condicionEquipoDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de equipo por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionEquipo(int idCondicion)
        {
            var condicionEquipo = await _condicionEquipoService.GetCondicionEquipo(idCondicion);

            if (condicionEquipo == null)
            {
                throw new AguilaException("Condicion No Existente", 404);
            }

            var condicionEquipoDto = _mapper.Map<condicionEquipoDto>(condicionEquipo);
            _condicionEquipoService.llenarCondicionLlantas(condicionEquipo, ref condicionEquipoDto);

            var response = new AguilaResponse<condicionEquipoDto>(condicionEquipoDto);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la ultima condicion del activo solicitado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("ultima/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEquipo(int idActivo)
        {
            var condicionEquipo = _condicionEquipoService.ultima(idActivo);

            if (condicionEquipo == null)
            {
                throw new AguilaException("No hay ninguna condicion", 404);
            }

            var condicionEquipoDto = _mapper.Map<condicionEquipoDto>(condicionEquipo);
            _condicionEquipoService.llenarCondicionLlantas(condicionEquipo, ref condicionEquipoDto);

            var response = new AguilaResponse<condicionEquipoDto>(condicionEquipoDto);
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
            var xEventosCondicionEquipo = new List<string>
            {
                ControlActivosEventos.CondicionIngreso.ToString(),
                ControlActivosEventos.CondicionSalida.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionEquipo);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de equipo
        /// </summary>
        /// <param name="condicionEquipoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionEquipoDto condicionEquipoDto)
        {
            var xCondicionEquipoDto = await _condicionEquipoService.InsertCondicionEquipo(condicionEquipoDto);

            var response = new AguilaResponse<condicionEquipoDto>(xCondicionEquipoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion de equipo, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionEquipoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionEquipoDto condicionEquipoDto)
        {
            var condicionEquipo = _mapper.Map<condicionEquipo>(condicionEquipoDto);
            //condicionEquipo.id = id;

            var result = await _condicionEquipoService.UpdateCondicionEquipo(condicionEquipo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condicion de equipo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionEquipoService.DeleteCondicionEquipo(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/CondicionEquipo/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _condicionEquipoService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Reporte condiciones de Equipo
        /// El filtro de empresa es obligatorio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteCondiciones")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCondicionesVehiculos([FromQuery] reporteCondicionesEquipoQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            //filter.global = true;

            var condiciones = _condicionEquipoService.reporteCondicionesEquipos(filter, usuario);
            var condicionesDto = _mapper.Map<IEnumerable<condicionActivosDto>>(condiciones);

            var response = new AguilaResponse<IEnumerable<condicionActivosDto>>(condicionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve la configuracion de imagen para una propiedad del recurso, propiedades disponibles ImagenFirmaPiloto , Fotos   
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpGet("ImagenConfiguracion/{propiedad}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ImagenRecursoConfiguracion>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImagenConfiguracion(string propiedad)
        {
            //var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var controlador = "condicionActivos";
            var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);

            var response = new AguilaResponse<ImagenRecursoConfiguracion>(imgRecConf);
            return Ok(response);
        }
    }
}
