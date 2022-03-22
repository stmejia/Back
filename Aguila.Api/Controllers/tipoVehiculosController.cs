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
    public class tipoVehiculosController : ControllerBase
    {
        private readonly ItipoVehiculosService _tipoVehiculosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tipoVehiculosController(ItipoVehiculosService tipoVehiculosService, IMapper mapper, IPasswordService password)
        {
            _tipoVehiculosService = tipoVehiculosService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los tipos de vehículos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoVehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTipoVehiculos([FromQuery] tipoVehiculosQueryFilter filter)
        {
            var tipoVehiculos = _tipoVehiculosService.GetTipoVehiculos(filter);
            var tipoVehiculosDto = _mapper.Map<IEnumerable<tipoVehiculosDto>>(tipoVehiculos);

            var metadata = new Metadata
            {
                TotalCount = tipoVehiculos.TotalCount,
                PageSize = tipoVehiculos.PageSize,
                CurrentPage = tipoVehiculos.CurrentPage,
                TotalPages = tipoVehiculos.TotalPages,
                HasNextPage = tipoVehiculos.HasNextPage,
                HasPreviousPage = tipoVehiculos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<tipoVehiculosDto>>(tipoVehiculosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un tipo de vehículo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoVehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoVehiculo(int id)
        {
            var tipoVehiculo = await _tipoVehiculosService.GetTipoVehiculo(id);
            var tipoVehiculoDto = _mapper.Map<tipoVehiculosDto>(tipoVehiculo);

            var response = new AguilaResponse<tipoVehiculosDto>(tipoVehiculoDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo tipo de vehículo
        /// </summary>
        /// <param name="tipoVehiculoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoVehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tipoVehiculosDto tipoVehiculoDto)
        {
            var tipoVehiculo = _mapper.Map<tipoVehiculos>(tipoVehiculoDto);
            await _tipoVehiculosService.InsertTipoVehiculo(tipoVehiculo);

            tipoVehiculoDto = _mapper.Map<tipoVehiculosDto>(tipoVehiculo);
            var response = new AguilaResponse<tipoVehiculosDto>(tipoVehiculoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un tipo de cliente, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoVehiculoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoVehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tipoVehiculosDto tipoVehiculoDto)
        {
            var tipoVehiculo = _mapper.Map<tipoVehiculos>(tipoVehiculoDto);
            tipoVehiculo.id = id;

            var result = await _tipoVehiculosService.UpdateTipoVehiculo(tipoVehiculo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un tipo de vehículo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tipoVehiculosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoVehiculosService.DeleteTipoVehiculo(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/TipoVehiculos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tipoVehiculosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
