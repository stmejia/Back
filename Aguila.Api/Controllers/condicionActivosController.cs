using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class condicionActivosController : ControllerBase
    {
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly IEstacionesTrabajoService _estacionesTrabajoService;
        private readonly IempleadosService _empleadosService;
        private readonly IUsuariosService _usuariosService;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;


        public condicionActivosController(IcondicionActivosService condicionActivosService, IMapper mapper, IPasswordService passwordService,
                                          IactivoOperacionesService activoOperacionesService,
                                          IEstacionesTrabajoService estacionesTrabajoService,
                                          IempleadosService empleadosService,
                                          IUsuariosService usuariosService,
                                          IestadosService estadosService,
                                          IImagenesRecursosService imagenesRecursosService)
        {
            _condicionActivosService = condicionActivosService;
            _activoOperacionesService = activoOperacionesService;
            _estacionesTrabajoService = estacionesTrabajoService;
            _usuariosService = usuariosService;
            _empleadosService = empleadosService;
            _estadosService = estadosService;
            _mapper = mapper;
            _passwordService = passwordService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las condiciones registradas, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetACondiciones([FromQuery] condicionActivosQueryFilter filter)
        {
            var condiciones = _condicionActivosService.GetCondiciones(filter);
            var condicionesDto = _mapper.Map<IEnumerable<condicionActivosDto>>(condiciones);

            var metadata = new Metadata
            {
                TotalCount = condiciones.TotalCount,
                PageSize = condiciones.PageSize,
                CurrentPage = condiciones.CurrentPage,
                TotalPages = condiciones.TotalPages,
                HasNextPage = condiciones.HasNextPage,
                HasPreviousPage = condiciones.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<condicionActivosDto>>(condicionesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicion(string id)
        {                     
            var condicion = await _condicionActivosService.GetCodicion(id);
            //var condicionDto = _mapper.Map<condicionActivosDto>(condicion);

            var response = new AguilaResponse<JObject>(condicion);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene los estados para vehiculos
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        //[HttpGet("activosEstadosCondicion/{idEmpresa}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public IActionResult GetActivoEstadoCondicion(int idEmpresa)
        //{
        //    var xEventosCondicionVehiculo = new List<string>
        //    {
        //        ControlActivosEventos.CondicionIngreso.ToString(),
        //        ControlActivosEventos.CondicionSalida.ToString(),
        //        ControlActivosEventos.Reparado.ToString(),
        //        ControlActivosEventos.Asignado.ToString(),
        //        ControlActivosEventos.Reservado.ToString(),
        //        ControlActivosEventos.Egresado.ToString(),
        //        ControlActivosEventos.Bodega.ToString(),
        //        ControlActivosEventos.RentaInterna.ToString(),
        //        ControlActivosEventos.RentaExterna.ToString()
        //    };

        //    var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionVehiculo);

        //    var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
        //    var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

        //    return Ok(response);
        //}

        /// <summary>
        /// Crea una nueva condicion
        /// </summary>
        /// <param name="condicionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionActivosDto condicionDto)
        {
            //var condicion = _mapper.Map<condicionActivos>(condicionDto);
            await _condicionActivosService.InsertCondicion(condicionDto, User.Identity.Name);

            
            //condicionDto = _mapper.Map<condicionActivosDto>(condicion);
            var response = new AguilaResponse<condicionActivosDto>(condicionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionActivosDto condicionDto)
        {
            var condicion = _mapper.Map<condicionActivos>(condicionDto);
            condicion.id = id;

            var result = await _condicionActivosService.UpdateCondicion(condicion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condicion, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionActivosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionActivosService.DeleteCondicion(id);
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
            var recurso = await _condicionActivosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
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
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);

            var response = new AguilaResponse<ImagenRecursoConfiguracion>(imgRecConf);
            return Ok(response);
        }





    }
    
}

