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
    public class direccionesController : ControllerBase
    {
        private readonly IdireccionesService _direccionesService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IpaisesService _paisesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public direccionesController(IdireccionesService direccionesService, IMapper mapper, IPasswordService password,
                                     ImunicipiosService municipiosService,
                                     IdepartamentosService departamentosService,
                                     IpaisesService paisesService)
        {
            _direccionesService = direccionesService;
            _municipiosService = municipiosService;
            _departamentosService = departamentosService;
            _paisesService = paisesService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene las direcciónes registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<direccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDirecciones([FromQuery] direccionesQueryFilter filter)
        {
            var direcciones = _direccionesService.GetDirecciones(filter);
            var direccionesDto = _mapper.Map<IEnumerable<direccionesDto>>(direcciones);

            var metadata = new Metadata
            {
                TotalCount = direcciones.TotalCount,
                PageSize = direcciones.PageSize,
                CurrentPage = direcciones.CurrentPage,
                TotalPages = direcciones.TotalPages,
                HasNextPage = direcciones.HasNextPage,
                HasPreviousPage = direcciones.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<direccionesDto>>(direccionesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una dirección por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<direccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDirecciones(int id)
        {
            var direcciones = await _direccionesService.GetDireccion(id);
            var direccionesDto = _mapper.Map<direccionesDto>(direcciones);

            if (direcciones == null)
            {
                throw new AguilaException("Direccion no existente", 404);
            }

            //Get de objeto direcciones 
            var municipioDireccion = await _municipiosService.GetMunicipio(direcciones.idMunicipio);
            var departamento = await _departamentosService.GetDepartamento(municipioDireccion.idDepartamento);
            var pais = await _paisesService.GetPais(departamento.idPais);

            //Set de los objetos
            //direccionesDto.idDepartamento = departamento.id;
            //direccionesDto.idPais = pais.Id;

            var response = new AguilaResponse<direccionesDto>(direccionesDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva dirección
        /// </summary>
        /// <param name="direccionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<direccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(direccionesDto direccionDto)
        {
            var direccion = _mapper.Map<direcciones>(direccionDto);
            await _direccionesService.InsertDireccion(direccion);

            direccionDto = _mapper.Map<direccionesDto>(direccion);
            var response = new AguilaResponse<direccionesDto>(direccionDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una dirección, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="direccionDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<direccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, direccionesDto direccionDto)
        {
            var direccion = _mapper.Map<direcciones>(direccionDto);
            direccion.id = id;

            var result = await _direccionesService.UpdateDireccion(direccion);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una dirección, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<direccionesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _direccionesService.DeleteDireccion(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        //[HttpGet("api/Direcciones/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _direccionesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
