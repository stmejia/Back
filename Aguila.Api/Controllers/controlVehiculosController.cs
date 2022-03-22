using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class controlVehiculosController : ControllerBase
    {
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IMapper _mapper;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IestadosService _estadosService;
        private readonly IvehiculosService _vehiculosService;

        public controlVehiculosController(IactivoMovimientosService activoMovimientosService, IMapper mapper,
              IactivoMovimientosActualService activoMovimientosActualService,
              IestadosService estadosService,
              IvehiculosService vehiculosService)
        {
            _activoMovimientosService = activoMovimientosService;
            _mapper = mapper;
            _activoMovimientosActualService = activoMovimientosActualService;
            _estadosService = estadosService;
            _vehiculosService = vehiculosService;
        }

        /// <summary>
        /// Obtiene una lista de vehiculos con su estado actual
        /// el parametro global se utiliza para poder visualizar todo el
        /// inventario en una sola ubicacion
        /// Si no se envia el parametro global es necesario enviar idEstacionTrabajo
        /// regresara una lista de los equipos que se encuentren unicamente en dicha estacion o en ruta
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetVehiculos([FromQuery] vehiculosQueryFilter filter)
        {
            var vehiculos = _activoMovimientosActualService.GetVehiculoEstadoActual(filter);
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
        /// Obtiene los datos de un vehiculo con su estado actual
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetVehiculo(int idActivo)
        {
            var xVehiculo = _activoMovimientosActualService.GetVehiculoByID(idActivo);
            var vehiculoEstadoDto = _mapper.Map<vehiculosDto>(xVehiculo);

            var response = new AguilaResponse<vehiculosDto>(vehiculoEstadoDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene los vehiculos que se encuentran en inventario con su estado actual
        /// Se omite el campo global
        /// El filtro de empresa es obligatorio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteInventario")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetInventarioEquipos([FromQuery] vehiculosQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            filter.global = true;

            var vehiculos = _activoMovimientosActualService.reporteInventarioVehiculos(filter, usuario);
            var vehiculosDto = _mapper.Map<IEnumerable<vehiculosDto>>(vehiculos);

            var response = new AguilaResponse<IEnumerable<vehiculosDto>>(vehiculosDto);
            return Ok(response);
        }

        

        /// <summary>
        /// Obtiene los movimientos de todos los vechiculos
        /// El filtro de empresa es obligatorio, listaIdsEstados se pueden enviar ninguno, uno o varios estados separados por coma(,)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteMovimientos")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<movimientoVehiculoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoReporteMovimientos([FromQuery] ReporteMovimientosVehiculosQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var idUsuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            filter.idUsuario = idUsuario;

            var movsVehiculos = _activoMovimientosService.reporteMovimientosVehiculos(filter); 

            var response = new AguilaResponse<IEnumerable<movimientoVehiculoDto>>(movsVehiculos);
            return Ok(response);
        }


        /// <summary>
        /// Obtiene un listado de vehiculos con su historico y su estado actual
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("historial")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoMovimientos([FromQuery] activosHistorialQueryFilter filter)
        {
            var xActivoMovimientosQueryFilter = new activoMovimientosQueryFilter()
            {
                idActivo = filter.idActivo,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var activoMovimientos = _activoMovimientosService.GetActivoMovimientos(xActivoMovimientosQueryFilter);
            var activoMovimientosDto = _mapper.Map<IEnumerable<activoMovimientosDto>>(activoMovimientos);

            var metadata = new Metadata
            {
                TotalCount = activoMovimientos.TotalCount,
                PageSize = activoMovimientos.PageSize,
                CurrentPage = activoMovimientos.CurrentPage,
                TotalPages = activoMovimientos.TotalPages,
                HasNextPage = activoMovimientos.HasNextPage,
                HasPreviousPage = activoMovimientos.HasPreviousPage,
            };


            var response = new AguilaResponse<IEnumerable<activoMovimientosDto>>(activoMovimientosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un vehiculo con su estado actual
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("buscarPorCodigo/{codigo}/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<vehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVehiculoPorCodigo(string codigo, int idEmpresa)
        {
            var activoOperaciones = _activoMovimientosActualService.GetVehiculoByCodigo(codigo, idEmpresa);
            var activoOperacionesDto = _mapper.Map<vehiculosDto>(activoOperaciones);

            if (activoOperacionesDto.activoOperacion.categoria.ToLower() == "v")
            {
                var xVehiculo = await  _vehiculosService.GetVehiculo(activoOperacionesDto.idActivo);
                activoOperacionesDto.activoOperacion.placa = xVehiculo.placa;
            }

            var response = new AguilaResponse<vehiculosDto>(activoOperacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Filtra todos los estados para el control de vehiculos(cabezales)
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("activosEstados/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoEstadoCondicion(int idEmpresa)
        {
            var xEventosCondicionVehiculo = new List<string>
            {
                ControlActivosEventos.CondicionIngreso.ToString(),
                ControlActivosEventos.CondicionSalida.ToString(),
                ControlActivosEventos.Reparado.ToString(),
                ControlActivosEventos.Asignado.ToString(),
                ControlActivosEventos.Reservado.ToString(),
                ControlActivosEventos.Egresado.ToString(),
                ControlActivosEventos.Bodega.ToString(),
                ControlActivosEventos.RentaInterna.ToString(),
                ControlActivosEventos.RentaExterna.ToString(),
                ControlActivosEventos.Ingresado.ToString(),
                ControlActivosEventos.IngresoConServicio.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionVehiculo);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }


        [HttpPost("ingresoSalida")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoMovimientosDto evento)
        {
            evento.fecha = DateTime.Now;
            if (evento.evento == ControlActivosEventos.Ingresado)
            {
                evento.tipoDocumento = "INGRESO";
                evento.observaciones = evento.observaciones;
                evento.documento = 0;
                if ((bool)evento.cargado)
                {
                    evento.evento = ControlActivosEventos.IngresoConServicio;
                }
            }
            else
            {
                evento.tipoDocumento = "SALIDA";
                evento.observaciones = evento.observaciones;
                evento.documento = 0;
            }

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("reserva")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Reserva(activoMovimientosDto evento)
        {
            evento.fecha = DateTime.Now;
            evento.evento = ControlActivosEventos.Reservado; //Reserva

            //Necesitamos el num de la solicitud en documento "solicitud Numero"
            //En observaciones necesitamos el cliente y numero de contenedor
            //Campo Lugar concatenar origen y destino
            evento.tipoDocumento = "SOLICITUD";
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("quitarReserva")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> quitarReserva(activoMovimientosDto evento)
        {
            evento.fecha = DateTime.Now;
            evento.evento = ControlActivosEventos.QuitarReserva; //Quitamos reserva

            evento.tipoDocumento = "DISPONIBLE";
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("preReserva")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PreReserva(activoMovimientosDto evento)
        {
            evento.fecha = DateTime.Now;
            evento.evento = ControlActivosEventos.PreReserva; //PreReserva

            evento.tipoDocumento = "SOLICITUD";
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);//se utiliza cuando no le enviamos un idEstado, debe ir idEstado=0

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("bodega")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Bodega(activoMovimientosDto evento)
        {
            evento.evento = ControlActivosEventos.Bodega; //Bodega

            evento.tipoDocumento = "BODEGA";
            evento.idEstado = null;
            evento.observaciones = evento.observaciones;
            evento.lugar = evento.observaciones.Trim();

            if (evento.observaciones.Trim().Length > 30)
            {
                evento.lugar = evento.observaciones.Substring(0, 30);
            }

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("reparacion")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Reparacion(activoMovimientosDto evento)
        {
            evento.evento = ControlActivosEventos.Reparado; //Reparacion

            evento.tipoDocumento = "REPARACION";
            evento.idEstado = null;
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("rentaInterna")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RentaInterna(activoMovimientosDto evento)
        {
            evento.evento = ControlActivosEventos.RentaInterna; //Renta Interna

            evento.tipoDocumento = "RENTA INTERNA";
            evento.idEstado = null;
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("rentaExterna")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RentaExterna(activoMovimientosDto evento)
        {
            evento.evento = ControlActivosEventos.RentaExterna; //Renta Externa

            evento.tipoDocumento = "RENTA EXTERNA";
            evento.idEstado = null;
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("ruta")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Ruta(activoMovimientosDto evento)
        {
            evento.evento = ControlActivosEventos.Egresado; //Renta Externa

            evento.tipoDocumento = "RUTA";
            evento.idEstado = null;
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpPost("disponible")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> disponible(activoMovimientosDto evento)
        {
            evento.fecha = DateTime.Now;
            //evento.evento = ControlActivosEventos.Creacion; //Lo colocamos en Disponible
            evento.evento = ControlActivosEventos.PonerDisponible;

            evento.tipoDocumento = "DISPONIBLE";
            evento.observaciones = evento.observaciones;

            var activoMovimiento = await _activoMovimientosService.InsertMovimientoPorEvento(evento);

            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }


        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _activoMovimientosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
