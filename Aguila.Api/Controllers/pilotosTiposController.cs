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
    public class pilotosTiposController : ControllerBase
    {
        private readonly IpilotosTiposService _pilotosTiposService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public pilotosTiposController(IpilotosTiposService pilotosTiposService, IMapper mapper, IPasswordService password)
        {
            _pilotosTiposService = pilotosTiposService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los tipos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDepartamentos([FromQuery] pilotosTiposQueryFilter filter)
        {
            var pilotosTipos = _pilotosTiposService.GetPilotosTipos(filter);
            var pilotosTiposDto = _mapper.Map<IEnumerable<pilotosTiposDto>>(pilotosTipos);

            var metadata = new Metadata
            {
                TotalCount = pilotosTipos.TotalCount,
                PageSize = pilotosTipos.PageSize,
                CurrentPage = pilotosTipos.CurrentPage,
                TotalPages = pilotosTipos.TotalPages,
                HasNextPage = pilotosTipos.HasNextPage,
                HasPreviousPage = pilotosTipos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<pilotosTiposDto>>(pilotosTiposDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<departamentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDepartamentos(int id)
        {
            var pilotosTipos = await _pilotosTiposService.GetPilotoTipo(id);
            var pilotosTiposDto = _mapper.Map<pilotosTiposDto>(pilotosTipos);

            if (pilotosTipos == null)
            {
                throw new AguilaException("Tipo No Existente", 404);
            }

            var response = new AguilaResponse<pilotosTiposDto>(pilotosTiposDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo
        /// </summary>
        /// <param name="pilotoTipoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(pilotosTiposDto pilotoTipoDto)
        {
            var pilotoTipo = _mapper.Map<pilotosTipos>(pilotoTipoDto);
            await _pilotosTiposService.InsertPilotoTipo(pilotoTipo);

            pilotoTipoDto = _mapper.Map<pilotosTiposDto>(pilotoTipo);
            var response = new AguilaResponse<pilotosTiposDto>(pilotoTipoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pilotoTipoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, pilotosTiposDto pilotoTipoDto)
        {
            var pilotoTipo = _mapper.Map<pilotosTipos>(pilotoTipoDto);
            pilotoTipo.id = id;

            var result = await _pilotosTiposService.UpdatePilotoTipo(pilotoTipo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosTiposDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pilotosTiposService.DeletePilotoTipo(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/PilotosTipos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _pilotosTiposService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
