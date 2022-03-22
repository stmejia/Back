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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public RolesController(IRolesService rolesService, IMapper mapper, IPasswordService passwordService)
        {
            _rolesService = rolesService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Roles, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<RolesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRoles([FromQuery] RolesQueryFilter filter)
        {
            var roles = _rolesService.GetRoles(filter);
            var rolesDto = _mapper.Map<IEnumerable<RolesDto>>(roles);

            var metadata = new Metadata
            {
                TotalCount = roles.TotalCount,
                PageSize = roles.PageSize,
                CurrentPage = roles.CurrentPage,
                TotalPages = roles.TotalPages,
                HasNextPage = roles.HasNextPage,
                HasPreviousPage = roles.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<RolesDto>>(rolesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Rol, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<RolesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRol(int id)
        {
            var rol = await _rolesService.GetRol(id);
            var rolDTo = _mapper.Map<RolesDto>(rol);

            var response = new AguilaResponse<RolesDto>(rolDTo);
            return Ok(response);
        }

        /// <summary>
        /// Crear Rol Nuevo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<EmpresasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(RolesDto rolDTo)
        {
            var rol = _mapper.Map<Roles>(rolDTo);
            await _rolesService.InsertRol(rol);

            rolDTo = _mapper.Map<RolesDto>(rol);
            var response = new AguilaResponse<RolesDto>(rolDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Rol, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rolDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(byte id, RolesDto rolDTo)
        {
            var rol = _mapper.Map<Roles>(rolDTo);
            rol.id = id;

            var result = await _rolesService.UpdateRol(rol);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Rol, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(byte id)
        {

            var result = await _rolesService.DeleteRol(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/Roles/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _rolesService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
