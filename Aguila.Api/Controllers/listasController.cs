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
    public class listasController : ControllerBase
    {
        private readonly IlistasService _listasService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public listasController(IlistasService listasService, IMapper mapper, IPasswordService passwordService )
        {
            _listasService = listasService;
            _mapper = mapper;
            _passwordService = passwordService;
        }


        /// <summary>
        /// Extraccion total de Listas, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<listasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetListas([FromQuery] listasQueryFilter filter)
        {
            var listas = _listasService.GetListas(filter);
            var listasDto = _mapper.Map<IEnumerable<listasDto>>(listas);

            var metadata = new Metadata
            {
                TotalCount = listas.TotalCount,
                PageSize = listas.PageSize,
                CurrentPage = listas.CurrentPage,
                TotalPages = listas.TotalPages,
                HasNextPage = listas.HasNextPage,
                HasPreviousPage = listas.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<listasDto>>(listasDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Lista, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<listasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTLista(int id)
        {
            var lista = await _listasService.GetLista(id);
            var listaDto = _mapper.Map<listasDto>(lista);

            var response = new AguilaResponse<listasDto>(listaDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Nueva Lista 
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<listasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(listasDto listaDTo)
        {
            var lista = _mapper.Map<listas>(listaDTo);
           
            await _listasService.InserLista(lista);

            listaDTo = _mapper.Map<listasDto>(lista);
            var response = new AguilaResponse<listasDto>(listaDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion Listas, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="listaDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, listasDto listaDTo)
        {
            var lista = _mapper.Map<listas>(listaDTo);
            lista.id = id;

            var result = await _listasService.UpdateLista(lista);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Listas, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _listasService.DeleteLista(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        [HttpGet("/api/listas/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _listasService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
