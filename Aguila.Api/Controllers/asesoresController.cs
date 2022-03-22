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
    public class asesoresController : ControllerBase
    {
        private readonly IasesoresService _asesoresService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public asesoresController(IasesoresService asesoresService, IMapper mapper, IPasswordService passwordService)
        {
            _asesoresService = asesoresService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los asesores registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<asesoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAsesores([FromQuery] asesoresQueryFilter filter)
        {
            var asesores = _asesoresService.GetAsesores(filter);
            var asesoresDto = _mapper.Map<IEnumerable<asesoresDto>>(asesores);

            var metadata = new Metadata
            {
                TotalCount = asesores.TotalCount,
                PageSize = asesores.PageSize,
                CurrentPage = asesores.CurrentPage,
                TotalPages = asesores.TotalPages,
                HasNextPage = asesores.HasNextPage,
                HasPreviousPage = asesores.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<asesoresDto>>(asesoresDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un asesor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<asesoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsesor(int id)
        {
            var asesores = await _asesoresService.GetAsesor(id);
            var asesoresDto = _mapper.Map<asesoresDto>(asesores);

            var response = new AguilaResponse<asesoresDto>(asesoresDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo asesor
        /// </summary>
        /// <param name="asesorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<asesoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(asesoresDto asesorDto)
        {
            var asesor = _mapper.Map<asesores>(asesorDto);
            await _asesoresService.InsertAsesor(asesor);

            asesorDto = _mapper.Map<asesoresDto>(asesor);
            var response = new AguilaResponse<asesoresDto>(asesorDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un asesor, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asesorDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<asesoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, asesoresDto asesorDto)
        {
            var asesor = _mapper.Map<asesores>(asesorDto);
            asesor.id = id;

            var result = await _asesoresService.UpdateAsesor(asesor);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un asesor, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<asesoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _asesoresService.DeleteAsesor(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Asesores/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _asesoresService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
