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
    public class tipoGeneradoresController : ControllerBase
    {
        private readonly ItipoGeneradoresService _tipoGeneradoresService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoGeneradoresController(ItipoGeneradoresService tipoGeneradoresService, IMapper mapper, IPasswordService password)
        {
            _tipoGeneradoresService = tipoGeneradoresService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene todos los tipos de generadores registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoGeneradoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTipoGeneradores([FromQuery] tipoGeneradoresQueryFilter filter)
        {
            var tipoGeneradores = _tipoGeneradoresService.GetTipoGeneradores(filter);
            var tipoGeneradoresDto = _mapper.Map<IEnumerable<tipoGeneradoresDto>>(tipoGeneradores);

            var metadata = new Metadata
            {
                TotalCount = tipoGeneradores.TotalCount,
                PageSize = tipoGeneradores.PageSize,
                CurrentPage = tipoGeneradores.CurrentPage,
                TotalPages = tipoGeneradores.TotalPages,
                HasNextPage = tipoGeneradores.HasNextPage,
                HasPreviousPage = tipoGeneradores.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<tipoGeneradoresDto>>(tipoGeneradoresDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de generadores por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoGeneradoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoGenerador(int id)
        {
            var tipoGeneradores = await _tipoGeneradoresService.GetTipoGenerador(id);
            var tipoGeneradoresDto = _mapper.Map<tipoGeneradoresDto>(tipoGeneradores);

            var response = new AguilaResponse<tipoGeneradoresDto>(tipoGeneradoresDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo de generador
        /// </summary>
        /// <param name="tipoGeneradorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoGeneradoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoGeneradoresDto tipoGeneradorDto)
        {
            var tipoGenerador = _mapper.Map<tipoGeneradores>(tipoGeneradorDto);
            await _tipoGeneradoresService.InsertTipoGenerador(tipoGenerador);

            tipoGeneradorDto = _mapper.Map<tipoGeneradoresDto>(tipoGenerador);
            var response = new AguilaResponse<tipoGeneradoresDto>(tipoGeneradorDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo de generador, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoGeneradorDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoGeneradoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoGeneradoresDto tipoGeneradorDto)
        {
            var tipoGenerador = _mapper.Map<tipoGeneradores>(tipoGeneradorDto);
            tipoGenerador.id = id;

            var result = await _tipoGeneradoresService.UpdateTipoGenerador(tipoGenerador);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de generador, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoGeneradoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoGeneradoresService.DeleteTipoGenerador(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/TipoGeneradores/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tipoGeneradoresService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
