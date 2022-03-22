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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class empleadosIngresosController  : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IempleadosIngresosService _empleadosIngresosService;

        public empleadosIngresosController(IMapper mapper,
                                        IempleadosIngresosService empleadosIngresosService)
        {
            _mapper = mapper;
            _empleadosIngresosService = empleadosIngresosService;
        }

        /// <summary>
        /// Obtiene las ingresos/salidas de empleados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosIngresosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetIngresos([FromQuery] empleadosIngresosQueryFilter filter)
        {

            var ingresos = _empleadosIngresosService.getIngresos(filter);
            var ingresosDto = _mapper.Map<IEnumerable<empleadosIngresosDto>>(ingresos);

            var metadata = new Metadata
            {
                TotalCount = ingresos.TotalCount,
                PageSize = ingresos.PageSize,
                CurrentPage = ingresos.CurrentPage,
                TotalPages = ingresos.TotalPages,
                HasNextPage = ingresos.HasNextPage,
                HasPreviousPage = ingresos.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<empleadosIngresosDto>>(ingresosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }


        ///// <summary>
        ///// REPORTE DE AUSENCIAS
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //[HttpGet("ausencias")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public IActionResult ausecias([FromQuery] reporteAusenciasQueryFilter filter)
        //{          
        //    var ausentes =  _empleadosIngresosService.getAusencias(filter);
        //    var usentesDto = _mapper.Map<IEnumerable<empleadosDto>>(ausentes);            

        //    var response = new AguilaResponse<IEnumerable<empleadosDto>>(usentesDto)
        //    {};
        //    return Ok(response);
        //}


        /// <summary>
        /// Consulta de Visita, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<empleadosIngresosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetVisita(long id)
        {
            var ingreso = await _empleadosIngresosService.GetIngreso(id);
            var ingresoDto = _mapper.Map<empleadosIngresosDto>(ingreso);

            var response = new AguilaResponse<empleadosIngresosDto>(ingresoDto);
            return Ok(response);
        }

        /// <summary>
        /// Registra un ingreso/salida a un empleado
        /// </summary>      
        /// <param name="ingresoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<empleadosIngresosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(empleadosIngresosDto ingresoDto)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            var ingreso = _mapper.Map<empleadosIngresos>(ingresoDto);
            ingreso.idUsuario = usuarioId;
            var validar = false;

            if (ingresoDto.validar != null)//verifica si viene la bandera de validar el ingreso/salida
            {
                validar = ingresoDto.validar.Value;
            }

            await _empleadosIngresosService.InsertIngreso(ingreso, validar);

            ingresoDto = _mapper.Map<empleadosIngresosDto>(ingreso);

            var response = new AguilaResponse<empleadosIngresosDto>(ingresoDto);
            return Ok(response);
        }


        /// <summary>
        /// Actualizacion de Ingreso, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ingresoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, empleadosIngresosDto ingresoDTo)
        {
            var ingreso = _mapper.Map<empleadosIngresos>(ingresoDTo);
            ingreso.id = id;

            var result = await _empleadosIngresosService.UpdateIngreso(ingreso);
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
            var result = await _empleadosIngresosService.DeleteVisita(id);
            var response = new AguilaResponse<bool>(result);

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
            var recurso = await _empleadosIngresosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
