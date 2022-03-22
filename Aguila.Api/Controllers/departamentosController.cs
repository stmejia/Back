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
    public class departamentosController : ControllerBase
    {
        private readonly IdepartamentosService _departamentosService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public departamentosController(IdepartamentosService departamentosService, IMapper mapper, IPasswordService password,
                                       ImunicipiosService municipiosService)
        {
            _departamentosService = departamentosService;
            _municipiosService = municipiosService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los departamentos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<departamentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDepartamentos([FromQuery] departamentosQueryFilter filter)
        {
            var dptos = _departamentosService.GetDepartamento(filter);
            var dptosDto = _mapper.Map<IEnumerable<departamentosDto>>(dptos);

            var metadata = new Metadata
            {
                TotalCount = dptos.TotalCount,
                PageSize = dptos.PageSize,
                CurrentPage = dptos.CurrentPage,
                TotalPages = dptos.TotalPages,
                HasNextPage = dptos.HasNextPage,
                HasPreviousPage = dptos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<departamentosDto>>(dptosDto)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<departamentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDepartamentos(int id)
        {
            var dptos = await _departamentosService.GetDepartamento(id);
            var dptosDto = _mapper.Map<departamentosDto>(dptos);

            if (dptos == null)
            {
                throw new AguilaException("Departamento No Existente", 404);
            }

            //Captura los municipios asignados al departamento
            //municipiosQueryFilter filterMunicipios = new municipiosQueryFilter
            //{
            //    idDepartamento = id
            //};

            //var DptoMunicipio = _municipiosService.GetMunicipio(filterMunicipios);
            //foreach (var municipio in DptoMunicipio)
            //{
            //    var currentMunicipio = await _municipiosService.GetMunicipio(municipio.id);
            //    var currentMunicipioDto = _mapper.Map<municipiosDto>(currentMunicipio);
            //    dptosDto.municipios.Add(currentMunicipioDto);
            //}

            var response = new AguilaResponse<departamentosDto>(dptosDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo país
        /// </summary>
        /// <param name="departamentoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<departamentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(departamentosDto departamentoDto)
        {
            var dptos = _mapper.Map<departamentos>(departamentoDto);
            await _departamentosService.InsertDepartamento(dptos);

            departamentoDto = _mapper.Map<departamentosDto>(dptos);
            var response = new AguilaResponse<departamentosDto>(departamentoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un departamento, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departamentoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<departamentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, departamentosDto departamentoDto)
        {
            var dpto = _mapper.Map<departamentos>(departamentoDto);
            dpto.id = id;

            var result = await _departamentosService.UpdateDepartamento(dpto);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un departamento, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<departamentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _departamentosService.DeleteDepartamento(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Departamentos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _departamentosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
