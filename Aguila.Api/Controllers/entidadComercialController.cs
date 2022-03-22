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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class entidadComercialController : ControllerBase
    {
        private readonly IentidadComercialService _entidadComercialService;
        private readonly IdireccionesService _direccionesService;
        private readonly IdepartamentosService _departamentoService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IpaisesService _paisesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public entidadComercialController(IentidadComercialService entidadComercialService, IMapper mapper, IPasswordService password,
                                          IdireccionesService direccionesService,
                                          IdepartamentosService departamentosService,
                                          IpaisesService paisesService,
                                          ImunicipiosService municipiosService)
        {
            _entidadComercialService = entidadComercialService;
            _direccionesService = direccionesService;
            _departamentoService = departamentosService;
            _paisesService = paisesService;
            _municipiosService = municipiosService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene las entidades registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadComercialDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEntidadComercial([FromQuery] entidadComercialQueryFilter filter)
        {
            var entidadComercial = _entidadComercialService.GetEntidadComercial(filter);
            var entidadComercialDto = _mapper.Map<IEnumerable<entidadComercialDto>>(entidadComercial);

            //si es filtrado por Nit 
            if (filter.nit != null)
            {            

                foreach (var entidad in entidadComercialDto)
                {
                    //SET de objeto Direccion de Entidad Comercial
                    var direccion = await _direccionesService.GetDireccion((long)entidad.idDireccionFiscal);
                    var direccionDto = _mapper.Map<direccionesDto>(direccion);
                    entidad.direccionFiscal = direccionDto;

                    //busca Id de departamento
                    //var municipio = await _municipiosService.GetMunicipio(entidad.direccionFiscal.idMunicipio);
                    //entidad.direccionFiscal.idDepartamento = municipio.idDepartamento;

                    //busca el Id de Pais
                    //var departamento = await _departamentoService.GetDepartamento(entidad.direccionFiscal.idDepartamento);
                    //entidad.direccionFiscal.idPais = departamento.idPais;

                    //asgina el tipo de entidad comercial
                    entidad.tipo = _entidadComercialService.getTipo(entidad.id, filter.idEmpresa);
                }

            }
            else
            {
                foreach (var entidad in entidadComercialDto)
                    entidad.direccionFiscal = null;
            }
                     

            var metadata = new Metadata
            {
                TotalCount = entidadComercial.TotalCount,
                PageSize = entidadComercial.PageSize,
                CurrentPage = entidadComercial.CurrentPage,
                TotalPages = entidadComercial.TotalPages,
                HasNextPage = entidadComercial.HasNextPage,
                HasPreviousPage = entidadComercial.HasPreviousPage
            };

            var response = new AguilaResponse<IEnumerable<entidadComercialDto>>(entidadComercialDto)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadComercialDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEntidadComercial(long id)
        {
            var entidadComercial = await _entidadComercialService.GetEntidadComercial(id);
            var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);

            if(entidadComercialDto != null)
            {
                //SET de objeto Direccion de Entidad Comercial
                var direccion = await _direccionesService.GetDireccion((long)entidadComercial.idDireccionFiscal);
                var direccionDto = _mapper.Map<direccionesDto>(direccion);
                entidadComercialDto.direccionFiscal = direccionDto;

                //busca Id de departamento
                //var municipio = await _municipiosService.GetMunicipio(entidadComercialDto.direccionFiscal.idMunicipio);
                //entidadComercialDto.direccionFiscal.idDepartamento = municipio.idDepartamento;

                //busca el Id de Pais
                //var departamento = await _departamentoService.GetDepartamento(entidadComercialDto.direccionFiscal.idDepartamento);
                //entidadComercialDto.direccionFiscal.idPais = departamento.idPais;
            }

            

            var response = new AguilaResponse<entidadComercialDto>(entidadComercialDto);
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="entidadComercialDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadComercialDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(entidadComercialDto entidadComercialDto)
        {
            var entidadComercial = _mapper.Map<entidadComercial>(entidadComercialDto);
            await _entidadComercialService.InsertEntidadComercial(entidadComercial);

            entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);
            var response = new AguilaResponse<entidadComercialDto>(entidadComercialDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza una entidad, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entidadComercialDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadComercialDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, entidadComercialDto entidadComercialDto)
        {
            var entidadComercial = _mapper.Map<entidadComercial>(entidadComercialDto);
            entidadComercial.id = id;

            var result = await _entidadComercialService.UpdateEntidadComercial(entidadComercial);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina una entidad, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadComercialDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _entidadComercialService.DeleteEntidadComercial(id);
            var response = new AguilaResponse<bool>(result);

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        //[HttpGet("api/EntidadComercial/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _entidadComercialService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
