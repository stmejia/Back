using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class llantaTiposController : ControllerBase
    {
        private readonly IllantaTiposService _llantaTiposService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public llantaTiposController(IllantaTiposService llantaTiposService, IMapper mapper, IPasswordService password)
        {
            _llantaTiposService = llantaTiposService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los tipos de llantas registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetLlantaTipos([FromQuery] llantaTiposQueryFilter filter)
        {
            var llantaTipos = _llantaTiposService.GetLlantaTipos(filter);
            var llantaTiposDto = _mapper.Map<IEnumerable<llantaTiposDto>>(llantaTipos);

            var metadata = new Metadata
            {
                TotalCount = llantaTipos.TotalCount,
                PageSize = llantaTipos.PageSize,
                CurrentPage = llantaTipos.CurrentPage,
                TotalPages = llantaTipos.TotalPages,
                HasNextPage = llantaTipos.HasNextPage,
                HasPreviousPage = llantaTipos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<llantaTiposDto>>(llantaTiposDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de llanta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLlantaTipo(int id)
        {
            var llantaTipos = await _llantaTiposService.GetLlantaTipo(id);
            var llantaTiposDto = _mapper.Map<llantaTiposDto>(llantaTipos);

            var response = new AguilaResponse<llantaTiposDto>(llantaTiposDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo de llanta
        /// </summary>
        /// <param name="llantaTipoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(llantaTiposDto llantaTipoDto)
        {
            var llantaTipo = _mapper.Map<llantaTipos>(llantaTipoDto);
            await _llantaTiposService.InsertLlantaTipo(llantaTipo);

            llantaTipoDto = _mapper.Map<llantaTiposDto>(llantaTipo);
            var response = new AguilaResponse<llantaTiposDto>(llantaTipoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo de llanta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="llantaTipoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, llantaTiposDto llantaTipoDto)
        {
            var llantaTipo = _mapper.Map<llantaTipos>(llantaTipoDto);
            llantaTipo.id = id;

            var result = await _llantaTiposService.UpdateLlantaTipo(llantaTipo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de llanta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<llantaTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _llantaTiposService.DeleteLlantaTipo(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/LlantaTipo/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _llantaTiposService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
