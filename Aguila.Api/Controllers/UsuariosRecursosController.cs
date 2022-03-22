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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosRecursosController : ControllerBase
    {
        private readonly IUsuariosRecursosService _usuariosRecursosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        
        public UsuariosRecursosController(IUsuariosRecursosService usuariosRecursosService,
                                          IMapper mapper, 
                                          IPasswordService passwordService)
        {
            _usuariosRecursosService = usuariosRecursosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de UsuariosRecursos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Lista de uruarios</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<UsuariosRecursosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsuariosRecursos([FromQuery] UsuariosRecursosQueryFilter filter)
        {
            var usuriosRecursos = _usuariosRecursosService.GetUsuariosRecursos(filter);
            var usuariosRecursosDto = _mapper.Map<IEnumerable<UsuariosRecursosDto>>(usuriosRecursos);

            var metadata = new Metadata
            {
                TotalCount = usuriosRecursos.TotalCount,
                PageSize = usuriosRecursos.PageSize,
                CurrentPage = usuriosRecursos.CurrentPage,
                TotalPages = usuriosRecursos.TotalPages,
                HasNextPage = usuriosRecursos.HasNextPage,
                HasPreviousPage = usuriosRecursos.HasPreviousPage,               
            };

            foreach (var recurso in usuariosRecursosDto)
            {
                recurso.opcionesAsignadas = null;
            }

            var response = new AguilaResponse<IEnumerable<UsuariosRecursosDto>>(usuariosRecursosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Consulta de UsuarioRecurso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<List<UsuariosRecursosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public  IActionResult GetUsuarioRecurso(long id)
        {
            //var usuarioRecursos = _usuariosRecursosService.GetUsuarioRecurso(id);
            var usuarioRecursos = _usuariosRecursosService.GetUsuarioRecursoIncludes(id).ToList();
            var usuarioRecursosDto = _mapper.Map<List<UsuariosRecursosDto>>(usuarioRecursos);
            //el string de opciones separados por coma se convierte en una lista para poblar la coleccion
            //usuarioRecursosDto.opcionesAsignadas = usuarioRecurso.opcionesAsignadas.Split(',').ToList();

            var response = new AguilaResponse<List<UsuariosRecursosDto>>(usuarioRecursosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Usuario Nueva Asginacion UsuarioRecurso
        /// </summary>      
        /// <param name="usuarioRecursoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<UsuariosRecursosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(UsuariosRecursosDto usuarioRecursoDto)
        {
            var usuarioRecurso = _mapper.Map<UsuariosRecursos>(usuarioRecursoDto);
            usuarioRecurso.opcionesAsignadas = "";

            //la coleccion de opciones se transforma a un string separado por comas
            foreach(var opcion in usuarioRecursoDto.opcionesAsignadas)
            {
                usuarioRecurso.opcionesAsignadas += opcion + ",";
            }

            await _usuariosRecursosService.InsertUsuarioRecurso(usuarioRecurso);
            usuarioRecursoDto = _mapper.Map<UsuariosRecursosDto>(usuarioRecurso);

            //el string de opciones separados por coma se convierte en una lista para poblar la coleccion
            usuarioRecursoDto.opcionesAsignadas = usuarioRecurso.opcionesAsignadas.Split(',').ToList();
            var response = new AguilaResponse<UsuariosRecursosDto>(usuarioRecursoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de UsuarioRecurso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuarioRecursoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, UsuariosRecursosDto usuarioRecursoDto)
        {
            var usuarioRecurso = _mapper.Map<UsuariosRecursos>(usuarioRecursoDto);
            usuarioRecurso.opcionesAsignadas = "";
            usuarioRecurso.id = id;

            //la coleccion de opciones se transforma a un string separado por comas
            foreach (var opcion in usuarioRecursoDto.opcionesAsignadas)
            {
                usuarioRecurso.opcionesAsignadas += opcion + ",";
            }

            var result = await _usuariosRecursosService.UpdateUsuarioRecurso(usuarioRecurso);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una Asignacionde  UsuarioRecurso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _usuariosRecursosService.DeleteUsuarioRecurso(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/UsuariosRecursos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _usuariosRecursosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
