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
    public class condicionCabezalController : ControllerBase
    {
        private readonly IcondicionCabezalService _condicionCabezalService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService; 

        public condicionCabezalController(IcondicionCabezalService condicionCabezalService, IMapper mapper, IPasswordService passwordService,
                                          IestadosService estadosService, IImagenesRecursosService imagenesRecursosService )
        {
            _condicionCabezalService = condicionCabezalService;
            _mapper = mapper;
            _passwordService = passwordService;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las condiciones de los cabezales registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCabezalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionCabezal([FromQuery] condicionCabezalQueryFilter filter)
        {
            var condicionCabezal = await  _condicionCabezalService.GetCondicionCabezal(filter);
            var condicionCabezalDto = _mapper.Map<IEnumerable<condicionCabezalDto>>(condicionCabezal);

            var metadata = new Metadata
            {
                TotalCount = condicionCabezal.TotalCount,
                PageSize = condicionCabezal.PageSize,
                CurrentPage = condicionCabezal.CurrentPage,
                TotalPages = condicionCabezal.TotalPages,
                HasNextPage = condicionCabezal.HasNextPage,
                HasPreviousPage = condicionCabezal.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionCabezalDto>>(condicionCabezalDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de los cabezales por id
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCabezalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionCabezal(long idCondicion)
        {
            var condicionCabezal = await  _condicionCabezalService.GetCondicionCabezal(idCondicion);

            if (condicionCabezal == null)
            {
                throw new AguilaException("Condicion No Existente", 404);
            }

            var condicionCabezalDto = _mapper.Map<condicionCabezalDto>(condicionCabezal);
            _condicionCabezalService.llenarCondicionLlantas(condicionCabezal, ref condicionCabezalDto);

            var response = new AguilaResponse<condicionCabezalDto>(condicionCabezalDto);

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
            var condicionCabezal = _condicionCabezalService.ultima(idActivo);

            if (condicionCabezal == null)
            {
                throw new AguilaException("No hay ninguna condicion", 404);
            }

            var condicionCabezalDto = _mapper.Map<condicionCabezalDto>(condicionCabezal);
            _condicionCabezalService.llenarCondicionLlantas(condicionCabezal, ref condicionCabezalDto);

            var response = new AguilaResponse<condicionCabezalDto>(condicionCabezalDto);
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
                ControlActivosEventos.CondicionIngreso.ToString(),
                ControlActivosEventos.CondicionSalida.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionVehiculo);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de cabezal
        /// </summary>
        /// <param name="condicionCabezalDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCabezalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionCabezalDto condicionCabezalDto)
        {
            var xCondicionCabezalDto = await _condicionCabezalService.InsertCondicionCabezal(condicionCabezalDto);

            var response = new AguilaResponse<condicionCabezalDto>(xCondicionCabezalDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condición de cabezal, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionCabezalDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCabezalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionCabezalDto condicionCabezalDto)
        {
            var condicionCabezal = _mapper.Map<condicionCabezal>(condicionCabezalDto);
            //condicionCabezal.id = id;

            var result = await _condicionCabezalService.UpdateCondicionCabezal(condicionCabezal);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condición de cabezal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionCabezalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionCabezalService.DeleteCondicionCabezal(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/CondicionCabezal/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _condicionCabezalService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }


        /// <summary>
        /// Reporte condiciones de Cabezales
        /// El filtro de empresa es obligatorio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteCondiciones")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCondicionesVehiculos([FromQuery] reporteCondicionesCabezalesQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            //filter.global = true;

            var condiciones = _condicionCabezalService.reporteCondicionesVehiculos(filter, usuario);
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
