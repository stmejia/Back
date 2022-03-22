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

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public EmpresasController(IEmpresaService empresaService, IMapper mapper, 
                                  IPasswordService passwordService,
                                  IImagenesRecursosService imagenesRecursosService)
        {
            //inyeccion de dependencias
            _empresaService = empresaService;
            _mapper = mapper;
            _passwordService = passwordService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Extraccion total de Empresas, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<EmpresasDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEmpresas([FromQuery] EmpresaQueryFilter filter)
        {
            var controlNombre = ControllerContext.ActionDescriptor.ControllerName;

            var empresas = await _empresaService.GetEmpresas(filter);
            //convertimos con automapper el resultado en un entity hacia un DTo para enviar la respuesta
            var empDTo = _mapper.Map<IEnumerable<EmpresasDto>>(empresas);

            var metadata = new Metadata
            {
                TotalCount = empresas.TotalCount,
                PageSize = empresas.PageSize,
                CurrentPage = empresas.CurrentPage,
                TotalPages = empresas.TotalPages,
                HasNextPage = empresas.HasNextPage,
                HasPreviousPage = empresas.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<EmpresasDto>>(empDTo)
            {
                Meta = metadata
            }; 

            return Ok(response);
        }

        /// <summary>
        /// Consulta de Empresa, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<EmpresasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEmpresa(byte id)
        {
            var emp = await _empresaService.GetEmpresa(id);
            var empDTo = _mapper.Map<EmpresasDto>(emp);

            var response = new AguilaResponse<EmpresasDto>(empDTo);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve la configuracion de imagen para una propiedad del recurso
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpGet("/api/Empresas/ImagenConfiguracion/{propiedad}")]
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
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        [HttpGet("/api/Empresas/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _empresaService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Crear Empresa Nueva
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<EmpresasDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(EmpresasDto empresaDTo)
        {
            var empresa = _mapper.Map<Empresas>(empresaDTo);
            
            //// manejo de imagenes
            //if (empresa.ImagenLogo != null)
            //{
            //    // Solo se debe de buscar la configuracion y dejar que el service se encargue
            //    var controlador = ControllerContext.ActionDescriptor.ControllerName;
            //    var propiedad = nameof(empresa.ImagenLogo);
            //    var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);
            //    empresa.ImagenLogo.ImagenRecursoConfiguracion = imgRecConf;
            //}
            //// Fin de manejo de imagenes

            await _empresaService.InsertEmpresa(empresa);

            empresaDTo = _mapper.Map<EmpresasDto>(empresa);
            var response = new AguilaResponse<EmpresasDto>(empresaDTo);
            return Ok(response);

        }

        /// <summary>
        /// Actualizacion de Empresa, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empresaDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(byte id, EmpresasDto empresaDTo)
        {
            var empresa = _mapper.Map<Empresas>(empresaDTo);
            empresa.Id = id;

            var result = await _empresaService.updateEmpresa(empresa);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Empresa, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(byte id)
        {
           
           var result =  await _empresaService.DeleteEmpresa(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }
    }
}
