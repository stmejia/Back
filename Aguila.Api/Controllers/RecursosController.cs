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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecursosController : ControllerBase
    {
        private readonly IRecursosService _recursosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public RecursosController(IRecursosService recursosService, IMapper mapper, IPasswordService passwordService)
        {
            _recursosService = recursosService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Recursos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<RecursosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRecursos([FromQuery] RecursosQueryFilter filter)
        {
            var recursos =  _recursosService.GetRecursos(filter);
            var recursosDto = _mapper.Map<IEnumerable<RecursosDto>>(recursos);

            var metadata = new Metadata
            {
                TotalCount = recursos.TotalCount,
                PageSize = recursos.PageSize,
                CurrentPage = recursos.CurrentPage,
                TotalPages = recursos.TotalPages,
                HasNextPage = recursos.HasNextPage,
                HasPreviousPage = recursos.HasPreviousPage,
               
            };

            foreach(var recurso in recursosDto)
            {
                recurso.opciones = null;
            }

            var response = new AguilaResponse<IEnumerable<RecursosDto>>(recursosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Extraccion total de Recursos, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("general")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<List<RecursosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRecursosGeneral([FromQuery] RecursosQueryFilter filter)
        {
            var recursos = _recursosService.GetRecursosGeneral(filter);
            var recursosDto = _mapper.Map<List<RecursosDto>>(recursos);
           

            var response = new AguilaResponse<List<RecursosDto>>(recursosDto);

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Recurso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<RecursosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso(int id)
        {
            var recurso = await _recursosService.GetRecurso(id);
            var recursoDto = _mapper.Map<RecursosDto>(recurso);
            //el string de opciones separados por coma se convierte en una lista para poblar la coleccion
            recursoDto.opciones = recurso.opciones.Split(',').ToList();

            var response = new AguilaResponse<RecursosDto>(recursoDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Recurso Nuevo
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<RecursosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(RecursosDto recursosDTo)
        {
            var recurso = _mapper.Map<Recursos>(recursosDTo);
            recurso.opciones = "";

            //la coleccion de opciones se transforma a un string separado por comas
            foreach (var opcion in recursosDTo.opciones)
            {
                recurso.opciones += opcion + ",";
            }

            await _recursosService.InsertRecurso(recurso);
            recursosDTo = _mapper.Map<RecursosDto>(recurso);

            //el string de opciones separados por coma se convierte en una lista para poblar la coleccion
            recursosDTo.opciones = recurso.opciones.Split(',').ToList();

            var response = new AguilaResponse<RecursosDto>(recursosDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Recurso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recursoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, RecursosDto recursoDTo)
        {
            var recurso = _mapper.Map<Recursos>(recursoDTo);
            recurso.opciones = "";
            recurso.Id = id;

            //la coleccion de opciones se transforma a un string separado por comas
            foreach (var opcion in recursoDTo.opciones)
            {
                recurso.opciones += opcion + ",";
            }

            var result = await _recursosService.updateRecurso(recurso);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Recurso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _recursosService.DeleteRecurso(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/Recursos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _recursosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
