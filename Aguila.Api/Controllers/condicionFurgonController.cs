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
using System;
using Aguila.Core.Enumeraciones;
using System.Security.Claims;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class condicionFurgonController : ControllerBase
    {
        private readonly IcondicionFurgonService _condicionFurgonService;
        private readonly IMapper _mapper;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionFurgonController(IcondicionFurgonService condicionFurgonService, IMapper mapper, IestadosService estadosService,
                                         IImagenesRecursosService imagenesRecursosService)
        {
            _condicionFurgonService = condicionFurgonService;
            _mapper = mapper;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las condiciones de los furgones registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionFurgonDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionFurgon([FromQuery] condicionFurgonQueryFilter filter)
        {
            var condicionFurgon = await  _condicionFurgonService.GetCondicionFurgon(filter);
            var condicionFurgonDto = _mapper.Map<IEnumerable<condicionFurgonDto>>(condicionFurgon);

            var metadata = new Metadata
            {
                TotalCount = condicionFurgon.TotalCount,
                PageSize = condicionFurgon.PageSize,
                CurrentPage = condicionFurgon.CurrentPage,
                TotalPages = condicionFurgon.TotalPages,
                HasNextPage = condicionFurgon.HasNextPage,
                HasPreviousPage = condicionFurgon.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionFurgonDto>>(condicionFurgonDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de los furgones por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionFurgonDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionFurgonId(long idCondicion)
        {
            var condicionFurgon = await  _condicionFurgonService.GetCondicionFurgon(idCondicion);

            if (condicionFurgon == null)
            {
                throw new AguilaException("Condicion No Existente", 404);
            }

            var condicionFurgonDto = _mapper.Map<condicionFurgonDto>(condicionFurgon);
            _condicionFurgonService.llenarCondicionLlantas(condicionFurgon, ref condicionFurgonDto);

            var response = new AguilaResponse<condicionFurgonDto>(condicionFurgonDto);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la ultima condicion del activo solicitado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("ultima/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionFurgonDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetFurgon(int idActivo)
        {
            var condicionFurgon = _condicionFurgonService.ultima(idActivo);

            if (condicionFurgon == null)
            {
                throw new AguilaException("No hay ninguna condicion", 404);
            }

            var condicionFurgonDto = _mapper.Map<condicionFurgonDto>(condicionFurgon);
            _condicionFurgonService.llenarCondicionLlantas(condicionFurgon, ref condicionFurgonDto);

            var response = new AguilaResponse<condicionFurgonDto>(condicionFurgonDto);
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
            var xEventosCondicionFurgon = new List<string>
            {
                ControlActivosEventos.CondicionIngreso.ToString(),
                ControlActivosEventos.CondicionSalida.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionFurgon);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de furgon
        /// </summary>
        /// <param name="condicionFurgonDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionFurgonDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionFurgonDto condicionFurgonDto)
        {
            var xCondicionFurgonDto = await _condicionFurgonService.InsertCondicionFurgon(condicionFurgonDto);

            var response = new AguilaResponse<condicionFurgonDto>(xCondicionFurgonDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condición de furgon, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionFurgonDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionFurgonDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionFurgonDto condicionFurgonDto)
        {
            var condicionFurgon = _mapper.Map<condicionFurgon>(condicionFurgonDto);
            //condicionCabezal.id = id;

            var result = await _condicionFurgonService.UpdateCondicionFurgon(condicionFurgon);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }
        
        /// <summary>
        /// Elimina una condición de furgon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionFurgonDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionFurgonService.DeleteCondicionFurgon(id);
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
            var recurso = await _condicionFurgonService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }


        /// <summary>
        /// Reporte condiciones de Furgon
        /// El filtro de empresa es obligatorio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteCondiciones")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCondicionesVehiculos([FromQuery] reporteCondicionesFurgonesQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            //filter.global = true;

            var condiciones = _condicionFurgonService.reporteCondicionesFurgones(filter, usuario);
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
