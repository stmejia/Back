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
    public class llantasController : ControllerBase
    {
        private readonly IllantasService _llantasService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public llantasController(IllantasService llantasService, IMapper mapper, IPasswordService passwordService)
        {
            _llantasService = llantasService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las llantas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetLlantas([FromQuery] llantasQueryFilter filter)
        {
            var llantas = _llantasService.GetLlantas(filter);
            var llantasDto = _mapper.Map<IEnumerable<llantasDto>>(llantas);

            var metadata = new Metadata
            {
                TotalCount = llantas.TotalCount,
                PageSize = llantas.PageSize,
                CurrentPage = llantas.CurrentPage,
                TotalPages = llantas.TotalPages,
                HasNextPage = llantas.HasNextPage,
                HasPreviousPage = llantas.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<llantasDto>>(llantasDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una llanta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLlanta(int id)
        {
            var llantas = await _llantasService.GetLlanta(id);
            var llantasDto = _mapper.Map<llantasDto>(llantas);

            var response = new AguilaResponse<llantasDto>(llantasDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva llanta
        /// </summary>
        /// <param name="llantaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(llantasDto llantaDto)
        {
            var llanta = _mapper.Map<llantas>(llantaDto);
            await _llantasService.InsertLlanta(llanta);

            llantaDto = _mapper.Map<llantasDto>(llanta);
            var response = new AguilaResponse<llantasDto>(llantaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una llanta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="llantaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, llantasDto llantaDto)
        {
            var llanta = _mapper.Map<llantas>(llantaDto);
            llanta.id = id;

            var result = await _llantasService.UpdateLlanta(llanta);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una llanta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _llantasService.DeleteLlanta(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Llantas/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _llantasService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
