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
    public class invCategoriaController : ControllerBase
    {
        private readonly IinvCategoriaService _invCategoriaService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public invCategoriaController(IinvCategoriaService invCategoriaService, IMapper mapper, IPasswordService passwordService)
        {
            _invCategoriaService = invCategoriaService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las categorias registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetInvCategoria([FromQuery] invCategoriaQueryFilter filter)
        {
            var invCategoria = _invCategoriaService.GetInvCategoria(filter);
            var invCategoriaDto = _mapper.Map<IEnumerable<invCategoriaDto>>(invCategoria);

            var metadata = new Metadata
            {
                TotalCount = invCategoria.TotalCount,
                PageSize = invCategoria.PageSize,
                CurrentPage = invCategoria.CurrentPage,
                TotalPages = invCategoria.TotalPages,
                HasNextPage = invCategoria.HasNextPage,
                HasPreviousPage = invCategoria.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<invCategoriaDto>>(invCategoriaDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una categoria por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvCategoria(int id)
        {
            var invCategoria = await _invCategoriaService.GetInvCategoria(id);
            var invCategoriaDto = _mapper.Map<invCategoriaDto>(invCategoria);

            var response = new AguilaResponse<invCategoriaDto>(invCategoriaDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva categoria
        /// </summary>
        /// <param name="invCategoriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(invCategoriaDto invCategoriaDto)
        {
            var invCategoria = _mapper.Map<invCategoria>(invCategoriaDto);
            await _invCategoriaService.InsertInvCategoria(invCategoria);

            invCategoriaDto = _mapper.Map<invCategoriaDto>(invCategoria);
            var response = new AguilaResponse<invCategoriaDto>(invCategoriaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una categoria, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invCategoriaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, invCategoriaDto invCategoriaDto)
        {
            var invCategoria = _mapper.Map<invCategoria>(invCategoriaDto);
            invCategoria.id = id;

            var result = await _invCategoriaService.UpdateInvCategoria(invCategoria);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una categoria, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invCategoriaService.DeleteInvCategoria(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/invCategoria/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _invCategoriaService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
