using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class condicionContenedorController : ControllerBase
    {
        private readonly IcondicionContenedorService _condicionContenedorService;
        private readonly IMapper _mapper;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionContenedorController(IcondicionContenedorService condicionContenedorService, IMapper mapper,
                                             IestadosService estadosService, IImagenesRecursosService imagenesRecursosService)
        {
            _condicionContenedorService = condicionContenedorService;
            _mapper = mapper;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las condiciones de los contenedores registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionContenedorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionContenedor([FromQuery] condicionContenedorQueryFilter filter)
        {
            var condicionContenedor = await _condicionContenedorService.GetCondicionContenedor(filter);
            var condicionContenedorDto = _mapper.Map<IEnumerable<condicionContenedorDto>>(condicionContenedor);

            var metadata = new Metadata
            {
                TotalCount = condicionContenedor.TotalCount,
                PageSize = condicionContenedor.PageSize,
                CurrentPage = condicionContenedor.CurrentPage,
                TotalPages = condicionContenedor.TotalPages,
                HasNextPage = condicionContenedor.HasNextPage,
                HasPreviousPage = condicionContenedor.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<condicionContenedorDto>>(condicionContenedorDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una condicion de un contenedor especifico
        /// </summary>
        /// <param name="idCondicion"></param>
        /// <returns></returns>
        [HttpGet("{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionContenedorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCondicionContenedor(long idCondicion)
        {
            var condicionContenedor = await _condicionContenedorService.GetCondicionContenedor(idCondicion);

            if (condicionContenedor == null)
                throw new AguilaException("Condicion No Existente", 404);

            var condicionContenedorDto = _mapper.Map<condicionContenedorDto>(condicionContenedor);
            var response = new AguilaResponse<condicionContenedorDto>(condicionContenedorDto);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la ultima condicion del activo solicitado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet("ultima/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionGenSetDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetGenSet(int idActivo)
        {
            var condicionContenedor = _condicionContenedorService.ultima(idActivo);

            if (condicionContenedor == null)
                throw new AguilaException("No hay ninguna condicion", 404);

            var condicionContenedorDto = _mapper.Map<condicionContenedorDto>(condicionContenedor);

            var response = new AguilaResponse<condicionContenedorDto>(condicionContenedorDto);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el listado de estados para los generadores tanto tecnica como de estructura
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("estados/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<estadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetActivoEstadoCondicion(int idEmpresa)
        {
            var xEventosCondicionContenedor = new List<string>
            {
                ControlActivosEventos.CondicionIngreso.ToString(),
                ControlActivosEventos.CondicionSalida.ToString()
            };

            var estadoEvento = _estadosService.GetEstadosByEvento(idEmpresa, "activoEstados", xEventosCondicionContenedor);

            var estadoEventoDto = _mapper.Map<IEnumerable<estadosDto>>(estadoEvento);
            var response = new AguilaResponse<IEnumerable<estadosDto>>(estadoEventoDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva condicion de contenedor
        /// </summary>
        /// <param name="condicionContenedorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionContenedorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(condicionContenedorDto condicionContenedorDto)
        {
            var xCondicionContenedor = await _condicionContenedorService.InsertCondicionContenedor(condicionContenedorDto);

            var response = new AguilaResponse<condicionContenedorDto>(xCondicionContenedor);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una condicion de contenedor, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condicionContenedorDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionContenedorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, condicionContenedorDto condicionContenedorDto)
        {
            var xCondicionContenedor = _mapper.Map<condicionContenedor>(condicionContenedorDto);
            //condicionEquipo.id = id;

            var result = await _condicionContenedorService.UpdateCondicionContenedor(xCondicionContenedor);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una condicion de contenedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<condicionContenedorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _condicionContenedorService.DeleteCondicionContenedor(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _condicionContenedorService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve la configuracion de imagen para una propiedad del recurso, propiedades disponibles ImagenFirmaPiloto , Fotos   
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpGet("ImagenConfiguracion/{propiedad}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ImagenRecursoConfiguracion>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImagenConfiguracion(string propiedad)
        {
            //var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var controlador = "condicionActivos";
            var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);

            var response = new AguilaResponse<ImagenRecursoConfiguracion>(imgRecConf);
            return Ok(response);
        }
    }
}
