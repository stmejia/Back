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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsigUsuariosModulosController : ControllerBase
    {
        private readonly IAsigUsuariosModulosService _asigUsuariosModulosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public AsigUsuariosModulosController(IAsigUsuariosModulosService asigUsuariosModulosService, IMapper mapper
                                            , IPasswordService passwordService)
        {
            _asigUsuariosModulosService = asigUsuariosModulosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Asignaciones UsuariosModulos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<AsigUsuariosModulosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAsigUsuariosModulos([FromQuery] AsigUsuariosModulosQueryFilter filter)
        {
            var modulos =  _asigUsuariosModulosService.GetAsigUsuariosModulos(filter);
            var modulosDto = _mapper.Map<IEnumerable<AsigUsuariosModulosDto>>(modulos);

            var metadata = new Metadata
            {
                TotalCount = modulos.TotalCount,
                PageSize = modulos.PageSize,
                CurrentPage = modulos.CurrentPage,
                TotalPages = modulos.TotalPages,
                HasNextPage = modulos.HasNextPage,
                HasPreviousPage = modulos.HasPreviousPage,
                
            };


            var response = new AguilaResponse<IEnumerable<AsigUsuariosModulosDto>>(modulosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Modulos de un usuario, enviar id de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<List<AsigUsuariosModulosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetModulosUsuario(long id)
        {            
            var usuarioModulos = _asigUsuariosModulosService.GetAsigUsuarioModulos(id).ToList();
            var usuarioModulosDto = _mapper.Map<List<AsigUsuariosModulosDto>>(usuarioModulos);          

            var response = new AguilaResponse<List<AsigUsuariosModulosDto>>(usuarioModulosDto);
            return Ok(response);
        }


        /// <summary>
        /// Crear Asignacion de UsuarioModulo
        /// </summary>      
        /// <param name="asigUsuariosModulosDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<AsigUsuariosModulosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(AsigUsuariosModulosDto asigUsuariosModulosDto)
        {
            var usuarioModulo = _mapper.Map<AsigUsuariosModulos>(asigUsuariosModulosDto);
            await _asigUsuariosModulosService.InsertAsigUsuarioModulo(usuarioModulo);

            asigUsuariosModulosDto = _mapper.Map<AsigUsuariosModulosDto>(usuarioModulo);
            var response = new AguilaResponse<AsigUsuariosModulosDto>(asigUsuariosModulosDto);

            return Ok(response);
        }

        /// <summary>
        /// Eliminar Asignacion de UsuariosModulos
        /// </summary>
        /// <param name="asigUsuariosModulosDto"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(AsigUsuariosModulosDto asigUsuariosModulosDto)
        {

            var result = await _asigUsuariosModulosService.DeleteAsigUsuarioModulo(asigUsuariosModulosDto.UsuarioId, asigUsuariosModulosDto.ModuloId);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/AsigUsuariosModulos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _asigUsuariosModulosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
