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
    public class tipoMecanicosController : ControllerBase
    {
        private readonly ItipoMecanicosService _tipoMecanicosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoMecanicosController(ItipoMecanicosService tipoMecanicosService, IMapper mapper, IPasswordService password)
        {
            _tipoMecanicosService = tipoMecanicosService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los tipos de movimientos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoMecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTipoMecanicos([FromQuery] tipoMecanicosQueryFilter filter)
        {
            var tipoMecanicos = _tipoMecanicosService.GetTipoMecanicos(filter);
            var tipoMecanicosDto = _mapper.Map<IEnumerable<tipoMecanicosDto>>(tipoMecanicos);

            var metadata = new Metadata
            {
                TotalCount = tipoMecanicos.TotalCount,
                PageSize = tipoMecanicos.PageSize,
                CurrentPage = tipoMecanicos.CurrentPage,
                TotalPages = tipoMecanicos.TotalPages,
                HasNextPage = tipoMecanicos.HasNextPage,
                HasPreviousPage = tipoMecanicos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<tipoMecanicosDto>>(tipoMecanicosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de movimiento por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoMecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoMecanico(int id)
        {
            var tipoMecanicos = await _tipoMecanicosService.GetTipoMecanico(id);
            var tipoMecanicosDto = _mapper.Map<tipoMecanicosDto>(tipoMecanicos);

            var response = new AguilaResponse<tipoMecanicosDto>(tipoMecanicosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo de movimiento
        /// </summary>
        /// <param name="tipoMecanicoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoMecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoMecanicosDto tipoMecanicoDto)
        {
            var tipoMecanico = _mapper.Map<tipoMecanicos>(tipoMecanicoDto);
            await _tipoMecanicosService.InsertTipoMecanico(tipoMecanico);

            tipoMecanicoDto = _mapper.Map<tipoMecanicosDto>(tipoMecanico);
            var response = new AguilaResponse<tipoMecanicosDto>(tipoMecanicoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo de movimiento, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoMecanicoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoMecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoMecanicosDto tipoMecanicoDto)
        {
            var tipoMecanico = _mapper.Map<tipoMecanicos>(tipoMecanicoDto);
            tipoMecanico.id = id;

            var result = await _tipoMecanicosService.UpdateTipoMecanico(tipoMecanico);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de movimiento, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoMecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoMecanicosService.DeleteTipoMecanico(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/TipoMovimientos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tipoMecanicosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
