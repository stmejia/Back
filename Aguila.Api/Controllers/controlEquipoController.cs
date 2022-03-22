using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Interfaces.Trafico.Model;
using Aguila.Interfaces.Trafico.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class controlEquipoController : ControllerBase
    {
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IMapper _mapper;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IestadosService _estadosService;
        private readonly IequipoRemolqueService _equipoRemolqueService;

        public controlEquipoController(IactivoMovimientosService activoMovimientosService, IMapper mapper, 
              IactivoMovimientosActualService activoMovimientosActualService,
              IestadosService estadosService,
              IequipoRemolqueService equipoRemolqueService)
        {
            _activoMovimientosService = activoMovimientosService;
            _mapper = mapper;
            _activoMovimientosActualService = activoMovimientosActualService;
            _estadosService = estadosService;
            _equipoRemolqueService = equipoRemolqueService;
        }

        /// <summary>
        /// Obtiene una lista de equipos con su estado actual
        /// el parametro global se utiliza para poder visualizar todo el
        /// inventario en una sola ubicacion
        /// Si no se envia el parametro global es necesario enviar idEstacionTrabajo
        /// regresara una lista de los equipos que se encuentren unicamente en dicha estacion o en ruta
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEquipos([FromQuery] equipoRemolqueQueryFilter filter)
        {
            var equipos = _activoMovimientosActualService.GetEquiposEstadoActual(filter);
            var equiposDto = _mapper.Map<IEnumerable<equipoRemolqueDto>>(equipos);

            var metadata = new Metadata
            {
                TotalCount = equipos.TotalCount,
                PageSize = equipos.PageSize,
                CurrentPage = equipos.CurrentPage,
                TotalPages = equipos.TotalPages,
                HasNextPage = equipos.HasNextPage,
                HasPreviousPage = equipos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<equipoRemolqueDto>>(equiposDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene los datos de un equipo con su estado actual
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEquipo(int idActivo)
        {
            var xEquipo = _activoMovimientosActualService.GetEquipoByID(idActivo);
            var equipoEstadoDto = _mapper.Map<equipoRemolqueDto>(xEquipo);

            var response = new AguilaResponse<equipoRemolqueDto>(equipoEstadoDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene los equipos que se encuentran en inventario con su estado actual
        /// Se omite el campo global
        /// El filtro de empresa es obligatorio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteInventario")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetInventarioEquipos([FromQuery] equipoRemolqueQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            filter.global = true;

            var equipos = _activoMovimientosActualService.reporteInventarioEquipo(filter, usuario);
            var equiposDto = _mapper.Map<IEnumerable<equipoRemolqueDto>>(equipos);

            var response = new AguilaResponse<IEnumerable<equipoRemolqueDto>>(equiposDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene los movimientos de todos los equipos
        /// El filtro de empresa es obligatorio, listaIdsEstados se pueden enviar ninguno, uno o varios estados separados por coma(,)
        /// Solo se envian movimientos de estaciones de trabajo que tenga asignadas el usuario.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("reporteMovimientos")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<movimientoEquipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoReporteMovimientos([FromQuery] ReporteMovimientosEquiposRemolqueQueryFilter filter)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var idUsuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            filter.idUsuario = idUsuario;

            var MovsEquipos = _activoMovimientosService.reporteMovimientosEquipos(filter);

            var response = new AguilaResponse<IEnumerable<movimientoEquipoRemolqueDto>>(MovsEquipos);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el historial del equipo de remolque por su id paginado
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

            if (filter.idActivo == null)
            {
                throw new AguilaException("Favor indicar el equipo para revisar su historial", 404);
            }

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
        /// Devuelve un equipo con su estado actual
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("buscarPorCodigo/{codigo}/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEquipoPorCodigo(string codigo, int idEmpresa)
        {
            var activoOperaciones = _activoMovimientosActualService.GetEquipoByCodigo(codigo, idEmpresa);
            var activoOperacionesDto = _mapper.Map<equipoRemolqueDto>(activoOperaciones);

            if (activoOperacionesDto.activoOperacion.categoria.ToLower() == "e")
            {
                var xEquipo = await _equipoRemolqueService.GetEquipoRemolque(activoOperacionesDto.idActivo);
                activoOperacionesDto.activoOperacion.placa = xEquipo.placa;
            }

            var response = new AguilaResponse<equipoRemolqueDto>(activoOperacionesDto);
            return Ok(response);
        }
               

        /// <summary>
        /// Filtra todos los estados para el control de equipo
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("activosEstados/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoEstadoCondicion(int idEmpresa)
        {
            var xEventosCondicionEquipo = new List<string>
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

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionEquipo);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Registra un nuevo ingreso de activo
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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
           // //*SE VERIFICA SI SE DEBE CREAR UN EVENTO DE ALERTA EN EL BOLSON
           //await _activoMovimientosService.verificarEvento(evento);
           // //*

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
            evento.observaciones =  evento.observaciones;

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

        /// <summary>
        /// PreReserva equipo/vehiculo , enviar activoMovimiento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Posiciona un equipo/vehiculo en Bodega de Cliente, enviar activoMovimiento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Envia un equipo/vehiculo a Reparacion, enviar activoMovimiento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Envia un equipo/vehiculo a Renta Interna, enviar activoMovimiento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Envia un equipo/vehiculo a Renta Externa, enviar activoMovimiento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Envia un equipo/vehiculo a Ruta, enviar activoMovimiento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
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
            var recurso = await _activoMovimientosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un listado de servicios del sistema Altas - Trafico pendientes de reservar equipo
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SolicitudesReservasAltas")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<SolicitudesMovimientosIntegracion>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SolicitudesReservasAltas([FromQuery] SolicitudesMovimientosQueryFilter filter)
        {
            var xSoliMovs = _activoMovimientosActualService.SolicitudesReservasAltas(filter);

            var metadata = new Metadata
            {
                TotalCount = xSoliMovs.TotalCount,
                PageSize = xSoliMovs.PageSize,
                CurrentPage = xSoliMovs.CurrentPage,
                TotalPages = xSoliMovs.TotalPages,
                HasNextPage = xSoliMovs.HasNextPage,
                HasPreviousPage = xSoliMovs.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<SolicitudesMovimientosIntegracion>>(xSoliMovs)
            {
                Meta = metadata
            };

            return Ok(response);
        }



    }
}
