using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
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
    public class tiposListaController : ControllerBase
    {
        private readonly ItiposListaService _tiposListaService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;


        public tiposListaController(ItiposListaService tiposListaService, IMapper mapper, IPasswordService passwordService)
        {
            _tiposListaService = tiposListaService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de tipos Lista, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<tiposListaDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTiposLista([FromQuery] tiposListaQueryFilter filter)
        {
            var tipos = _tiposListaService.GetTiposLista(filter);
            var tiposDto = _mapper.Map<IEnumerable<tiposListaDto>>(tipos);

            var metadata = new Metadata
            {
                TotalCount = tipos.TotalCount,
                PageSize = tipos.PageSize,
                CurrentPage = tipos.CurrentPage,
                TotalPages = tipos.TotalPages,
                HasNextPage = tipos.HasNextPage,
                HasPreviousPage = tipos.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<tiposListaDto>>(tiposDto)
            {
                Meta = metadata
            };

            return Ok(response);            
        }

        /// <summary>
        /// Consulta de tipo Lista, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<tiposListaDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTipoLista(int id)
        {
            var tipo = await _tiposListaService.GetTipoLista(id);
            var tipoDto = _mapper.Map<tiposListaDto>(tipo);

            var response = new AguilaResponse<tiposListaDto>(tipoDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear tipo Lista Nuevo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<tiposListaDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(tiposListaDto tipoDTo)
        {
            var tipo = _mapper.Map<tiposLista>(tipoDTo);
            await _tiposListaService.InsertTipoLista(tipo);

            tipoDTo = _mapper.Map<tiposListaDto>(tipo);
            var response = new AguilaResponse<tiposListaDto>(tipoDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de tipo Lista, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, tiposListaDto tipoDTo)
        {
            var tipo = _mapper.Map<tiposLista>(tipoDTo);
            tipo.id = id;

            var result = await _tiposListaService.UpdateTipoLista(tipo);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar tipo Lista, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _tiposListaService.DeleteTipoLista(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }



        /// <summary>
        /// Devuelve los objetos de una lista mediante el el id de tipolista y campo
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("lista")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<listasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetLista([FromQuery] tiposListaQueryFilter filter)
        {
            var lista = _tiposListaService.GetLista(filter);
            var listaDto = _mapper.Map<IEnumerable<listasDto>>(lista);

            var metadata = new Metadata
            {
                TotalCount = lista.TotalCount,
                PageSize = 1000,
                CurrentPage = lista.CurrentPage,
                TotalPages = lista.TotalPages,
                HasNextPage = lista.HasNextPage,
                HasPreviousPage = lista.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<listasDto>>(listaDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/tiposLista/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _tiposListaService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
