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
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class empleadosController : ControllerBase
    {
        private readonly IempleadosService _empleadosService;
        private readonly IdireccionesService _direccionesService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IpaisesService _paisesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public empleadosController(IempleadosService empleadosService, IMapper mapper, IPasswordService password,
                                   IdireccionesService direccionesService,
                                   ImunicipiosService municipiosService,
                                   IdepartamentosService departamentosService,
                                   IpaisesService paisesService,
                                   IImagenesRecursosService imagenesRecursosService)
        {
            _empleadosService = empleadosService;
            _direccionesService = direccionesService;
            _municipiosService = municipiosService;
            _departamentosService = departamentosService;
            _paisesService = paisesService;
            _mapper = mapper;
            _passwordService = password;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Obtiene los empleados registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEmpleados([FromQuery] empleadosQueryFilter filter)
        {
            var empleados = await _empleadosService.GetEmpleados(filter);
            var empleadosDto = _mapper.Map<IEnumerable<empleadosDto>>(empleados);

            //foreach (var empleado in empleadosDto)
            //{
            //    //Get de objetos direcciones
            //    var direccion = await _direccionesService.GetDireccion(empleado.idDireccion);
            //    var municipio = await _municipiosService.GetMunicipio(direccion.idMunicipio);
            //    var departamento = await _departamentosService.GetDepartamento(municipio.idDepartamento);
            //    var pais = await _paisesService.GetPais(departamento.idPais);

            //    empleado.vDireccion = direccion.colonia + ", " + direccion.direccion + ", " + direccion.zona + ", " +
            //                                                    municipio.nombreMunicipio + ", " + departamento.nombre + ", " +
            //                                                    pais.Nombre;
            //}

            var metadata = new Metadata
            {
                TotalCount = empleados.TotalCount,
                PageSize = empleados.PageSize,
                CurrentPage = empleados.CurrentPage,
                TotalPages = empleados.TotalPages,
                HasNextPage = empleados.HasNextPage,
                HasPreviousPage = empleados.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<empleadosDto>>(empleadosDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un empleado, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEmpleado(int id)
        {
            var empleados = await _empleadosService.GetEmpleado(id);
            var empleadosDto = _mapper.Map<empleadosDto>(empleados);

            if (empleados == null)
            {
                throw new AguilaException("Empleado No Existente", 404);
            }

            //Get de objeto direcciones 
            //var direccionEmpleado = await _direccionesService.GetDireccion(empleadosDto.idDireccion);
            //var direccionEmpleadoDto = _mapper.Map<direccionesDto>(direccionEmpleado);

            //var municipioDireccion = await _municipiosService.GetMunicipio(direccionEmpleado.idMunicipio);
            //var departamento = await _departamentosService.GetDepartamento(municipioDireccion.idDepartamento);
            //var pais = await _paisesService.GetPais(departamento.idPais);

            //Set de los objetos
            //empleadosDto.direccion = direccionEmpleadoDto;
            //empleadosDto.direccion.idDepartamento = departamento.id;
            //empleadosDto.direccion.idPais = pais.Id;

            var response = new AguilaResponse<empleadosDto>(empleadosDto);
            return Ok(response);
        }

        [HttpGet("empleado/{cui}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEmpleadoByCui(string cui)
        {
            var empleados = await _empleadosService.GetEmpleadoByCui(cui);
            var empleadosDto = _mapper.Map<empleadosDto>(empleados);

            if (empleados == null)
            {
                throw new AguilaException("Empleado No Existente", 404);
            }

            var response = new AguilaResponse<empleadosDto>(empleadosDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo empleado
        /// </summary>
        /// <param name="empleadoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(empleadosDto empleadoDto)
        {
            //if (await _empleadosService.existeCodigo(empleadoDto))
            //{
            //    throw new AguilaException("Codigo de Empleado ya Registrado!!....", 406);
            //}

            //Guarda la dirección del empleado
            //var direccion = _mapper.Map<direcciones>(empleadoDto.direccion);
            //Get de objetos 
            //var municipio = await _municipiosService.GetMunicipio(direccion.idMunicipio);
            //var departamento = await _departamentosService.GetDepartamento(municipio.idDepartamento);
            //var pais = await _paisesService.GetPais(departamento.idPais);
            //Asigna el id de la nueva dirección para el empleado
            //empleadoDto.idDireccion = direccion.id;

            //Guarda el empleado
            var empleados = _mapper.Map<empleados>(empleadoDto);
            await _empleadosService.InsertEmpleado(empleados);

            empleadoDto = _mapper.Map<empleadosDto>(empleados);

            //empleadoDto.direccion = null;
            //empleadoDto.vDireccion = direccion.colonia + ", " + direccion.direccion + ", " + direccion.zona + ", " +
            //                                                    municipio.nombreMunicipio + ", " + departamento.nombre + ", " +
            //                                                    pais.Nombre;

            var response = new AguilaResponse<empleadosDto>(empleadoDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un empleado, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empleadoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, empleadosDto empleadoDto)
        {
            var empleado = _mapper.Map<empleados>(empleadoDto);
            empleado.id = id;

            var result = await _empleadosService.UpdateEmpleado(empleado);

            if (result)
            {
                //Actualiza la direccion del empleado
                var direccion = _mapper.Map<direcciones>(empleadoDto.direccion);
                await _direccionesService.UpdateDireccion(direccion);
            }

            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un empleado, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _empleadosService.DeleteEmpleado(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        //[HttpGet("api/Empleados/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _empleadosService.GetRecursoByControlador(controlador);
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
        /// REPORTE DE AUSENCIAS DE EMPLEADOS
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("ausencias")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<empleadosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult ausecias([FromQuery] reporteAusenciasQueryFilter filter)
        {
            var ausentes = _empleadosService.getAusencias(filter);
            var usentesDto = _mapper.Map<IEnumerable<empleadosDto>>(ausentes);

            var response = new AguilaResponse<IEnumerable<empleadosDto>>(usentesDto)
            { };
            return Ok(response);
        }

    }
}
