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

    public class controlVisitasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IcontrolVisitasService _controlVisitasService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public controlVisitasController(IMapper mapper,
                                        IcontrolVisitasService controlVisitasService,
                                        IImagenesRecursosService imagenesRecursosService)
        {
            _mapper = mapper;
            _controlVisitasService = controlVisitasService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene las visitas registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlVisitasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVisitas([FromQuery] controlVisitasQueryFilter filter)
        {

            var visitas = await _controlVisitasService.GetVisitas(filter);
            var visitasDto = _mapper.Map<IEnumerable<controlVisitasDto>>(visitas);

            var metadata = new Metadata
            {
                TotalCount = visitas.TotalCount,
                PageSize = visitas.PageSize,
                CurrentPage = visitas.CurrentPage,
                TotalPages = visitas.TotalPages,
                HasNextPage = visitas.HasNextPage,
                HasPreviousPage = visitas.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<controlVisitasDto>>(visitasDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Consulta de Visita, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlVisitasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetVisita(long id)
        {
            var visita = await _controlVisitasService.GetVisita(id);
            var visitaDto = _mapper.Map<controlVisitasDto>(visita);

            var response = new AguilaResponse<controlVisitasDto>(visitaDto);
            return Ok(response);
        } 

        /// <summary>
        /// Da Ingreso a visita
        /// </summary>      
        /// <param name="visitaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlVisitasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(controlVisitasDto visitaDto)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            var visita = _mapper.Map<controlVisitas>(visitaDto);
            visita.idUsuario = usuarioId;

            await _controlVisitasService.InsertVisita(visita);

            visitaDto = _mapper.Map<controlVisitasDto>(visita);
            
            var response = new AguilaResponse<controlVisitasDto>(visitaDto);
            return Ok(response);
        }

        /// <summary>
        /// Da Salida a visita
        /// </summary>      
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpPut("salida/{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlVisitasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DarSalida(string identificacion)
        {
                        
           var visita= await _controlVisitasService.darSalida(identificacion);

            var visitaDto = _mapper.Map<controlVisitasDto>(visita);

            var response = new AguilaResponse<controlVisitasDto>(visitaDto);
            return Ok(response);
        }


        /// <summary>
        /// Actualizacion de Visita, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="visitaDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, controlVisitasDto visitaDTo)
        {
            var visita = _mapper.Map<controlVisitas>(visitaDTo);
            visita.id = id;

            var result = await _controlVisitasService.UpdateVisita(visita);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una visita, anviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _controlVisitasService.DeleteVisita(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }


        /// <summary>
        /// Obtiene una vista por su Documento de Identificacion siempre y cuando tenga una salida pendiente
        /// </summary>      
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpGet("visita/{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlVisitasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> getVisitaXid(string identificacion)
        {

            var visita = await _controlVisitasService.visitaPorId(identificacion);

            var visitaDto = _mapper.Map<controlVisitasDto>(visita);

            var response = new AguilaResponse<controlVisitasDto>(visitaDto);
            return Ok(response);
        }


        /// <summary>
        /// Obtiene una vista por su Documento de Identificacion
        /// </summary>      
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [HttpGet("getVisita/{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlVisitasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> getVisitaGeneric(string identificacion)
        {

            var visita = await _controlVisitasService.visitaPorIdGeneric(identificacion);

            var visitaDto = _mapper.Map<controlVisitasDto>(visita);

            var response = new AguilaResponse<controlVisitasDto>(visitaDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Asesores/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _controlVisitasService.GetRecursoByControlador(controlador);
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
        /// Devuelve el listado de Visitas que aun estan en predio
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("enPredio")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlVisitasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult enPredio([FromQuery] contratistasEnPredioQueryFilter filter)
        {
            if (filter.fecha == null)
            {
                throw new AguilaException("Debe especificar una fecha para generar el reporte...", 404);
            }

            if (filter.idEstacionTrabajo == null)
            {
                throw new AguilaException("Debe especificar una predio para generar el reporte...", 404);
            }

            var visitas = _controlVisitasService.enPredio(filter.fecha.Value, (int)filter.idEstacionTrabajo);
            var visitasDto = _mapper.Map<IEnumerable<controlVisitasDto>>(visitas);

            var response = new AguilaResponse<IEnumerable<controlVisitasDto>>(visitasDto);
            return Ok(response);
        }

    }
}
