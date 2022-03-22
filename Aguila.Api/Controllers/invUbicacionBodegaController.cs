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
    public class invUbicacionBodegaController : ControllerBase
    {
        private readonly IinvUbicacionBodegaService _invUbicacionBodegaService;
        private readonly IEstacionesTrabajoService _estacionesTrabajoService;
        private readonly IproductosService _productosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public invUbicacionBodegaController(IinvUbicacionBodegaService invUbicacionBodegaService, IMapper mapper, IPasswordService passwordService,
                                            IEstacionesTrabajoService estacionesTrabajoService,
                                            IproductosService productosService)
        {
            _invUbicacionBodegaService = invUbicacionBodegaService;
            _estacionesTrabajoService = estacionesTrabajoService;
            _productosService = productosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las ubicaciones registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invUbicacionBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAsesores([FromQuery] invUbicacionBodegaQueryFilter filter)
        {
            var invUbicacionBodegas = _invUbicacionBodegaService.GetInvUbicacionBodega(filter);
            var invUbicacionBodegasDto = _mapper.Map<IEnumerable<invUbicacionBodegaDto>>(invUbicacionBodegas);

            var metadata = new Metadata
            {
                TotalCount = invUbicacionBodegas.TotalCount,
                PageSize = invUbicacionBodegas.PageSize,
                CurrentPage = invUbicacionBodegas.CurrentPage,
                TotalPages = invUbicacionBodegas.TotalPages,
                HasNextPage = invUbicacionBodegas.HasNextPage,
                HasPreviousPage = invUbicacionBodegas.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<invUbicacionBodegaDto>>(invUbicacionBodegasDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una ubicación por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invUbicacionBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsesor(int id)
        {
            var invUbicacionBodega = await _invUbicacionBodegaService.GetInvUbicacionBodega(id);
            var invUbicacionBodegaDto = _mapper.Map<invUbicacionBodegaDto>(invUbicacionBodega);

            var response = new AguilaResponse<invUbicacionBodegaDto>(invUbicacionBodegaDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva ubicación
        /// </summary>
        /// <param name="invUbicacionBodegaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invUbicacionBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(invUbicacionBodegaDto invUbicacionBodegaDto)
        {
            var invUbicacionBodega = _mapper.Map<invUbicacionBodega>(invUbicacionBodegaDto);
            await _invUbicacionBodegaService.InsertInvUbicacionBodega(invUbicacionBodega);

            invUbicacionBodegaDto = _mapper.Map<invUbicacionBodegaDto>(invUbicacionBodega);
            var response = new AguilaResponse<invUbicacionBodegaDto>(invUbicacionBodegaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una ubicación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invUbicacionBodegaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invUbicacionBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, invUbicacionBodegaDto invUbicacionBodegaDto)
        {
            var invUbicacionBodega = _mapper.Map<invUbicacionBodega>(invUbicacionBodegaDto);
            invUbicacionBodega.id = id;

            var result = await _invUbicacionBodegaService.UpdateInvUbicacionBodega(invUbicacionBodega);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una ubicación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invUbicacionBodegaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invUbicacionBodegaService.DeleteInvUbicacionBodega(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }


        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/InvUbicacionBodega/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _invUbicacionBodegaService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
