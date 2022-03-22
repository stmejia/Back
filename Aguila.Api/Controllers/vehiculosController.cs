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
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class vehiculosController : ControllerBase
    {
        private readonly IvehiculosService _vehiculosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IestadosService _estadosService;
        private readonly IempleadosService _empleadosService;
        private readonly IserviciosService _serviciosService;
        private readonly IrutasService _rutasService;
        private readonly ItransportesService _transportesService;
        private readonly IUsuariosService _usuariosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public vehiculosController(IvehiculosService vehiculosService, IMapper mapper, IPasswordService password,
                                   IactivoOperacionesService activoOperacionesService,
                                   IactivoMovimientosActualService activoMovimientosActualService,
                                   IestadosService estadosService,
                                   IempleadosService empleadosService,
                                   IserviciosService serviciosService,
                                   IrutasService rutasService,
                                   ItransportesService transportesService,
                                   IUsuariosService usuariosService,
                                   IImagenesRecursosService imagenesRecursosService)
        {
            _vehiculosService = vehiculosService;
            _mapper = mapper;
            _passwordService = password;
            _activoOperacionesService = activoOperacionesService;
            _activoMovimientosActualService = activoMovimientosActualService;
            _estadosService = estadosService;
            _empleadosService = empleadosService;
            _serviciosService = serviciosService;
            _rutasService = rutasService;
            _transportesService = transportesService;
            _usuariosService = usuariosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene los vehiculos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVehiculos([FromQuery] vehiculosQueryFilter filter)
        {
            var vehiculos = await _vehiculosService.GetVehiculos(filter);
            var vehiculosDto = _mapper.Map<IEnumerable<vehiculosDto>>(vehiculos);
            
            var metadata = new Metadata
            {
                TotalCount = vehiculos.TotalCount,
                PageSize = vehiculos.PageSize,
                CurrentPage = vehiculos.CurrentPage,
                TotalPages = vehiculos.TotalPages,
                HasNextPage = vehiculos.HasNextPage,
                HasPreviousPage = vehiculos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<vehiculosDto>>(vehiculosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un vehiculo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVehiculo(int id)
        {
            var vehiculos = await  _vehiculosService.GetVehiculo(id);
            if (vehiculos == null)
            {
                throw new AguilaException("Vehiculo NO Existente! revise sus datos.",404);
            }

            var vehiculosDto = _mapper.Map<vehiculosDto>(vehiculos);

            var currentActivoOperacion = await _activoOperacionesService.GetActivoOperacion(vehiculosDto.idActivo);
            vehiculosDto.activoOperacion = _mapper.Map<activoOperacionesDto>(currentActivoOperacion);


            var response = new AguilaResponse<vehiculosDto>(vehiculosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo vehículo
        /// </summary>
        /// <param name="vehiculoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(vehiculosDto vehiculoDto)
        {
            //var accessToken = Request.Headers[HeaderNames.Authorization];
            //capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse( identity.FindFirst("UsuarioId").Value.ToString());

            //var vehiculo = _mapper.Map<vehiculos>(vehiculoDto);
            var result = await _vehiculosService.InsertVehiculo(vehiculoDto, usuario);
            

            //vehiculoDto = _mapper.Map<vehiculosDto>(vehiculo);
            var response = new AguilaResponse<vehiculosDto>(result);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un vehiculo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehiculoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, vehiculosDto vehiculoDto)
        {
            //var vehiculo = _mapper.Map<vehiculos>(vehiculoDto);

            vehiculoDto.idActivo = id;
            var result = await _vehiculosService.UpdateVehiculo(vehiculoDto);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Elimina un vehiculo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _vehiculosService.DeleteVehiculo(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Vehiculos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _vehiculosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve la configuracion de imagen para una propiedad del recurso, imagenTarjetaCirculacion
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
