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
/* NO ESTA EN UN USO ACTUALMENTE,EVALUAR LA ELIMINACION DEFINITIVA */
namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsigUsuariosRecursosAtributosController : ControllerBase
    {
        public readonly IAsigUsuariosRecursosAtributosService _asigUsuariosRecursosAtributosService;
        public readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public AsigUsuariosRecursosAtributosController(IAsigUsuariosRecursosAtributosService asigUsuariosRecursosAtributosService,
                                                        IMapper mapper, IPasswordService passwordService)
        {
            _asigUsuariosRecursosAtributosService = asigUsuariosRecursosAtributosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

       
        /// <summary>
        /// Extraccion total de Asignaciones  usuariosRecursosAtributos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<AsigUsuariosRecursosAtributosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRecursosAtributos([FromQuery] AsigUsuariosRecursosAtributosQueryFilter filter)
        {
            var recursosAtributos =  _asigUsuariosRecursosAtributosService.GetAsigRecursosAtributos(filter);
            var recursosAtributosDTO = _mapper.Map< IEnumerable<AsigUsuariosRecursosAtributosDto>>(recursosAtributos);

            var metadata = new Metadata
            {
                TotalCount = recursosAtributos.TotalCount,
                PageSize = recursosAtributos.PageSize,
                CurrentPage = recursosAtributos.CurrentPage,
                TotalPages = recursosAtributos.TotalPages,
                HasNextPage = recursosAtributos.HasNextPage,
                HasPreviousPage = recursosAtributos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<AsigUsuariosRecursosAtributosDto>>(recursosAtributosDTO)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        //retorna una asigancion de recurso atributo especifica por medio de su ID de registro

        /// <summary>
        /// Consulta de Asignacion usuariosRecursosAtributos, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<AsigUsuariosRecursosAtributosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsigRecursoAtributo(long id)
        {
            var recursoAtributo = await _asigUsuariosRecursosAtributosService.GetAsigUsuarioRecursoAtributo(id);
            var recursoAtriburoDTO = _mapper.Map<AsigUsuariosRecursosAtributosDto>(recursoAtributo);

            var response = new AguilaResponse<AsigUsuariosRecursosAtributosDto>(recursoAtriburoDTO);
            return Ok(response);
        }

        //inserta un nuevo registro (asigUsuarioRecursoAtriburo)

        /// <summary>
        /// Crear Asignacion  usuariosRecursosAtributos
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<AsigUsuariosRecursosAtributosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(AsigUsuariosRecursosAtributosDto recursoAtributoDTO)
        {
            var recursoAtributo = _mapper.Map<AsigUsuariosRecursosAtributos>(recursoAtributoDTO);
            await _asigUsuariosRecursosAtributosService.insertAsigUsuarioRecursoAtributo(recursoAtributo);

            recursoAtributoDTO = _mapper.Map<AsigUsuariosRecursosAtributosDto>(recursoAtributo);
            var response = new AguilaResponse<AsigUsuariosRecursosAtributosDto>(recursoAtributoDTO);

            return Ok(response); 
        }



        /// <summary>
        /// Actualizacion de Asignacion usuariosRecursosAtributos, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recursoAtributoDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, AsigUsuariosRecursosAtributosDto recursoAtributoDTO)
        {
            var recursoAtributo = _mapper.Map<AsigUsuariosRecursosAtributos>(recursoAtributoDTO);
            recursoAtributo.Id = id;

            var result = await _asigUsuariosRecursosAtributosService.updateUsuarioRecursoAtributo(recursoAtributo);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        
        /// <summary>
        /// Eliminar Asignacion de usuarioRecursoAtributo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _asigUsuariosRecursosAtributosService.deleteAsigUsuarioRecursoAtributo(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

    }
}
