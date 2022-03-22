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
    public class transportesController : ControllerBase
    {
        private readonly ItransportesService _transportesService;
        private readonly IproveedoresService _proveedoresService;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public transportesController(ItransportesService transportesService, IMapper mapper, IPasswordService password,
                                     IproveedoresService proveedoresService,
                                     IentidadComercialService entidadComercialService)
        {
            _transportesService = transportesService;
            _proveedoresService = proveedoresService;
            _entidadComercialService = entidadComercialService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los transportes registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<transportesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTransportes([FromQuery] transportesQueryFilter filter)
        {
            var transportes = _transportesService.GetTransportes(filter);
            var transportesDto = _mapper.Map<IEnumerable<transportesDto>>(transportes);

            foreach (var transporte in transportesDto)
            {
                //Get de objeto proveedores 
                var proveedorTransporte = await _proveedoresService.GetProveedor(transporte.idProveedor);
                //Get de objeto entidad comercial
                var entidadComercial = await _entidadComercialService.GetEntidadComercial(proveedorTransporte.idEntidadComercial);
                var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);

                //Set de los objetos
                transporte.entidadComercial.Add(entidadComercialDto);
            }

            var metadata = new Metadata
            {
                TotalCount = transportes.TotalCount,
                PageSize = transportes.PageSize,
                CurrentPage = transportes.CurrentPage,
                TotalPages = transportes.TotalPages,
                HasNextPage = transportes.HasNextPage,
                HasPreviousPage = transportes.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<transportesDto>>(transportesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un transporte por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<transportesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDepartamentos(int id)
        {
            var transportes = await _transportesService.GetTransporte(id);
            var transportesDto = _mapper.Map<transportesDto>(transportes);

            if (transportes == null)
            {
                throw new AguilaException("Transporte No Existente", 404);
            }

            ////Get de objeto proveedores 
            //var proveedorTransporte = await _proveedoresService.GetProveedor(transportesDto.idProveedor);
            ////Get de objeto entidad comercial
            //var entidadComercial = await _entidadComercialService.GetEntidadComercial(proveedorTransporte.idEntidadComercial);
            //var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);

            ////Set de los objetos
            //transportesDto.entidadComercial.Add(entidadComercialDto);

            var response = new AguilaResponse<transportesDto>(transportesDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo transporte
        /// </summary>
        /// <param name="transporteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<transportesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(transportesDto transporteDto)
        {
            var transporte = _mapper.Map<transportes>(transporteDto);
            await _transportesService.InsertTransporte(transporte);

            transporteDto = _mapper.Map<transportesDto>(transporte);
            var response = new AguilaResponse<transportesDto>(transporteDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un transporte, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transporteDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<transportesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, transportesDto transporteDto)
        {
            var transporte = _mapper.Map<transportes>(transporteDto);
            transporte.id = id;

            var result = await _transportesService.UpdateTransporte(transporte);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un transporte, envíamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<transportesDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _transportesService.DeleteTransporte(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        //[HttpGet("api/Transportes/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _transportesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
