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
    public class rutasController : ControllerBase
    {
        private readonly IrutasService _rutasService;
        private readonly IubicacionesService _ubicacionesService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IpaisesService _paisesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public rutasController(IrutasService rutasService, IMapper mapper, IPasswordService password,
                               IubicacionesService ubicacionesService,
                               ImunicipiosService municipiosService,
                               IdepartamentosService departamentosService,
                               IpaisesService paisesService)
        {
            _rutasService = rutasService;
            _ubicacionesService = ubicacionesService;
            _municipiosService = municipiosService;
            _departamentosService = departamentosService;
            _paisesService = paisesService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene las rutas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<rutasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRutas([FromQuery] rutasQueryFilter filter)
        {
            var rutas = _rutasService.GetRutas(filter);
            var rutasDto = _mapper.Map<IEnumerable<rutasDto>>(rutas);

            foreach (var ruta in rutasDto)
            {
                //Get de objeto ubicaciones
                var ubicacionRutaOrigen = await _ubicacionesService.GetUbicacion(ruta.idUbicacionOrigen);
                var ubicacionOrigenDto = _mapper.Map<ubicacionesDto>(ubicacionRutaOrigen);
                var ubicacionRutaDestino = await _ubicacionesService.GetUbicacion(ruta.idUbicacionDestino);
                var ubicacionDestinoDto = _mapper.Map<ubicacionesDto>(ubicacionRutaDestino);

                //Get de objetos origen
                var municipios = await _municipiosService.GetMunicipio(ubicacionOrigenDto.idMunicipio);
                var departamento = await _departamentosService.GetDepartamento(municipios.idDepartamento);
                var pais = await _paisesService.GetPais(departamento.idPais);

                //Get de objetos destino
                var municipiosD = await _municipiosService.GetMunicipio(ubicacionDestinoDto.idMunicipio);
                var departamentoD = await _departamentosService.GetDepartamento(municipios.idDepartamento);
                var paisD = await _paisesService.GetPais(departamento.idPais);

                //Se valida si es puerto o no para setear el comentario 
                if (ubicacionRutaOrigen.esPuerto == true)
                {
                    ruta.vUbicacionOrigen = ubicacionRutaOrigen.lugar + ", " + municipios.nombreMunicipio + ", " +
                                                                departamento.nombre + ", " + pais.Nombre + ", Es puerto";
                } else
                {
                    ruta.vUbicacionOrigen = ubicacionRutaOrigen.lugar + ", " + pais.Nombre + ", " +
                                                                departamento.nombre + ", " + pais.Nombre;
                }

                if (ubicacionRutaDestino.esPuerto == true)
                {
                    ruta.vUbicacionDestino = ubicacionRutaDestino.lugar + ", " + municipiosD.nombreMunicipio + ", " +
                                                departamentoD.nombre+ ", " + paisD.Nombre + ", Es puerto";
                } else
                {
                    ruta.vUbicacionDestino = ubicacionRutaDestino.lugar + ", " + municipiosD.nombreMunicipio + ", " +
                                                departamentoD.nombre + ", " + paisD.Nombre;
                }

            }

            var metadata = new Metadata
            {
                TotalCount = rutas.TotalCount,
                PageSize = rutas.PageSize,
                CurrentPage = rutas.CurrentPage,
                TotalPages = rutas.TotalPages,
                HasNextPage = rutas.HasNextPage,
                HasPreviousPage = rutas.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<rutasDto>>(rutasDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una ruta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<rutasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDepartamentos(int id)
        {
            var rutas = await _rutasService.GetRuta(id);
            var rutasDto = _mapper.Map<rutasDto>(rutas);

            if (rutas == null)
            {
                throw new AguilaException("Ruta No Existente", 404);
            }

            //Get de objeto ubicaciones
            var ubicacionRutaOrigen = await _ubicacionesService.GetUbicacion(rutasDto.idUbicacionOrigen);
            var ubicacionOrigenDto = _mapper.Map<ubicacionesDto>(ubicacionRutaOrigen);
            var ubicacionRutaDestino = await _ubicacionesService.GetUbicacion(rutasDto.idUbicacionDestino);
            var ubicacionDestinoDto = _mapper.Map<ubicacionesDto>(ubicacionRutaDestino);

            //Get de objetos origen
            var municipios = await _municipiosService.GetMunicipio(ubicacionOrigenDto.idMunicipio);
            var departamento = await _departamentosService.GetDepartamento(municipios.idDepartamento);
            var pais = await _paisesService.GetPais(departamento.idPais);

            //Get de objetos destino
            var municipiosD = await _municipiosService.GetMunicipio(ubicacionDestinoDto.idMunicipio);
            var departamentoD = await _departamentosService.GetDepartamento(municipios.idDepartamento);
            var paisD = await _paisesService.GetPais(departamento.idPais);

            //Se valida si es puerto o no para setear el comentario 
            if (ubicacionRutaOrigen.esPuerto == true)
            {
                rutasDto.vUbicacionOrigen = ubicacionRutaOrigen.lugar + ", " + municipios.nombreMunicipio + ", " +
                                                            departamento.nombre + ", " + pais.Nombre + ", Es puerto";
            }
            else
            {
                rutasDto.vUbicacionOrigen = ubicacionRutaOrigen.lugar + ", " + pais.Nombre + ", " +
                                                            departamento.nombre + ", " + pais.Nombre;
            }

            if (ubicacionRutaDestino.esPuerto == true)
            {
                rutasDto.vUbicacionDestino = ubicacionRutaDestino.lugar + ", " + municipiosD.nombreMunicipio + ", " +
                                            departamentoD.nombre + ", " + paisD.Nombre + ", Es puerto";
            }
            else
            {
                rutasDto.vUbicacionDestino = ubicacionRutaDestino.lugar + ", " + municipiosD.nombreMunicipio + ", " +
                                            departamentoD.nombre + ", " + paisD.Nombre;
            }

            var response = new AguilaResponse<rutasDto>(rutasDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva ruta
        /// </summary>
        /// <param name="rutaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<rutasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(rutasDto rutaDto)
        {
            var ruta = _mapper.Map<rutas>(rutaDto);
            var ubicacionOrigen = await _ubicacionesService.GetUbicacion(rutaDto.idUbicacionOrigen);
            var ubicacionDestino = await _ubicacionesService.GetUbicacion(rutaDto.idUbicacionDestino);

            if (ubicacionOrigen == null)
            {
                throw new AguilaException("Ubicación de origen No Existente", 404);
            }

            if (ubicacionDestino == null)
            {
                throw new AguilaException("Ubicación de destino No Existente", 404);
            }

            await _rutasService.InsertRuta(ruta);

            rutaDto = _mapper.Map<rutasDto>(ruta);
            var response = new AguilaResponse<rutasDto>(rutaDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una ruta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rutaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<rutasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, rutasDto rutaDto)
        {
            var ruta = _mapper.Map<rutas>(rutaDto);
            ruta.id = id;

            var ubicacionOrigen = await _ubicacionesService.GetUbicacion(rutaDto.idUbicacionOrigen);
            var ubicacionDestino = await _ubicacionesService.GetUbicacion(rutaDto.idUbicacionDestino);

            if (ubicacionOrigen == null)
            {
                throw new AguilaException("Ubicación de origen No Existente", 404);
            }

            if (ubicacionDestino == null)
            {
                throw new AguilaException("Ubicación de destino No Existente", 404);
            }

            var result = await _rutasService.UpdateRuta(ruta);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una ruta, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<rutasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rutasService.DeleteRuta(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Rutas/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _rutasService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
