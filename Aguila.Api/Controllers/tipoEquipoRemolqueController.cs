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
    public class tipoEquipoRemolqueController : ControllerBase
    {
        private readonly ItipoEquipoRemolqueService _tipoEquipoRemolqueService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoEquipoRemolqueController(ItipoEquipoRemolqueService tipoEquipoRemolqueService, IMapper mapper, IPasswordService password)
        {
            _tipoEquipoRemolqueService = tipoEquipoRemolqueService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los tipos de remolque registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoEquipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTipoEquipoRemolque([FromQuery] tipoEquipoRemolqueQueryFilter filter)
        {
            var tipoEquipoRemolque = _tipoEquipoRemolqueService.GetTipoEquipoRemolque(filter);
            var tipoEquipoRemolqueDto = _mapper.Map<IEnumerable<tipoEquipoRemolqueDto>>(tipoEquipoRemolque);

            var metadata = new Metadata
            {
                TotalCount = tipoEquipoRemolque.TotalCount,
                PageSize = tipoEquipoRemolque.PageSize,
                CurrentPage = tipoEquipoRemolque.CurrentPage,
                TotalPages = tipoEquipoRemolque.TotalPages,
                HasNextPage = tipoEquipoRemolque.HasNextPage,
                HasPreviousPage = tipoEquipoRemolque.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<tipoEquipoRemolqueDto>>(tipoEquipoRemolqueDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de equipo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoEquipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoEquipoRemolque(int id)
        {
            var tipoEquipoRemolque = await _tipoEquipoRemolqueService.GetTipoEquipoRemolque(id);
            var tipoEquipoRemolqueDto = _mapper.Map<tipoEquipoRemolqueDto>(tipoEquipoRemolque);

            var response = new AguilaResponse<tipoEquipoRemolqueDto>(tipoEquipoRemolqueDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo de equipo
        /// </summary>
        /// <param name="tipoEquipoRemolqueDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoEquipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoEquipoRemolqueDto tipoEquipoRemolqueDto)
        {
            var tipoEquipoRemolque = _mapper.Map<tipoEquipoRemolque>(tipoEquipoRemolqueDto);
            await _tipoEquipoRemolqueService.InsertTipoEquipoRemolque(tipoEquipoRemolque);

            tipoEquipoRemolqueDto = _mapper.Map<tipoEquipoRemolqueDto>(tipoEquipoRemolque);
            var response = new AguilaResponse<tipoEquipoRemolqueDto>(tipoEquipoRemolqueDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo de equipo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoEquipoRemolqueDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoEquipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoEquipoRemolqueDto tipoEquipoRemolqueDto)
        {
            var tipoEquipoRemolque = _mapper.Map<tipoEquipoRemolque>(tipoEquipoRemolqueDto);
            tipoEquipoRemolque.id = id;

            var result = await _tipoEquipoRemolqueService.UpdateTipoEquipoRemolque(tipoEquipoRemolque);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de equipo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoEquipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoEquipoRemolqueService.DeleteTipoEquipoRemolque(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/TipoEquipoRemolque/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tipoEquipoRemolqueService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
