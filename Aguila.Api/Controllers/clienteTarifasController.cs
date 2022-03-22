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
    public class clienteTarifasController : ControllerBase
    {
        private readonly IclienteTarifasService _clienteTarifasService;
        private readonly IclientesService _clientesService;
        private readonly ItarifarioService _tarifarioService;
        private readonly IserviciosService _serviciosService;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public clienteTarifasController(IclienteTarifasService clienteTarifasService, IMapper mapper, IPasswordService passwordService,
                                        IclientesService clientesService,
                                        ItarifarioService tarifarioService,
                                        IserviciosService serviciosService,
                                        IentidadComercialService entidadComercialService)
        {
            _clienteTarifasService = clienteTarifasService;
            _clientesService = clientesService;
            _tarifarioService = tarifarioService;
            _serviciosService = serviciosService;
            _entidadComercialService = entidadComercialService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las tarifas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteTarifasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClienteTarifas([FromQuery] clienteTarifasQueryFilter filter)
        {
            var clienteTarifas = _clienteTarifasService.GetClienteTarifas(filter);
            var clienteTarifasDto = _mapper.Map<IEnumerable<clienteTarifasDto>>(clienteTarifas);

            foreach (var clienteTarifa in clienteTarifasDto)
            {
                //Get de objetos
                var cliente = await _clientesService.GetCliente(clienteTarifa.idCliente);
                var clienteDto = _mapper.Map<clientesDto>(cliente);
                var entidadComercial = await _entidadComercialService.GetEntidadComercial(cliente.idEntidadComercial);
                var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);
                var tarifa = await _tarifarioService.GetTarifario(clienteTarifa.idTarifa);
                var tarifaDto = _mapper.Map<tarifarioDto>(tarifa);
                var servicio = await _serviciosService.GetServicio(tarifa.idServicio);
                var servicioDto = _mapper.Map<serviciosDto>(servicio);

                //Set de objetos
                clienteTarifa.cliente = clienteDto;
                clienteTarifa.tarifa = tarifaDto;
                clienteTarifa.tarifa.servicio = servicioDto;
                clienteTarifa.cliente.entidadComercial = entidadComercialDto;
            }

            var metadata = new Metadata
            {
                TotalCount = clienteTarifas.TotalCount,
                PageSize = clienteTarifas.PageSize,
                CurrentPage = clienteTarifas.CurrentPage,
                TotalPages = clienteTarifas.TotalPages,
                HasNextPage = clienteTarifas.HasNextPage,
                HasPreviousPage = clienteTarifas.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<clienteTarifasDto>>(clienteTarifasDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una tarifa por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteTarifasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClienteTarifa(int id)
        {
            var clienteTarifas = await _clienteTarifasService.GetClienteTarifa(id);
            var clienteTarifasDto = _mapper.Map<clienteTarifasDto>(clienteTarifas);

            var response = new AguilaResponse<clienteTarifasDto>(clienteTarifasDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva tarifa
        /// </summary>
        /// <param name="clienteTarifaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteTarifasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(clienteTarifasDto clienteTarifaDto)
        {
            var clienteTarifa = _mapper.Map<clienteTarifas>(clienteTarifaDto);
            await _clienteTarifasService.InsertClienteTarifa(clienteTarifa);

            clienteTarifaDto = _mapper.Map<clienteTarifasDto>(clienteTarifa);
            var response = new AguilaResponse<clienteTarifasDto>(clienteTarifaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una tarifa, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clienteTarifaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteTarifasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, clienteTarifasDto clienteTarifaDto)
        {
            var clienteTarifa = _mapper.Map<clienteTarifas>(clienteTarifaDto);
            clienteTarifa.id = id;

            var result = await _clienteTarifasService.UpdateClienteTarifa(clienteTarifa);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una tarifa, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<clienteTarifasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clienteTarifasService.DeleteClienteTarifa(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/ClienteTarifas/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _clienteTarifasService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
