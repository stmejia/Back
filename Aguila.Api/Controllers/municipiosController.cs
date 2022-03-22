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
    public class municipiosController : ControllerBase
    {
        private readonly ImunicipiosService _municipiosService;
        private readonly IubicacionesService _ubicacionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public municipiosController(ImunicipiosService municipiosService, IMapper mapper, IPasswordService password,
                                    IubicacionesService ubicacionesService)
        {
            _municipiosService = municipiosService;
            _ubicacionesService = ubicacionesService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los municipios registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<municipiosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetMunicipios([FromQuery] municipiosQueryFilter filter)
        {
            var municipios = _municipiosService.GetMunicipio(filter);
            var municipiosDto = _mapper.Map<IEnumerable<municipiosDto>>(municipios);

            var metadata = new Metadata
            {
                TotalCount = municipios.TotalCount,
                PageSize = municipios.PageSize,
                CurrentPage = municipios.CurrentPage,
                TotalPages = municipios.TotalPages,
                HasNextPage = municipios.HasNextPage,
                HasPreviousPage = municipios.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<municipiosDto>>(municipiosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un municipio por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<municipiosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMunicipios(int id)
        {
            var municipios = await _municipiosService.GetMunicipio(id);
            var municipiosDto = _mapper.Map<municipiosDto>(municipios);

            if (municipios == null)
            {
                throw new AguilaException("Municipio No Existente", 404);
            }

            //captura las ubicaciones asignadas al municipio
            //ubicacionesQueryFilter filterUbicaciones = new ubicacionesQueryFilter
            //{
            //    idMunicipio = id
            //};

            //var municipioUbicacion = _ubicacionesService.GetUbicaciones(filterUbicaciones);
            //foreach (var ubicacion in municipioUbicacion)
            //{
            //    var currentUbicacion = await _ubicacionesService.GetUbicacion(ubicacion.id);
            //    var currentUbicacionDto = _mapper.Map<ubicacionesDto>(currentUbicacion);
            //    municipiosDto.ubicaciones.Add(currentUbicacionDto);
            //}

            var response = new AguilaResponse<municipiosDto>(municipiosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo municipio
        /// </summary>
        /// <param name="municipioDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<municipiosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(municipiosDto municipioDto)
        {
            var municipios = _mapper.Map<municipios>(municipioDto);
            await _municipiosService.InsertMunicipio(municipios);

            municipioDto = _mapper.Map<municipiosDto>(municipios);
            var response = new AguilaResponse<municipiosDto>(municipioDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un municipio, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="municipioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<municipiosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, municipiosDto municipioDto)
        {
            var municipio = _mapper.Map<municipios>(municipioDto);
            municipio.id = id;

            var result = await _municipiosService.UpdateMunicipio(municipio);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        } 

        /// <summary>
        /// Elimina un municipio, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<municipiosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _municipiosService.DeleteMunicipio(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Municipios/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _municipiosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
