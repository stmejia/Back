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
using System;
using Aguila.Core.Enumeraciones;
using System.Security.Claims;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class controlContratistasController : ControllerBase
    {
        private readonly IcontrolContratistasService _controlContratistasService;
        private readonly IMapper _mapper;
        private readonly IestadosService _estadosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public controlContratistasController(IcontrolContratistasService controlContratistasService, IMapper mapper, 
                                             IestadosService estadosService, IImagenesRecursosService imagenesRecursosService)
        {
            _controlContratistasService = controlContratistasService;
            _mapper = mapper;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene los contratistas registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlContratistasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetControlContratistas([FromQuery] controlContratistasQueryFilter filter)
        {
            var controlContratistas = await _controlContratistasService.GetControlContratistas(filter);
            var controlContratistasDto = _mapper.Map<IEnumerable<controlContratistasDto>>(controlContratistas);

            var metadata = new Metadata
            {
                TotalCount = controlContratistas.TotalCount,
                PageSize = controlContratistas.PageSize,
                CurrentPage = controlContratistas.CurrentPage,
                TotalPages = controlContratistas.TotalPages,
                HasNextPage = controlContratistas.HasNextPage,
                HasPreviousPage = controlContratistas.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<controlContratistasDto>>(controlContratistasDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Se obtiene un contratista por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlContratistasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsesor(int id)
        {
            var controlContratistas = await _controlContratistasService.GetControlContratistas(id);
            var controlContratistasDto = _mapper.Map<controlContratistasDto>(controlContratistas);

            var response = new AguilaResponse<controlContratistasDto>(controlContratistasDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un contratista por documento de IDENTIFICACION siempre y cuando tenga una salida pendiente
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpGet("contratista/{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlContratistasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> getContratistaXid(string identificacion)
        {
            var contratista = await _controlContratistasService.contratistaPorId(identificacion);
            var contratistaDto = _mapper.Map<controlContratistasDto>(contratista);

            var response = new AguilaResponse<controlContratistasDto>(contratistaDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo contratista
        /// </summary>
        /// <param name="controlContratistasDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlContratistasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(controlContratistasDto controlContratistasDto)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            //controlContratistasDto.idUsuario = usuarioId;

            //var controlContratista = await _controlContratistasService.InsertControlContratistas(controlContratistasDto);

            var contratista = _mapper.Map<controlContratistas>(controlContratistasDto);
            contratista.idUsuario = usuarioId;

            await _controlContratistasService.InsertControlContratistas(contratista);

            controlContratistasDto = _mapper.Map<controlContratistasDto>(contratista);

            var response = new AguilaResponse<controlContratistasDto>(controlContratistasDto);
            return Ok(response);
        }

        /// <summary>
        /// Da salida a un contratista, envíar identificacion
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpPut("salida/{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlContratistasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DarSalida(string identificacion)
        {
            var controlContratista = await _controlContratistasService.darSalida(identificacion);

            var controlContratistaDto = _mapper.Map<controlContratistasDto>(controlContratista);

            var response = new AguilaResponse<controlContratistasDto>(controlContratistaDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza a un contratista, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="controlContratistasDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlContratistasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, controlContratistasDto controlContratistasDto)
        {
            var controlContratistas = _mapper.Map<controlContratistas>(controlContratistasDto);

            var result = await _controlContratistasService.UpdateControlContratistas(controlContratistas);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un contratista
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlContratistasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _controlContratistasService.DeleteControlContratistas(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }


        /// <summary>
        /// Obtiene un constratista por su Documento de Identificacion
        /// </summary>      
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpGet("getContratista/{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlVisitasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> getVisitaGeneric(string identificacion)
        {

            var visita = await _controlContratistasService.contratistaPorIdGeneric(identificacion);

            var visitaDto = _mapper.Map<controlContratistasDto>(visita);

            var response = new AguilaResponse<controlContratistasDto>(visitaDto);
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
            var recurso = await _controlContratistasService.GetRecursoByControlador(controlador);
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

        /// <summary>
        /// Devuelve el listado de contratistas que aun estan en predio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("enPredio")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlContratistasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult enPredio([FromQuery] contratistasEnPredioQueryFilter filter)
        {
            if (filter.fecha == null)
            {
                throw new AguilaException("Debe especificar una fecha para generar el reporte...", 404);
            }

            if(filter.idEstacionTrabajo == null)
            {
                throw new AguilaException("Debe especificar una predio para generar el reporte...", 404);
            }

            var contratistas = _controlContratistasService.enPredio(filter.fecha.Value, (int)filter.idEstacionTrabajo);
            var contratistasDto = _mapper.Map<IEnumerable<controlContratistasDto>>(contratistas);

            var response = new AguilaResponse<IEnumerable<controlContratistasDto>>(contratistasDto);
            return Ok(response);
        }
    }
}
