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
    public class tipoClientesController : ControllerBase
    {
        private readonly ItipoClientesService _tipoClientesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoClientesController(ItipoClientesService tipoClientesService, IMapper mapper, IPasswordService password)
        {
            _tipoClientesService = tipoClientesService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los tipos de clientes registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoClientesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTipoClientes([FromQuery] tipoClientesQueryFilter filter)
        {
            var tipoClientes = _tipoClientesService.GetTipoClientes(filter);
            var tipoClientesDto = _mapper.Map<IEnumerable<tipoClientesDto>>(tipoClientes);

            var metadata = new Metadata
            {
                TotalCount = tipoClientes.TotalCount,
                PageSize = tipoClientes.PageSize,
                CurrentPage = tipoClientes.CurrentPage,
                TotalPages = tipoClientes.TotalPages,
                HasNextPage = tipoClientes.HasNextPage,
                HasPreviousPage = tipoClientes.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<tipoClientesDto>>(tipoClientesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de cliente por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoClientesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoClientes(int id)
        {
            var tipoClientes = await _tipoClientesService.GetTipoCliente(id);
            var tipoClientesDto = _mapper.Map<tipoClientesDto>(tipoClientes);

            var response = new AguilaResponse<tipoClientesDto>(tipoClientesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo de cliente
        /// </summary>
        /// <param name="tipoClienteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoClientesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoClientesDto tipoClienteDto)
        {
            var tipoCliente = _mapper.Map<tipoClientes>(tipoClienteDto);
            await _tipoClientesService.InsertTipoCliente(tipoCliente);

            tipoClienteDto = _mapper.Map<tipoClientesDto>(tipoCliente);
            var response = new AguilaResponse<tipoClientesDto>(tipoClienteDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo de cliente, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoClienteDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoClientesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoClientesDto tipoClienteDto)
        {
            var tipoCliente = _mapper.Map<tipoClientes>(tipoClienteDto);
            tipoCliente.id = id;

            var result = await _tipoClientesService.UpdateTipoCliente(tipoCliente);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de cliente, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoClientesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoClientesService.DeleteTipoCliente(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/TipoClientes/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tipoClientesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
