using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class eventosControlEquipoController : ControllerBase
    {
        private readonly IeventosControlEquipoService _eventosControlEquipoService;
        private readonly IMapper _mapper;
        private readonly IUsuariosService _usuariosService;
        public eventosControlEquipoController(IeventosControlEquipoService eventosControlEquipoService, IMapper mapper,
                                              IUsuariosService usuariosService)
        {
            _eventosControlEquipoService = eventosControlEquipoService;
            _mapper = mapper;
            _usuariosService = usuariosService;

        }

        /// <summary>
        /// Obtiene los eventos registrados en un rango de fechas, enviar fechaInicio y fechaFin
        /// , *para usar el filtro de estado los valores posibles a enviar son CREADO, REVISADO, RESUELTO, ANULADO
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<eventosControlEquipoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEventosControlEquipo([FromQuery] eventosControlEquipoQueryFilter filter)
        {            
            var eventosControl = _eventosControlEquipoService.GetEventosControl(filter); 
            var eventosControlDto = _mapper.Map<IEnumerable<eventosControlEquipoDto>>(eventosControl);

            //se aplican filtros de estado, se realiza aqui en el controlador porque el campo estado es virtual y se llena
            //cuando se realiza el mapeo del resultado de entidades a DTo's
            if (filter.estado!=null) eventosControlDto = eventosControlDto.Where(x => x.estado.ToUpper().Trim().Equals(filter.estado.ToUpper().Trim()));

            var metadata = new Metadata
            {
                TotalCount = eventosControl.TotalCount,
                PageSize = eventosControl.PageSize,
                CurrentPage = eventosControl.CurrentPage,
                TotalPages = eventosControl.TotalPages,
                HasNextPage = eventosControl.HasNextPage,
                HasPreviousPage = eventosControl.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<eventosControlEquipoDto>>(eventosControlDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un evento especifico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<eventosControlEquipoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEventoControlEquipo(long id)
        {
            var eventoControl = await _eventosControlEquipoService.GetEventoControl(id);
            var eventoControlDto = _mapper.Map<eventosControlEquipoDto>(eventoControl);

            var response = new AguilaResponse<eventosControlEquipoDto>(eventoControlDto);
            return Ok(response);
        }


        /// <summary>
        /// Registra un evento de control de equipo
        /// </summary>
        /// <param name="eventoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<eventosControlEquipoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostEventoControlEquipo(eventosControlEquipoDto eventoDto)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            var usuario = await _usuariosService.GetUsuario(usuarioId);

            string nombreUsuario = "";
            if (usuario != null) nombreUsuario = usuario.Username;

            eventoDto.idUsuarioCreacion = usuarioId;//se asigna el usuario que crea el evento
            eventoDto.fechaCreacion = DateTime.Now; //se asigna la fecha en que se crea el evento
            var eventoControl = await _eventosControlEquipoService.InsertEventoControl(eventoDto,nombreUsuario);
            var response = new AguilaResponse<eventosControlEquipoDto>(eventoControl);
            return Ok(response);
        }


        /// <summary>
        /// Asigna el estatus Revisado a un evento de control de equipo , enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventoDTo"></param>
        /// <returns></returns>
        [HttpPut("revisar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> setRevisarEvento(long id, eventosControlEquipoDto eventoDTo)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            var usuario = await _usuariosService.GetUsuario(usuarioId);

            string nombreUsuario = "";
            if (usuario != null) nombreUsuario = usuario.Username;


            eventoDTo.id = id;
            eventoDTo.fechaRevisado = DateTime.Now;
            eventoDTo.idUsuarioRevisa = usuarioId;

            var result = await _eventosControlEquipoService.RevisarEventoControl(eventoDTo, nombreUsuario);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Asigna el estatus Resuelto a un evento de control de equipo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventoDTo"></param>
        /// <returns></returns>
        [HttpPut("resolver/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> setResolverEvento(long id, eventosControlEquipoDto eventoDTo)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            var usuario = await _usuariosService.GetUsuario(usuarioId);

            string nombreUsuario = "";
            if (usuario != null) nombreUsuario = usuario.Username;

            eventoDTo.id = id;
            eventoDTo.fechaResuelto = DateTime.Now;
            eventoDTo.idUsuarioResuelve = usuarioId;

            var result = await _eventosControlEquipoService.ResolverEventoControl(eventoDTo, nombreUsuario);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Asigna el estatus Anulado a un evento de control de equipo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventoDTo"></param>
        /// <returns></returns>
        [HttpPut("anular/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> setAnularEvento(long id, eventosControlEquipoDto eventoDTo)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());
            var usuario = await _usuariosService.GetUsuario(usuarioId);

            string nombreUsuario = "";
            if (usuario != null) nombreUsuario = usuario.Username;

            eventoDTo.id = id;
            eventoDTo.fechaAnulado = DateTime.Now;
            eventoDTo.idUsuarioAnula = usuarioId;

            var result = await _eventosControlEquipoService.AnularEventoControl(eventoDTo,nombreUsuario);
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
            var recurso = await _eventosControlEquipoService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
