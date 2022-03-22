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
    public class ubicacionesController : ControllerBase
    {
        private readonly IubicacionesService _ubicacionesService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IpaisesService _paisesService;
        private readonly IdireccionesService _direccionesService;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public ubicacionesController(IubicacionesService ubicacionesService, IMapper mapper, IPasswordService password,
                                     ImunicipiosService municipiosService,
                                     IdepartamentosService departamentosService,
                                     IpaisesService paisesService,
                                     IdireccionesService direccionesService,
                                     IentidadComercialService entidadComercialService,
                                     IEmpresaService empresaService)
        {
            _ubicacionesService = ubicacionesService;
            _municipiosService = municipiosService;
            _departamentosService = departamentosService;
            _paisesService = paisesService;
            _direccionesService = direccionesService;
            _entidadComercialService = entidadComercialService;
            _empresaService = empresaService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene las ubicaciónes registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ubicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUbicaciones([FromQuery] ubicacionesQueryFilter filter)
        {
            var ubicaciones = _ubicacionesService.GetUbicaciones(filter);
            var ubicacionesDto = _mapper.Map<IEnumerable<ubicacionesDto>>(ubicaciones);

            foreach (var ubicacion in ubicacionesDto)
            {
                //Get de objeto direcciónes
                var municipios = await _municipiosService.GetMunicipio(ubicacion.idMunicipio);
                var departamento = await _departamentosService.GetDepartamento(municipios.idDepartamento);
                var pais = await _paisesService.GetPais(departamento.idPais);

                ubicacion.vDireccion = ubicacion.lugar + ", " + municipios.nombreMunicipio + ", " +
                                                                departamento.nombre + ", " + pais.Nombre;
            }

            var metadata = new Metadata
            {
                TotalCount = ubicaciones.TotalCount,
                PageSize = ubicaciones.PageSize,
                CurrentPage = ubicaciones.CurrentPage,
                TotalPages = ubicaciones.TotalPages,
                HasNextPage = ubicaciones.HasNextPage,
                HasPreviousPage = ubicaciones.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<ubicacionesDto>>(ubicacionesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una ubicación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ubicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUbicaciones(int id)
        {
            var ubicaciones = await _ubicacionesService.GetUbicacion(id);
            var ubicacionesDto = _mapper.Map<ubicacionesDto>(ubicaciones);

            if (ubicaciones == null)
            {
                throw new AguilaException("Ubicación no existente", 404);
            }

            //Get Id's
            var municipioUbicacion = await _municipiosService.GetMunicipio(ubicaciones.idMunicipio);
            var departamento = await _departamentosService.GetDepartamento(municipioUbicacion.idDepartamento);
            var pais = await _paisesService.GetPais(departamento.idPais);

            //Set de los id de cada objeto
            ubicacionesDto.idDepartamento = departamento.id;
            ubicacionesDto.idPais = pais.Id;

            var response = new AguilaResponse<ubicacionesDto>(ubicacionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva ubicación
        /// </summary>
        /// <param name="ubicacionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ubicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(ubicacionesDto ubicacionDto)
        {
            var ubicaciones = _mapper.Map<ubicaciones>(ubicacionDto);
            var municipio = await _municipiosService.GetMunicipio(ubicacionDto.idMunicipio);
            var empresa = await _empresaService.GetEmpresa(ubicacionDto.idEmpresa.Value);

            if (municipio == null)
            {
                throw new AguilaException("Municipio No Existente", 404);
            }

            if (empresa == null)
            {
                throw new AguilaException("Empresa No Existente", 404);
            }

            await _ubicacionesService.InsertUbicacion(ubicaciones);

            ubicacionDto = _mapper.Map<ubicacionesDto>(ubicaciones);
            var response = new AguilaResponse<ubicacionesDto>(ubicacionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una ubicación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ubicacionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ubicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, ubicacionesDto ubicacionDto)
        {
            var ubicacion = _mapper.Map<ubicaciones>(ubicacionDto);
            ubicacion.id = id;

            var municipio = await _municipiosService.GetMunicipio(ubicacionDto.idMunicipio);
            var empresa = await _empresaService.GetEmpresa(ubicacionDto.idEmpresa.Value);

            if (municipio == null)
            {
                throw new AguilaException("Municipio No Existente", 404);
            }

            if (empresa == null)
            {
                throw new AguilaException("Empresa No Existente", 404);
            }

            var result = await _ubicacionesService.UpdateUbicacion(ubicacion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una ubicación, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ubicacionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ubicacionesService.DeleteUbicacion(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Ubicaciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _ubicacionesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
