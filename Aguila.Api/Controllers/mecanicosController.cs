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
    public class mecanicosController : ControllerBase
    {
        private readonly ImecanicosService _mecanicosService;
        private readonly ItipoMecanicosService _tipoMecanicosService;
        private readonly IempleadosService _empleadosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public mecanicosController(ImecanicosService mecanicosService, IMapper mapper, IPasswordService passwordService,
                                   ItipoMecanicosService tipoMecanicosService,
                                   IempleadosService empleadosService)
        {
            _mecanicosService = mecanicosService;
            _tipoMecanicosService = tipoMecanicosService;
            _empleadosService = empleadosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene los mecanicos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<mecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMecanicos([FromQuery] mecanicosQueryFilter filter)
        {
            var mecanicos = _mecanicosService.GetMecanicos(filter);
            var mecanicosDto = _mapper.Map<IEnumerable<mecanicosDto>>(mecanicos);

            foreach (var mecanico in mecanicosDto)
            {
                //Get de objetos
                var empleados = await _empleadosService.GetEmpleado(mecanico.idEmpleado);
                var tipoMecanico = await _tipoMecanicosService.GetTipoMecanico(mecanico.idTipoMecanico);
                var tipoMecanicoDto = _mapper.Map<tipoMecanicosDto>(tipoMecanico);

                //Set de objetos
                mecanico.vNombreEmpleado = empleados.nombres;
                mecanico.tipoMecanico = tipoMecanicoDto;
            }

            var metadata = new Metadata
            {
                TotalCount = mecanicos.TotalCount,
                PageSize = mecanicos.PageSize,
                CurrentPage = mecanicos.CurrentPage,
                TotalPages = mecanicos.TotalPages,
                HasNextPage = mecanicos.HasNextPage,
                HasPreviousPage = mecanicos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<mecanicosDto>>(mecanicosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un mecanico por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<mecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMecanico(int id)
        {
            var mecanicos = await _mecanicosService.GetMecanico(id);
            var mecanicosDto = _mapper.Map<mecanicosDto>(mecanicos);

            var response = new AguilaResponse<mecanicosDto>(mecanicosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo mecanico
        /// </summary>
        /// <param name="mecanicoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<mecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(mecanicosDto mecanicoDto)
        {
            var mecanico = _mapper.Map<mecanicos>(mecanicoDto);
            var tipoMecanico = await _tipoMecanicosService.GetTipoMecanico(mecanico.idTipoMecanico);
            var empleado = await _empleadosService.GetEmpleado(mecanico.idEmpleado);

            if (tipoMecanico == null)
            {
                throw new AguilaException("Tipo de mecanico No Existente", 404);
            }

            if (empleado == null)
            {
                throw new AguilaException("Empleado No Existente", 404);
            }

            await _mecanicosService.InsertMecanico(mecanico);

            mecanicoDto = _mapper.Map<mecanicosDto>(mecanico);
            var response = new AguilaResponse<mecanicosDto>(mecanicoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un mecanico, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mecanicoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<mecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, mecanicosDto mecanicoDto)
        {
            var mecanico = _mapper.Map<mecanicos>(mecanicoDto);
            mecanico.id = id;

            var tipoMecanico = await _tipoMecanicosService.GetTipoMecanico(mecanico.idTipoMecanico);
            var empleado = await _empleadosService.GetEmpleado(mecanico.idEmpleado);

            if (tipoMecanico == null)
            {
                throw new AguilaException("Tipo de mecanico No Existente", 404);
            }

            if (empleado == null)
            {
                throw new AguilaException("Empleado No Existente", 404);
            }

            var result = await _mecanicosService.UpdateMecanico(mecanico);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un mecanico, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<mecanicosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mecanicosService.DeleteMecanico(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Mecanicos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _mecanicosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
