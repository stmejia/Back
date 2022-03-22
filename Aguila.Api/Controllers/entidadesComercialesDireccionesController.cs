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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class entidadesComercialesDireccionesController : ControllerBase
    {
        private readonly IentidadesComercialesDireccionesService _entidadComercialDireccionService;
        private readonly IdireccionesService _direccionesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IdepartamentosService _departamentoService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IpaisesService _paisesService;

        public entidadesComercialesDireccionesController(IentidadesComercialesDireccionesService entidadComercialDireccionService,
                                                         IMapper mapper, IPasswordService password,
                                                         IdireccionesService direccionesService,
                                                         IdepartamentosService departamentosService,                                        
                                                         ImunicipiosService municipiosService,
                                                         IpaisesService paisesService)
        {
            _entidadComercialDireccionService = entidadComercialDireccionService;
            _direccionesService = direccionesService;
            _mapper = mapper;
            _passwordService = password;
            _departamentoService = departamentosService;
            _municipiosService = municipiosService;
            _paisesService = paisesService;
        }

        /// <summary>
        /// Obtiene las entidades registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadesComercialesDireccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEntidadComercialDireccion([FromQuery] entidadesComercialesDireccionesQueryFilter filter)
        {
            var entidadComercialDireccion = _entidadComercialDireccionService.GetEntidadComercialDireccion(filter);
            var entidadComercialDireccionDto = _mapper.Map<IEnumerable<entidadesComercialesDireccionesDto>>(entidadComercialDireccion);


            //se inicia la creacion de la direccion virtual

            foreach(var entidadesDirecciones in entidadComercialDireccionDto)
            {
                //se busca la direccion asociada 
                var direccion = await _direccionesService.GetDireccion(entidadesDirecciones.idDireccion);
                var municipio = await _municipiosService.GetMunicipio(direccion.idMunicipio);
                var departamento = await _departamentoService.GetDepartamento(municipio.idDepartamento);
                var pais = await _paisesService.GetPais(departamento.idPais);

                entidadesDirecciones.vDireccion = direccion.direccion + ", " + direccion.colonia + ", " +
                                                              direccion.zona + ", Codigo Postal " + direccion.codigoPostal + ", " +
                                                              municipio.nombreMunicipio + ", " + departamento.nombre + ", " + pais.Nombre;
            }


            var metadata = new Metadata
            {
                TotalCount = entidadComercialDireccion.TotalCount,
                PageSize = entidadComercialDireccion.PageSize,
                CurrentPage = entidadComercialDireccion.CurrentPage,
                TotalPages = entidadComercialDireccion.TotalPages,
                HasNextPage = entidadComercialDireccion.HasNextPage,
                HasPreviousPage = entidadComercialDireccion.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<entidadesComercialesDireccionesDto>>(entidadComercialDireccionDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una entidad por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadesComercialesDireccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEntidadComercialDireccion(long id)
        {
            var entidadComercialDireccion = await _entidadComercialDireccionService.GetEntidadComercialDireccion(id);
            var entidadComercialDireccionDto = _mapper.Map<entidadesComercialesDireccionesDto>(entidadComercialDireccion);

            //se busca la direccion asociada 
            var direccion = await _direccionesService.GetDireccion(entidadComercialDireccionDto.idDireccion);
            var direccionDto = _mapper.Map<direccionesDto>(direccion);

            //se asigna la direccion encontrada
            entidadComercialDireccionDto.direccion = direccionDto;

            //busca Id de departamento
            //var municipio = await _municipiosService.GetMunicipio(direccionDto.idMunicipio);
            //entidadComercialDireccionDto.direccion.idDepartamento = municipio.idDepartamento;

            //busca el Id de Pais
            //var departamento = await _departamentoService.GetDepartamento(municipio.idDepartamento);
            //entidadComercialDireccionDto.direccion.idPais = departamento.idPais;

           var response = new AguilaResponse<entidadesComercialesDireccionesDto>(entidadComercialDireccionDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="entidadComercialDireccionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadesComercialesDireccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(entidadesComercialesDireccionesDto entidadComercialDireccionDto)
        {

            //se inserta la direccion 
            var direccion = _mapper.Map<direcciones>(entidadComercialDireccionDto.direccion);
            await _direccionesService.InsertDireccion(direccion);

            //se asigna el ID de direccion generado en la entidadComercialDireccionDto
            entidadComercialDireccionDto.idDireccion = direccion.id;

            //se guarda la entidad comercial direccion 
            var entidadComercialDireccion = _mapper.Map<entidadesComercialesDirecciones>(entidadComercialDireccionDto);
            await _entidadComercialDireccionService.InsertEntidadComercialDireccion(entidadComercialDireccion);
            
            entidadComercialDireccionDto = _mapper.Map<entidadesComercialesDireccionesDto>(entidadComercialDireccion);
            var response = new AguilaResponse<entidadesComercialesDireccionesDto>(entidadComercialDireccionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una entidad, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entidadComercialDireccionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadesComercialesDireccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, entidadesComercialesDireccionesDto entidadComercialDireccionDto)
        {
            var entidadComercialDireccion = _mapper.Map<entidadesComercialesDirecciones>(entidadComercialDireccionDto);
            entidadComercialDireccion.id = id;

            var direccion = _mapper.Map<direcciones>(entidadComercialDireccionDto.direccion);
            direccion.id = entidadComercialDireccionDto.idDireccion;
            var resultupdateDireccion = await _direccionesService.UpdateDireccion(direccion);

            if (!resultupdateDireccion)
            {
                throw new AguilaException("Error al Actualizar Direccion, revise sus datos!...");
            }

            var result = await _entidadComercialDireccionService.UpdateEntidadComercialDireccion(entidadComercialDireccion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una entidad, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadesComercialesDireccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var entidadDireccion = await _entidadComercialDireccionService.GetEntidadComercialDireccion(id);

            var result = await _entidadComercialDireccionService.DeleteEntidadComercialDireccion(id);

            if (!result)
            {
                throw new AguilaException("Error al Eliminar Direccion, revise sus datos!...");
            }

            var resultDeleteDireccion = await _direccionesService.DeleteDireccion(entidadDireccion.idDireccion);

            if (!resultDeleteDireccion)
            {
                throw new AguilaException("Error al Eliminar Direccion, revise sus datos!...");
            }

            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        //[HttpGet("api/entidadesComercialesDirecciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _entidadComercialDireccionService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
