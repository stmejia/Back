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

    public class controlEquipoAjenoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IcontrolEquipoAjenoService _controlEquipoAjenoService;

        public controlEquipoAjenoController(IMapper mapper,
                                        IcontrolEquipoAjenoService controlEquipoAjenoService)
        {
            _mapper = mapper;
            _controlEquipoAjenoService = controlEquipoAjenoService;
        }

        /// <summary>
        /// Obtiene los ingresos de equipo ajeno registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<controlEquipoAjenoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetIngresosEquipoAjeno([FromQuery] controlEquipoAjenoQueryFilter filter)
        {

            var ajeno = _controlEquipoAjenoService.GetEquiposEjanos(filter);
            var ajenoDto = _mapper.Map<IEnumerable<controlEquipoAjenoDto>>(ajeno);

            var metadata = new Metadata
            {
                TotalCount = ajeno.TotalCount,
                PageSize = ajeno.PageSize,
                CurrentPage = ajeno.CurrentPage,
                TotalPages = ajeno.TotalPages,
                HasNextPage = ajeno.HasNextPage,
                HasPreviousPage = ajeno.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<controlEquipoAjenoDto>>(ajenoDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Consulta de Ingreso de Equipo Ajeno, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlEquipoAjenoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetEquipoAjeno(long id)
        {
            var ajeno = await _controlEquipoAjenoService.GetAjeno(id);
            var ajenoDto = _mapper.Map<controlEquipoAjenoDto>(ajeno);

            var response = new AguilaResponse<controlEquipoAjenoDto>(ajenoDto);
            return Ok(response);
        }

        /// <summary>
        /// Da Ingreso a un equipo ajeno
        /// </summary>      
        /// <param name="control"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<controlEquipoAjenoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(controlGaritaDto control)
        {
            //Capturamos el id del usuario del token de sesion
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = System.Int32.Parse(identity.FindFirst("UsuarioId").Value.ToString());

            controlEquipoAjenoDto ajenoDto = new controlEquipoAjenoDto();
            ajenoDto.nombrePiloto = control.piloto;
            ajenoDto.idEstacionTrabajo = control.idEstacionTrabajo;
            ajenoDto.atc = control.atc;
            ajenoDto.cargado = control.lleno;
            ajenoDto.origen = control.origenDestino;
            ajenoDto.marchamo = control.marchamo;
            ajenoDto.empresa = control.empresa;

           //tipo 1: Cabezal, tipo 2 : furgon/contenedor, tipo 3: chasis , tipo 4: generador
            foreach (var equipo in control.equipos) {

                switch (equipo.tipoEquipo) {

                    case 1:
                        if (ajenoDto.placaCabezal!=null && ajenoDto.placaCabezal.Length > 0)
                            ajenoDto.placaCabezal = ajenoDto.placaCabezal + ", " + equipo.codigo;
                        else
                            ajenoDto.placaCabezal = equipo.codigo;

                        break;

                    case 3:
                        if (ajenoDto.codigoChasis!=null && ajenoDto.codigoChasis.Length > 0)
                            ajenoDto.codigoChasis = ajenoDto.codigoChasis + ", " + equipo.codigo;
                        else
                            ajenoDto.codigoChasis = equipo.codigo;

                        if (ajenoDto.tipoEquipo!=null && ajenoDto.tipoEquipo.Length > 0)
                            ajenoDto.tipoEquipo = ajenoDto.tipoEquipo + ", " + equipo.tamanoEquipo;
                        else
                            ajenoDto.tipoEquipo = equipo.tamanoEquipo;

                        break;

                    case 2:
                        if (ajenoDto.codigoEquipo!=null && ajenoDto.codigoEquipo.Length > 0)
                            ajenoDto.codigoEquipo = ajenoDto.codigoEquipo + ", " + equipo.codigo;
                        else
                            ajenoDto.codigoEquipo = equipo.codigo;

                        if (ajenoDto.tipoEquipo!=null && ajenoDto.tipoEquipo.Length > 0)
                            ajenoDto.tipoEquipo = ajenoDto.tipoEquipo + ", " + equipo.tamanoEquipo;
                        else
                            ajenoDto.tipoEquipo = equipo.tamanoEquipo;

                        break;

                    case 4:
                        if (ajenoDto.codigoGenerador!=null && ajenoDto.codigoGenerador.Length > 0)
                            ajenoDto.codigoGenerador = ajenoDto.codigoGenerador + ", " + equipo.codigo;
                        else
                            ajenoDto.codigoGenerador = equipo.codigo;

                        break;

                }
                
            }                 
           

            var ajeno = _mapper.Map<controlEquipoAjeno>(ajenoDto);
            ajeno.idUsuario = usuarioId;

            if (control.movimiento.ToLower().Equals("ingreso")) { ajeno.ingreso = DateTime.Now; ajeno.salida = null; }

            else if (control.movimiento.ToLower().Equals("salida")) { ajeno.salida = DateTime.Now; ajeno.ingreso = null; }
               

            await _controlEquipoAjenoService.InsertAjeno(ajeno);

            ajenoDto = _mapper.Map<controlEquipoAjenoDto>(ajeno);

            var response = new AguilaResponse<controlEquipoAjenoDto>(ajenoDto);
            await _controlEquipoAjenoService.ingresarPropios(control, usuarioId);


            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Ingreso de equipo ajeno, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ajenoDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, controlEquipoAjenoDto ajenoDTo)
        {
            var ajeno = _mapper.Map<controlEquipoAjeno>(ajenoDTo);
            ajeno.id = id;

            var result = await _controlEquipoAjenoService.UpdateAjeno(ajeno);
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
            var result = await _controlEquipoAjenoService.DeleteVisita(id);
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
            var recurso = await _controlEquipoAjenoService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
