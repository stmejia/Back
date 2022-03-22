using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class llantaActualController : ControllerBase
    {
        private readonly IllantaActualService _llantaActualService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public llantaActualController(IllantaActualService llantaActualService, IMapper mapper, IPasswordService passwordService)
        {
            _llantaActualService = llantaActualService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las llantas actuales registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetLlantaActual([FromQuery] llantaActualQueryFilter filter)
        {
            var llantaActual = _llantaActualService.GetLlantaActual(filter);
            var llantaActualDto = _mapper.Map<IEnumerable<llantaActualDto>>(llantaActual);

            var metadata = new Metadata
            {
                TotalCount = llantaActual.TotalCount,
                PageSize = llantaActual.PageSize,
                CurrentPage = llantaActual.CurrentPage,
                TotalPages = llantaActual.TotalPages,
                HasNextPage = llantaActual.HasNextPage,
                HasPreviousPage = llantaActual.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<llantaActualDto>>(llantaActualDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una llanta actual por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLlantaActual(int id)
        {
            var llantaActual = await _llantaActualService.GetLlantaActual(id);
            var llantaActualDto = _mapper.Map<llantaActualDto>(llantaActual);

            var response = new AguilaResponse<llantaActualDto>(llantaActualDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo registro de tipo llanta actual
        /// </summary>
        /// <param name="llantaActualDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(llantaActualDto llantaActualDto)
        {
            var llantaActual = _mapper.Map<llantaActual>(llantaActualDto);
            await _llantaActualService.InsertLlantaActual(llantaActual);

            llantaActualDto = _mapper.Map<llantaActualDto>(llantaActual);
            var response = new AguilaResponse<llantaActualDto>(llantaActualDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una llanta actual, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="llantaActualDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, llantaActualDto llantaActualDto)
        {
            var llantaActual = _mapper.Map<llantaActual>(llantaActualDto);
            llantaActual.idLlanta = id;

            var result = await _llantaActualService.UpdateLlantaActual(llantaActual);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una llanta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaActualDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _llantaActualService.DeleteLlantaActual(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/LlantaActual/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _llantaActualService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
