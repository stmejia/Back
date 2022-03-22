using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursosAtributosController : ControllerBase
    {
        private readonly IRecursosAtributosService _recursosAtributosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public RecursosAtributosController(IRecursosAtributosService recursosAtributosService, 
                                            IMapper mapper, IPasswordService passwordService)
        {
            _recursosAtributosService = recursosAtributosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        //devuelve los recursosAtributos Existentes en la tabla RecursosAtributos

        /// <summary>
        /// Extraccion total de RecursosAtributos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<RecursosAtributosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRecursosAtributos([FromQuery] RecursosAtributosQueryFilter filter)
        {
            var recursos =  _recursosAtributosService.GetRecursosAtributos(filter);
            var recursosDto = _mapper.Map<IEnumerable<RecursosAtributosDto>>(recursos);

            var metadata = new Metadata
            {
                TotalCount = recursos.TotalCount,
                PageSize = recursos.PageSize,
                CurrentPage = recursos.CurrentPage,
                TotalPages = recursos.TotalPages,
                HasNextPage = recursos.HasNextPage,
                HasPreviousPage = recursos.HasPreviousPage,

            };

            var response = new AguilaResponse<IEnumerable<RecursosAtributosDto>>(recursosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        //devuelve un recursoAtributo por su ID

        /// <summary>
        /// Consulta de RecursoAtributo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<RecursosAtributosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso(int id)
        {
            var recurso = await _recursosAtributosService.GetRecursoAtributo(id);
            var recursoDto = _mapper.Map<RecursosAtributosDto>(recurso);

            var response = new AguilaResponse<RecursosAtributosDto>(recursoDto);
            return Ok(response);
        }

        //inserta un nuevo RecursoAtributo

        /// <summary>
        /// Crear RecursoAtributo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<RecursosAtributosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(RecursosAtributosDto recursosDTo)
        {
            var recurso = _mapper.Map<RecursosAtributos>(recursosDTo);
            await _recursosAtributosService.InsertRecursoAtributo(recurso);

            recursosDTo = _mapper.Map<RecursosAtributosDto>(recurso);
            var response = new AguilaResponse<RecursosAtributosDto>(recursosDTo);
            return Ok(response);
        }

        //edita un recursoAtributo

        /// <summary>
        /// Actualizacion de Recurso Atributo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recursoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, RecursosAtributosDto recursoDTo)
        {
            var recurso = _mapper.Map<RecursosAtributos>(recursoDTo);
            recurso.Id = id;

            var result = await _recursosAtributosService.updateRecursoAtributo(recurso);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        //elimina un recursoAtributo por medio de su ID

        /// <summary>
        /// Eliminar RecursoAtributo, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _recursosAtributosService.DeleteRecursoAtributo(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/RecursosAtributos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _recursosAtributosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
