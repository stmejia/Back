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
using Aguila.Core.Exceptions;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class medidasController : ControllerBase
    {
        private readonly ImedidasService _medidasService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public medidasController(ImedidasService medidasService, IMapper mapper, IPasswordService password)
        {
            _medidasService = medidasService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene las medidas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<medidasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetMedidas([FromQuery] medidasQueryFilter filter)
        {
            var medidas = _medidasService.GetMedidas(filter);
            var medidasDto = _mapper.Map<IEnumerable<medidasDto>>(medidas);

            var metadata = new Metadata
            {
                TotalCount = medidas.TotalCount,
                PageSize = medidas.PageSize,
                CurrentPage = medidas.CurrentPage,
                TotalPages = medidas.TotalPages,
                HasNextPage = medidas.HasNextPage,
                HasPreviousPage = medidas.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<medidasDto>>(medidasDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una medida por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<medidasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMedida(int id)
        {
            var medidas = await _medidasService.GetMedida(id);
            var medidasDto = _mapper.Map<medidasDto>(medidas);

            var response = new AguilaResponse<medidasDto>(medidasDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva medida
        /// </summary>
        /// <param name="medidaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<medidasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(medidasDto medidaDto)
        {
            var medida = _mapper.Map<medidas>(medidaDto);
            await _medidasService.InsertMedida(medida);

            medidaDto = _mapper.Map<medidasDto>(medida);
            var response = new AguilaResponse<medidasDto>(medidaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una medida, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="medidaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<medidasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, medidasDto medidaDto)
        {
            var medida = _mapper.Map<medidas>(medidaDto);
            medida.id = id;

            var result = await _medidasService.UpdateMedida(medida);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una medida, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<medidasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medidasService.DeleteMedida(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Medidas/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _medidasService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
