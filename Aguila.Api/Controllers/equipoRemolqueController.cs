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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class equipoRemolqueController : ControllerBase
    {
        private readonly IequipoRemolqueService _equipoRemolqueService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public equipoRemolqueController(IequipoRemolqueService equipoRemolqueService, IMapper mapper, 
                                        IPasswordService passwordService,
                                        IactivoOperacionesService activoOperacionesService,
                                        IImagenesRecursosService imagenesRecursosService)
        {
            _equipoRemolqueService = equipoRemolqueService;
            _mapper = mapper;
            _passwordService = passwordService;
            _activoOperacionesService = activoOperacionesService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene los equipos registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEquipoRemolque([FromQuery] equipoRemolqueQueryFilter filter)
        {
            var equipoRemolque = await  _equipoRemolqueService.GetEquipoRemolque(filter);
            var equipoRemolqueDto = _mapper.Map<IEnumerable<equipoRemolqueDto>>(equipoRemolque);

            //foreach (var equipo in equipoRemolqueDto)
            //{
            //    var currentActivoOperacion = await _activoOperacionesService.GetActivoOperacion(equipo.idActivo);
            //    equipo.activoOperacion = _mapper.Map<activoOperacionesDto>(currentActivoOperacion);
            //}

            var metadata = new Metadata
            {
                TotalCount = equipoRemolque.TotalCount,
                PageSize = equipoRemolque.PageSize,
                CurrentPage = equipoRemolque.CurrentPage,
                TotalPages = equipoRemolque.TotalPages,
                HasNextPage = equipoRemolque.HasNextPage,
                HasPreviousPage = equipoRemolque.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<equipoRemolqueDto>>(equipoRemolqueDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un equipo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEquipoRemolque(int id)
        {
            var equipoRemolque = await _equipoRemolqueService.GetEquipoRemolque(id);
            var equipoRemolqueDto = _mapper.Map<equipoRemolqueDto>(equipoRemolque);
                        
           var currentActivoOperacion = await _activoOperacionesService.GetActivoOperacion(equipoRemolqueDto.idActivo);
           equipoRemolqueDto.activoOperacion = _mapper.Map<activoOperacionesDto>(currentActivoOperacion);
            

            var response = new AguilaResponse<equipoRemolqueDto>(equipoRemolqueDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo equipo 
        /// </summary>
        /// <param name="equipoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(equipoRemolqueDto equipoDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            var result = await _equipoRemolqueService.InsertEquipoRemolque(equipoDto, usuario);
            var response = new AguilaResponse<equipoRemolqueDto>(result);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un equipo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equipoRemolqueDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, equipoRemolqueDto equipoRemolqueDto)
        {
            //var equipoRemolque = _mapper.Map<equipoRemolque>(equipoRemolqueDto);

            equipoRemolqueDto.idActivo = id;
            var result = await _equipoRemolqueService.UpdateEquipoRemolque(equipoRemolqueDto);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar un equipo, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<equipoRemolqueDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _equipoRemolqueService.DeleteEquipoRemolque(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        //[HttpGet("api/EquipoRemolque/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _equipoRemolqueService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve la configuracion de imagen para una propiedad del recurso, imagenTarjetaCirculacion
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpGet("ImagenConfiguracion/{propiedad}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ImagenRecursoConfiguracion>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImagenConfiguracion(string propiedad)
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);

            var response = new AguilaResponse<ImagenRecursoConfiguracion>(imgRecConf);
            return Ok(response);
        }
    }
}
