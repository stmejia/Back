using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aguila.Api.Responses;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using Aguila.Core.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aguila.Core.CustomEntities;

namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosMnuController : ControllerBase
    {
        private readonly IModulosMnuService _modulosMnuService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public ModulosMnuController(IModulosMnuService modulosMnuService, IMapper mapper, IPasswordService passwordService)
        {
            _modulosMnuService = modulosMnuService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de ModulosMnu, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ModulosMnuDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetModulosMnu([FromQuery] ModulosMnuQueryFilter filter)
        {
            var modulosMnu = _modulosMnuService.GetModulosMnu(filter);
            var modulosMnuDto = _mapper.Map<IEnumerable<ModulosMnuDto>>(modulosMnu);

            var metadata = new Metadata
            {
                TotalCount = modulosMnu.TotalCount,
                PageSize = modulosMnu.PageSize,
                CurrentPage = modulosMnu.CurrentPage,
                TotalPages = modulosMnu.TotalPages,
                HasNextPage = modulosMnu.HasNextPage,
                HasPreviousPage = modulosMnu.HasPreviousPage,
               
            };

            var response = new AguilaResponse<IEnumerable<ModulosMnuDto>>(modulosMnuDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de ModuloMnu, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ModulosMnuDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetModuloMnu(int id)
        {
            var moduloMnu = await _modulosMnuService.GetModuloMnu(id);
            var moduloMnuDto = _mapper.Map<ModulosMnuDto>(moduloMnu);

            var response = new AguilaResponse<ModulosMnuDto>(moduloMnuDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear ModuloMnu Nuevo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ModulosMnuDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(ModulosMnuDto modulosMnuDto)
        {
            var moduloMnu = _mapper.Map<ModulosMnu>(modulosMnuDto);
            await _modulosMnuService.InsertModuloMnu(moduloMnu);

            modulosMnuDto = _mapper.Map<ModulosMnuDto>(moduloMnu);
            var response = new AguilaResponse<ModulosMnuDto>(modulosMnuDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de ModuloMnu, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modulosMnuDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, ModulosMnuDto modulosMnuDto)
        {
            var modulosMnu = _mapper.Map<ModulosMnu>(modulosMnuDto);
            modulosMnu.Id = id;

            var result = await _modulosMnuService.updateModuloMnu(modulosMnu);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar ModuloMnu, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _modulosMnuService.DeleteModuloMnu(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        [HttpGet("/api/ModulosMnu/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _modulosMnuService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
