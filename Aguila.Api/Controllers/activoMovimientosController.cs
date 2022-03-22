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
using Aguila.Core.Enumeraciones;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class activoMovimientosController : ControllerBase
    {
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IempleadosService _empleadosService;
        private readonly IlistasService _listasService;
        private readonly IUsuariosService _usuariosService;
        private readonly IserviciosService _serviciosService;
        private readonly IrutasService _rutasService;
        private readonly IestadosService _estadosService;

        public activoMovimientosController(IactivoMovimientosService activoMovimientosService, 
                                            IMapper mapper, 
                                            IPasswordService passwordService,
                                            IempleadosService empleadosService,
                                            IlistasService listasService,
                                            IUsuariosService usuariosService,                                            
                                            IserviciosService serviciosService,
                                            IrutasService rutasService,
                                            IestadosService estadosService)
        {
            _activoMovimientosService = activoMovimientosService;
            _mapper = mapper;
            _passwordService = passwordService;
            _empleadosService = empleadosService;
            _listasService = listasService;
            _usuariosService = usuariosService;
            _serviciosService = serviciosService;
            _rutasService = rutasService;
            _estadosService = estadosService;
        }

        /// <summary>
        /// Obtiene los movimientos del activo registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> GetActivoMovimientos([FromQuery] activoMovimientosQueryFilter filter)
        public  IActionResult GetActivoMovimientos([FromQuery] activoMovimientosQueryFilter filter)
        {
            var activoMovimientos = _activoMovimientosService.GetActivoMovimientos(filter);
            var activoMovimientosDto = _mapper.Map<IEnumerable<activoMovimientosDto>>(activoMovimientos);

            var metadata = new Metadata
            {
                TotalCount = activoMovimientos.TotalCount,
                PageSize = activoMovimientos.PageSize,
                CurrentPage = activoMovimientos.CurrentPage,
                TotalPages = activoMovimientos.TotalPages,
                HasNextPage = activoMovimientos.HasNextPage,
                HasPreviousPage = activoMovimientos.HasPreviousPage,
            };


            var response = new AguilaResponse<IEnumerable<activoMovimientosDto>>(activoMovimientosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene los movimientos de un activo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActivoMovimientos(int id)
        {
            var activoMovimientos = await _activoMovimientosService.GetActivoMovimiento(id);
            var activoMovimientosDto = _mapper.Map<activoMovimientosDto>(activoMovimientos);

            if (activoMovimientos == null)
            {
                throw new AguilaException("Movimiento No Existente", 404);
            }

            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientosDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo movimiento para cada activo
        /// </summary>
        /// <param name="activoMovimientoDto"></param>
        /// <param name="evento"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(activoMovimientosDto activoMovimientoDto, ControlActivosEventos evento)
        {
            var activoMovimiento = _mapper.Map<activoMovimientos>(activoMovimientoDto);
            await _activoMovimientosService.InsertActivoMovimiento(activoMovimiento, evento);

            activoMovimientoDto = _mapper.Map<activoMovimientosDto>(activoMovimiento);
            var response = new AguilaResponse<activoMovimientosDto>(activoMovimientoDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un movimiento, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activoMovimientoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, activoMovimientosDto activoMovimientoDto)
        {
            var activoMovimiento = _mapper.Map<activoMovimientos>(activoMovimientoDto);
            activoMovimiento.id = id;

            var result = await _activoMovimientosService.UpdateActivoMovimiento(activoMovimiento);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un movimiento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<activoMovimientosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activoMovimientosService.DeleteActivoMovimiento(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/ActivoMovimientos/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _activoMovimientosService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        //public async Task<string> getNombreEmpleado(int id)
        //{
        //    var empleado = await  _empleadosService.GetEmpleado(id);
        //    if(empleado != null)
        //    {
        //        return empleado.nombres;
        //    }
        //    else
        //    {
        //        return null;
        //    }
           
        //}
    }
}
