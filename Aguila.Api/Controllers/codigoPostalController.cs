using Aguila.Api.Responses;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
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
    public class codigoPostalController : ControllerBase
    {
        private readonly IcodigoPostalService _codigoPostalService;
        private readonly IMapper _mapper;

        public codigoPostalController(IcodigoPostalService codigoPostalService, IMapper mapper)
        {
            _codigoPostalService = codigoPostalService;
            _mapper = mapper;
        }

        /// <summary>
        /// Consulta de codigo postal, enviar id de Municipio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<codigoPostalDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCodigo(int id)
        {
            var codigo = await _codigoPostalService.getCodigo(id);
            var codigoDto = _mapper.Map<codigoPostalDto>(codigo);

            var response = new AguilaResponse<codigoPostalDto>(codigoDto);
            return Ok(response);
        }

        /// <summary>
        /// Consulta de codigo postal, enviar id de Municipio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("departamento/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<List<codigoPostalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCodigosPorDepartamento(int id)
        {
            var codigos =  _codigoPostalService.getCodigosDepartamento(id);
            var codigosDto = _mapper.Map<List<codigoPostalDto>>(codigos);

            var response = new AguilaResponse<List<codigoPostalDto>>(codigosDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/clientes/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _codigoPostalService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
