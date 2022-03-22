using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalService _sucursalService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SucursalesController(ISucursalService sucursalService, IMapper mapper, IPasswordService passwordService)
        {
            //inyeccion de dependencias
            _sucursalService = sucursalService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Extraccion total de Sucursales, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<SucursalDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetSucursales([FromQuery] SucursalQueryFilter filter)
        {
            var sucursales =  _sucursalService.GetSucursales(filter);
            var sucursalesDto = _mapper.Map<IEnumerable<SucursalDto>>(sucursales);

            var metadata = new Metadata
            {
                TotalCount = sucursales.TotalCount,
                PageSize = sucursales.PageSize,
                CurrentPage = sucursales.CurrentPage,
                TotalPages = sucursales.TotalPages,
                HasNextPage = sucursales.HasNextPage,
                HasPreviousPage = sucursales.HasPreviousPage,                
            };

            var response = new AguilaResponse<IEnumerable<SucursalDto>>(sucursalesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Sucursal, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<SucursalDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSucursal(short id)
        {
            var sucursal = await _sucursalService.GetSucursal(id);
            var sucursalDto = _mapper.Map<SucursalDto>(sucursal);

            var response = new AguilaResponse<SucursalDto>(sucursalDto);
            return Ok(response);
        }

        /// <summary>
        /// Crear Sucursal Nueva
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<SucursalDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(SucursalDto sucursalDTo)
        {
            var sucursal = _mapper.Map<Sucursales>(sucursalDTo);
            await _sucursalService.InsertSucursal(sucursal);

            sucursalDTo = _mapper.Map<SucursalDto>(sucursal);
            var response = new AguilaResponse<SucursalDto>(sucursalDTo);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Sucursal, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sucursalDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(byte id, SucursalDto sucursalDTo)
        {
            var sucursal = _mapper.Map<Sucursales>(sucursalDTo);
            sucursal.Id = id;

            var result = await _sucursalService.updateSucursal(sucursal);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Sucursal, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(short id)
        {

            var result = await _sucursalService.DeleteSucursal(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        ///[HttpGet("/api/Sucursales/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _sucursalService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

    }
}
