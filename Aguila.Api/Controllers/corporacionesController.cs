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
    public class corporacionesController : ControllerBase
    {
        private readonly IcorporacionesService _corporacionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public corporacionesController(IcorporacionesService corporacionesService, IMapper mapper, IPasswordService password)
        {
            _corporacionesService = corporacionesService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene las corporaciones registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<corporacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCorporaciones([FromQuery] corporacionesQueryFilter filter)
        {
            var corporaciones = _corporacionesService.GetCorporaciones(filter);
            var corporacionesDto = _mapper.Map<IEnumerable<corporacionesDto>>(corporaciones);

            var metadata = new Metadata
            {
                TotalCount = corporaciones.TotalCount,
                PageSize = corporaciones.PageSize,
                CurrentPage = corporaciones.CurrentPage,
                TotalPages = corporaciones.TotalPages,
                HasNextPage = corporaciones.HasNextPage,
                HasPreviousPage = corporaciones.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<corporacionesDto>>(corporacionesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una corporacion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<corporacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoClientes(int id)
        {
            var corporaciones = await _corporacionesService.GetCorporacion(id);
            var corporacionesDto = _mapper.Map<corporacionesDto>(corporaciones);

            var response = new AguilaResponse<corporacionesDto>(corporacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva corporación
        /// </summary>
        /// <param name="corporacionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<corporacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(corporacionesDto corporacionDto)
        {
            var corporacion = _mapper.Map<corporaciones>(corporacionDto);
            await _corporacionesService.InsertCorporacion(corporacion);

            corporacionDto = _mapper.Map<corporacionesDto>(corporacion);
            var response = new AguilaResponse<corporacionesDto>(corporacionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una ubicación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="corporacionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<corporacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, corporacionesDto corporacionDto)
        {
            var corporacion = _mapper.Map<corporaciones>(corporacionDto);
            corporacion.id = id;

            var result = await _corporacionesService.UpdateCorporacion(corporacion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una corporación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<corporacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _corporacionesService.DeleteCorporacion(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Corporaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _corporacionesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
