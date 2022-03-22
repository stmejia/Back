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
    public class paisesController : ControllerBase
    {
        private readonly IpaisesService _paisesService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public paisesController(IpaisesService paisesService, IMapper mapper, IPasswordService passwordService,
                                IdepartamentosService departamentosService)
        {
            _paisesService = paisesService;
            _departamentosService = departamentosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Obtiene todos los Paises registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<paisesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPaises([FromQuery] paisesQueryFilter filter)
        {
            var paises = _paisesService.GetPaises(filter);
            var paisesDto = _mapper.Map<IEnumerable<paisesDto>>(paises);

            var metadata = new Metadata
            {
                TotalCount = paises.TotalCount,
                PageSize = paises.PageSize,
                CurrentPage = paises.CurrentPage,
                TotalPages = paises.TotalPages,
                HasNextPage = paises.HasNextPage,
                HasPreviousPage = paises.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<paisesDto>>(paisesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un país por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<paisesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPais(int id)
        {
            var pais = await _paisesService.GetPais(id);
            var paisDto = _mapper.Map<paisesDto>(pais);

            if (pais == null)
            {
                throw new AguilaException("Pais No Existente", 404);
            }


            //captura los departamentos asignados al pais
            //departamentosQueryFilter filterDepartamentos = new departamentosQueryFilter
            //{
            //    idPais = id
            //};

            //var paisDepartamento = _departamentosService.GetDepartamento(filterDepartamentos);
            //foreach (var departamento in paisDepartamento)
            //{
            //    var currentDepartamento = await _departamentosService.GetDepartamento(departamento.id);
            //    var currentDepartamentoDto = _mapper.Map<departamentosDto>(currentDepartamento);
            //    paisDto.departamentos.Add(currentDepartamentoDto);
            //}

            var response = new AguilaResponse<paisesDto>(paisDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo país
        /// </summary>
        /// <param name="paisDTo"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<paisesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(paisesDto paisDTo)
        {
            var pais = _mapper.Map<paises>(paisDTo);
            await _paisesService.InsertPais(pais);

            paisDTo = _mapper.Map<paisesDto>(pais);
            var response = new AguilaResponse<paisesDto>(paisDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un pais, enviamos el id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paisDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(byte id, paisesDto paisDTo)
        {
            var pais = _mapper.Map<paises>(paisDTo);
            pais.Id = id;

            var result = await _paisesService.UpdatePais(pais);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un país, enviamos el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _paisesService.DeletePais(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/Paises/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _paisesService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
