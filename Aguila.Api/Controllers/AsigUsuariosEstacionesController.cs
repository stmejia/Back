using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AsigUsuariosEstacionesController : ControllerBase
    {
        private readonly IAsigUsuariosEstacionesTrabajoService _usuarioEstacionService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public AsigUsuariosEstacionesController(IAsigUsuariosEstacionesTrabajoService usuarioEstacionService,
                                                IMapper mapper, IPasswordService passwordService)
        {
            _usuarioEstacionService = usuarioEstacionService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Asignaciones UsuarioEstacionesTrabajo, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<AsigUsuariosEstacionesTrabajoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsuarioEstaciones([FromQuery] AsigUsuariosEstacionesTrabajoQueryFilter filter)
        {
            var usuarioEstaciones = _usuarioEstacionService.GetUsuarioEstaciones(filter);
            var usuarioEstacionesDto = _mapper.Map<IEnumerable<AsigUsuariosEstacionesTrabajoDto>>(usuarioEstaciones);

            var metadata = new Metadata
            {
                TotalCount = usuarioEstaciones.TotalCount,
                PageSize = usuarioEstaciones.PageSize,
                CurrentPage = usuarioEstaciones.CurrentPage,
                TotalPages = usuarioEstaciones.TotalPages,
                HasNextPage = usuarioEstaciones.HasNextPage,
                HasPreviousPage = usuarioEstaciones.HasPreviousPage,

            };


            var response = new AguilaResponse<IEnumerable<AsigUsuariosEstacionesTrabajoDto>>(usuarioEstacionesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Asignacion UsuarioEstacionesTrabajo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<List<AsigUsuariosEstacionesTrabajoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public  IActionResult GetUsuarioEstaciones(long id)
        {
            var usuarioEstaciones =  _usuarioEstacionService.GetEstacionesUsuarisIncludes(id).ToList();
            var usuarioEstacionesDto = _mapper.Map<List<AsigUsuariosEstacionesTrabajoDto>>(usuarioEstaciones);

            var response = new AguilaResponse<List<AsigUsuariosEstacionesTrabajoDto>>(usuarioEstacionesDto);
            return Ok(response);

        }

        /// <summary>
        /// Crear Asignacion UsuarioEstacionesTrabajo
        /// </summary>        
        /// <param name="usuarioEstacionDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<AsigUsuariosEstacionesTrabajoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostUsuarioEstacion(AsigUsuariosEstacionesTrabajoDto usuarioEstacionDTO)
        {
            var usuarioEstacion = _mapper.Map<AsigUsuariosEstacionesTrabajo>(usuarioEstacionDTO);
            await _usuarioEstacionService.InsertAsigUsuarioEstacion(usuarioEstacion);

            usuarioEstacionDTO = _mapper.Map<AsigUsuariosEstacionesTrabajoDto>(usuarioEstacion);
            var response = new AguilaResponse<AsigUsuariosEstacionesTrabajoDto>(usuarioEstacionDTO);

            return Ok(response);
        }

        /// <summary>
        /// Eliminar Asignacion UsuarioEstacionesTrabajo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUsuarioEstacion(long id)
        {
            var result = await _usuarioEstacionService.DeleteAsigUsuarioEstacionTrabajo(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/AsigUsuariosEstaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _usuarioEstacionService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
