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
    public class condicionGenSetController : ControllerBase
    {
        private readonly IcondicionGenSetService _condicionGenSetService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionGenSetController(IcondicionGenSetService condicionGenSetService, IMapper mapper, IPasswordService passwordService,
                                         IestadosService estadosService, IImagenesRecursosService imagenesRecursosService)
        {
            _condicionGenSetService = condicionGenSetService;
            _mapper = mapper;
            _passwordService = passwordService;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las condiciones de los generadores registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionGenSet([FromQuery] condicionGenSetQueryFilter filter)
        {
            var condicionGenSet = await _condicionGenSetService.GetCondicionGenSet(filter);
            var condicionGenSetDto = _mapper.Map<IEnumerable<condicionGenSetDto>>(condicionGenSet);

            var metadata = new Metadata
            {
                TotalCount = condicionGenSet.TotalCount,
                PageSize = condicionGenSet.PageSize,
                CurrentPage = condicionGenSet.CurrentPage,
                TotalPages = condicionGenSet.TotalPages,
                HasNextPage = condicionGenSet.HasNextPage,
                HasPreviousPage = condicionGenSet.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionGenSetDto>>(condicionGenSetDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de los generadores por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionGenSet(long idCondicion)
        {
            var condicionGenSet = await _condicionGenSetService.GetCondicionGenSet(idCondicion);

            if (condicionGenSet == null)
            {
                throw new AguilaException("Condicion No Existente", 404);
            }

            var condicionGenSetDto = _mapper.Map<condicionGenSetDto>(condicionGenSet);

            var response = new AguilaResponse<condicionGenSetDto>(condicionGenSetDto);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la ultima condicion del activo solicitado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("ultima/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetGenSet(int idActivo)
        {
            var condicionGenSet = _condicionGenSetService.ultima(idActivo);

            if (condicionGenSet == null)
            {
                throw new AguilaException("No hay ninguna condicion", 404);
            }

            var condicionGenSetDto = _mapper.Map<condicionGenSetDto>(condicionGenSet);

            var response = new AguilaResponse<condicionGenSetDto>(condicionGenSetDto);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el listado de estados para los generadores tanto tecnica como de estructura
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("estados/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoEstadoCondicion(int idEmpresa)
        {
            var xEventosCondicionGenerador = new List<string>
            {
                ControlActivosEventos.CondicionGeneradorEstructura.ToString(),
                ControlActivosEventos.CondicionGeneradorTecnica.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionGenerador);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de generador
        /// </summary>
        /// <param name="condicionGenSetDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionGenSetDto condicionGenSetDto)
        {
            var xCondicionGenSetDto = await _condicionGenSetService.InsertCondicionGenSet(condicionGenSetDto);

            var response = new AguilaResponse<condicionGenSetDto>(xCondicionGenSetDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion de generador, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionGenSetDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionGenSetDto condicionGenSetDto)
        {
            var condicionGenSet = _mapper.Map<condicionGenSet>(condicionGenSetDto);
            //condicionEquipo.id = id;

            var result = await _condicionGenSetService.UpdateCondicionGenSet(condicionGenSet);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condicion de generador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionGenSetService.DeleteCondicionGenSet(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/CondicionGenSet/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _condicionGenSetService.GetRecursoByControlador(controlador);
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
        public IActionResult GetCondicionesGeneradores([FromQuery] reporteCondicionesGeneradoresQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            //filter.global = true;

            var condiciones = _condicionGenSetService.reporteCondicionesGeneradores(filter, usuario);
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
