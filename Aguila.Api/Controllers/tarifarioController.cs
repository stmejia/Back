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
    public class tarifarioController : ControllerBase
    {
        private readonly ItarifarioService _tarifarioService;
        private readonly IserviciosService _serviciosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public tarifarioController(ItarifarioService tarifarioService, IMapper mapper, IPasswordService passwordService,
                                   IserviciosService serviciosService)
        {
            _tarifarioService = tarifarioService;
            _serviciosService = serviciosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene las tarifas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tarifarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTarifario([FromQuery] tarifarioQueryFilter filter)
        {
            var tarifario = _tarifarioService.GetTarifario(filter);
            var tarifarioDto = _mapper.Map<IEnumerable<tarifarioDto>>(tarifario);

            foreach (var servicio in tarifarioDto)
            {
                //Get de objetos
                var tarifaServicio = await _serviciosService.GetServicio(servicio.idServicio);
                var tarifaServicioDto = _mapper.Map<serviciosDto>(tarifaServicio);

                //Set de objetos
                servicio.servicio = tarifaServicioDto;
            }

            var metadata = new Metadata
            {
                TotalCount = tarifario.TotalCount,
                PageSize = tarifario.PageSize,
                CurrentPage = tarifario.CurrentPage,
                TotalPages = tarifario.TotalPages,
                HasNextPage = tarifario.HasNextPage,    
                HasPreviousPage = tarifario.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<tarifarioDto>>(tarifarioDto)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tarifarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTarifario(int id)
        {
            var tarifario = await _tarifarioService.GetTarifario(id);
            var tarifarioDto = _mapper.Map<tarifarioDto>(tarifario);

            var response = new AguilaResponse<tarifarioDto>(tarifarioDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva tarifa
        /// </summary>
        /// <param name="tarifarioDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tarifarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tarifarioDto tarifarioDto)
        {
            var tarifario = _mapper.Map<tarifario>(tarifarioDto);
            var tarifaServicio = await _serviciosService.GetServicio(tarifario.idServicio);

            //Valida si no es una tarifa estándar asigna los campos vacíos
            if (tarifaServicio.ruta == false)
            {
                tarifario.idUbicacionOrigen = null;
                tarifario.idUbicacionDestino = null;
                tarifario.idRuta = null;
            }

            await _tarifarioService.InsertTarifario(tarifario);

            tarifarioDto = _mapper.Map<tarifarioDto>(tarifario);
            var response = new AguilaResponse<tarifarioDto>(tarifarioDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una tarifa, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tarifarioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tarifarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tarifarioDto tarifarioDto)
        {
            var tarifario = _mapper.Map<tarifario>(tarifarioDto);
            tarifario.id = id;

            var result = await _tarifarioService.UpdateTarifario(tarifario);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una tarifa, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tarifarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tarifarioService.DeleteTarifario(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Tarifario/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _tarifarioService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
