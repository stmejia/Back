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
    public class clienteServiciosController : ControllerBase
    {
        private readonly IclienteServicioService _clienteServicioService;
        private readonly IserviciosService _serviciosService;
        private readonly IclientesService _clientesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public clienteServiciosController(IclienteServicioService clienteServicioService, IMapper mapper, IPasswordService password,
                                          IserviciosService serviciosService, IclientesService clientesService)
        {
            _clienteServicioService = clienteServicioService;
            _serviciosService = serviciosService;
            _clientesService = clientesService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los servicios registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteServiciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetClienteServicios([FromQuery] clienteServiciosQueryFilter filter)
        {
            var clienteServicios = _clienteServicioService.GetClienteServicios(filter);
            var clienteServiciosDto = _mapper.Map<IEnumerable<clienteServiciosDto>>(clienteServicios);

            var metadata = new Metadata
            {
                TotalCount = clienteServicios.TotalCount,
                PageSize = clienteServicios.PageSize,
                CurrentPage = clienteServicios.CurrentPage,
                TotalPages = clienteServicios.TotalPages,
                HasNextPage = clienteServicios.HasNextPage,
                HasPreviousPage = clienteServicios.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<clienteServiciosDto>>(clienteServiciosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un servicio por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteServiciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClienteServicio(int id)
        {
            var clienteServicios = await _clienteServicioService.GetClienteServicio(id);
            var clienteServiciosDto = _mapper.Map<clienteServiciosDto>(clienteServicios);

            if (clienteServicios == null)
            {
                throw new AguilaException("Servicio No Existente", 404);
            }

            var response = new AguilaResponse<clienteServiciosDto>(clienteServiciosDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo servicios
        /// </summary>
        /// <param name="clienteServicioDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteServiciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(clienteServiciosDto clienteServicioDto)
        {
            var clienteServicio = _mapper.Map<clienteServicios>(clienteServicioDto);
            var servicio = await _serviciosService.GetServicio(clienteServicioDto.idServicio);
            var cliente = await _clientesService.GetCliente(clienteServicioDto.idCliente);

            if (servicio == null)
            {
                throw new AguilaException("El id del servicio ingresado no es válido o no existe", 404);
            }

            await _clienteServicioService.InsertClienteServicio(clienteServicio);

            clienteServicioDto = _mapper.Map<clienteServiciosDto>(clienteServicio);
            var response = new AguilaResponse<clienteServiciosDto>(clienteServicioDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un servicio, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clienteServicioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteServiciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, clienteServiciosDto clienteServicioDto)
        {
            var clienteServicio = _mapper.Map<clienteServicios>(clienteServicioDto);
            clienteServicio.id = id;
            var servicio = await _serviciosService.GetServicio(clienteServicioDto.idServicio);
            var cliente = await _clientesService.GetCliente(clienteServicioDto.idCliente);

            if (servicio == null)
            {
                throw new AguilaException("El id del servicio ingresado no es válido o no existe", 404);
            }

            var result = await _clienteServicioService.UpdateClienteServicio(clienteServicio);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un servicio, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteServiciosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clienteServicioService.DeleteClienteServicio(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/ClienteServicios/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _clienteServicioService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
