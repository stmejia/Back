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
    public class invSubCategoriaController : ControllerBase
    {
        private readonly IinvSubCategoriaService _invSubCategoriaService;
        private readonly IinvCategoriaService _invCategoriaService;
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public invSubCategoriaController(IinvSubCategoriaService invSubCategoriaService, IMapper mapper, IPasswordService passwordService,
                                         IEmpresaService empresaService,
                                         IinvCategoriaService invCategoriaService)
        {
            _invSubCategoriaService = invSubCategoriaService;
            _invCategoriaService = invCategoriaService;
            _empresaService = empresaService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las sub categorias registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invSubCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetInvSubCategoria([FromQuery] invSubCategoriaQueryFilter filter)
        {
            var invSubCategoria = _invSubCategoriaService.GetInvSubCategoria(filter);
            var invSubCategoriaDto = _mapper.Map<IEnumerable<invSubCategoriaDto>>(invSubCategoria);

            var metadata = new Metadata
            {
                TotalCount = invSubCategoria.TotalCount,
                PageSize = invSubCategoria.PageSize,
                CurrentPage = invSubCategoria.CurrentPage,
                TotalPages = invSubCategoria.TotalPages,
                HasNextPage = invSubCategoria.HasNextPage,
                HasPreviousPage = invSubCategoria.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<invSubCategoriaDto>>(invSubCategoriaDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una sub categoria por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invSubCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvCategoria(int id)
        {
            var invSubCategoria = await _invSubCategoriaService.GetInvSubCategoria(id);
            var invSubCategoriaDto = _mapper.Map<invSubCategoriaDto>(invSubCategoria);

            var response = new AguilaResponse<invSubCategoriaDto>(invSubCategoriaDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva sub categoria
        /// </summary>
        /// <param name="invSubCategoriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invSubCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(invSubCategoriaDto invSubCategoriaDto)
        {
            var invSubCategoria = _mapper.Map<invSubCategoria>(invSubCategoriaDto);
            await _invSubCategoriaService.InsertInvSubCategoria(invSubCategoria);

            invSubCategoriaDto = _mapper.Map<invSubCategoriaDto>(invSubCategoria);
            var response = new AguilaResponse<invSubCategoriaDto>(invSubCategoriaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una sub categoria, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invSubCategoriaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invSubCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, invSubCategoriaDto invSubCategoriaDto)
        {
            var invSubCategoria = _mapper.Map<invSubCategoria>(invSubCategoriaDto);
            invSubCategoria.id = id;

            var result = await _invSubCategoriaService.UpdateInvSubCategoria(invSubCategoria);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una categoria, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<invSubCategoriaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invSubCategoriaService.DeleteInvSubCategoria(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/invSubCategoria/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _invSubCategoriaService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
