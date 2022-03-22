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
    public class ModulosController : ControllerBase
    {
        private readonly IModulosService _modulosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public ModulosController(IModulosService modulosService, IMapper mapper, IPasswordService passwordService)
        {
            _modulosService = modulosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Modulos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ModulosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetModulos([FromQuery] ModuloQueryFilter filter)
        {
            var modulos = _modulosService.GetModulos(filter);
            var modulosDto = _mapper.Map<IEnumerable<ModulosDto>>(modulos);

            var metadata = new Metadata
            {
                TotalCount = modulos.TotalCount,
                PageSize = modulos.PageSize,
                CurrentPage = modulos.CurrentPage,
                TotalPages = modulos.TotalPages,
                HasNextPage = modulos.HasNextPage,
                HasPreviousPage = modulos.HasPreviousPage,
                
            };

            var response = new AguilaResponse<IEnumerable<ModulosDto>>(modulosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Consulta de Modulo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ModulosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetModulo(byte id)
        {
            var modulo = await _modulosService.GetModulo(id);
            var moduloDto = _mapper.Map<ModulosDto>(modulo);

            var response = new AguilaResponse<ModulosDto>(moduloDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Modulo Nuevo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ModulosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(ModulosDto moduloDTo)
        {
            var modulo = _mapper.Map<Modulos>(moduloDTo);
            await _modulosService.InsertModulo(modulo);

            moduloDTo = _mapper.Map<ModulosDto>(modulo);
            var response = new AguilaResponse<ModulosDto>(moduloDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Modulo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modulolDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(byte id, ModulosDto modulolDTo)
        {
            var modulo = _mapper.Map<Modulos>(modulolDTo);
            modulo.Id = id;

            var result = await _modulosService.updateModulo(modulo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Modulo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(byte id)
        {

            var result = await _modulosService.DeleteModulo(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        [HttpGet("/api/Modulos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _modulosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
