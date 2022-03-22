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
using System.Linq;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class proveedoresController : ControllerBase
    {
        private readonly IproveedoresService _proveedoresService;
        private readonly IdireccionesService _direccionesService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IpaisesService _paisesService;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IentidadesComercialesDireccionesService _entidadesComercialesDireccionesService;
        private readonly ItipoProveedoresService _tipoProveedoresService;
        private readonly IcorporacionesService _corporacionesService;

        public proveedoresController(IproveedoresService proveedoresService, IMapper mapper, IPasswordService password,
                                  IdireccionesService direccionesService,
                                  IentidadComercialService entidadComercialService,
                                  ImunicipiosService municipiosService,
                                  IdepartamentosService departamentosService,
                                  IpaisesService paisesService,
                                  IentidadesComercialesDireccionesService entidadesComercialesDireccionesService,
                                  ItipoProveedoresService tipoProveedoresService,
                                  IcorporacionesService corporacionesService)
        {
            _proveedoresService = proveedoresService;
            _direccionesService = direccionesService;
            _entidadComercialService = entidadComercialService;
            _municipiosService = municipiosService;
            _departamentosService = departamentosService;
            _paisesService = paisesService;
            _mapper = mapper;
            _passwordService = password;
            _entidadesComercialesDireccionesService = entidadesComercialesDireccionesService;
            _tipoProveedoresService = tipoProveedoresService;
            _corporacionesService = corporacionesService;
        }

        /// <summary>
        /// Obtiene los proveedores registrados
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<proveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProveedores([FromQuery] proveedoresQueryFilter filter)
        {
            var proveedores = _proveedoresService.GetProveedores(filter);
            var proveedoresDto = _mapper.Map<IEnumerable<proveedoresDto>>(proveedores);

            foreach (var proveedor in proveedoresDto)
            {
                //set de objeto entidad comercial
                var entidadComercial = await _entidadComercialService.GetEntidadComercial(proveedor.idEntidadComercial);
                var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);               
                proveedor.entidadComercial=entidadComercialDto;

                //set de objeto direccion cliente
                var direccionCliente = await _direccionesService.GetDireccion(proveedor.idDireccion);
                var direccionClienteDto = _mapper.Map<direccionesDto>(direccionCliente);
                var municipio = await _municipiosService.GetMunicipio(direccionCliente.idMunicipio);
                var departamento = await _departamentosService.GetDepartamento(municipio.idDepartamento);
                var pais = await _paisesService.GetPais(departamento.idPais);                
                proveedor.direccion = null;

                //set de Objeto direccion fiscal
                var direccionFiscal = await _direccionesService.GetDireccion((long)entidadComercialDto.idDireccionFiscal);
                var direccionFiscalDto = _mapper.Map<direccionesDto>(direccionFiscal);
                var municipioF = await _municipiosService.GetMunicipio(direccionFiscal.idMunicipio);
                var departamentoF = await _departamentosService.GetDepartamento(municipioF.idDepartamento);
                var paisF = await _paisesService.GetPais(departamentoF.idPais);                
                proveedor.direccionFiscal = null;

                //direcciones virtuales
                proveedor.vDireccion = direccionClienteDto.direccion + ", " + direccionClienteDto.colonia + ", " +
                                                              direccionClienteDto.zona + ", Codigo Postal " + direccionClienteDto.codigoPostal + ", " +
                                                              municipio.nombreMunicipio + ", " + departamento.nombre + ", " + pais.Nombre;

                proveedor.vDireccionFiscal = direccionFiscalDto.direccion + ", " + direccionFiscalDto.colonia + ", " +
                                                                     direccionFiscalDto.zona + ", Codigo Postal " + direccionFiscalDto.codigoPostal + ", " +
                                                                     municipioF.nombreMunicipio + ", " + departamentoF.nombre + ", " + paisF.Nombre;
                //se busca y se setea el tipo de cliente 
                var tipoDeProveedor = await _tipoProveedoresService.GetTipoProveedor(proveedor.idTipoProveedor);
                var tipoProveedorDto = _mapper.Map<tipoProveedoresDto>(tipoDeProveedor);
                proveedor.tipoProveedor = tipoProveedorDto;

                //se busca y se setea la corporacion
                if (!(entidadComercialDto.idCorporacion is null))
                {
                    var corporacion = await _corporacionesService.GetCorporacion((int)entidadComercialDto.idCorporacion);
                    var corporacionDto = _mapper.Map<corporacionesDto>(corporacion);
                    proveedor.corporacion = corporacionDto;
                }
                else
                {
                    proveedor.corporacion = null;
                }

            }


            var metadata = new Metadata
            {
                TotalCount = proveedores.TotalCount,
                PageSize = proveedores.PageSize,
                CurrentPage = proveedores.CurrentPage,
                TotalPages = proveedores.TotalPages,
                HasNextPage = proveedores.HasNextPage,
                HasPreviousPage = proveedores.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<proveedoresDto>>(proveedoresDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un proveedor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<proveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProveedor(long id)
        {
            var proveedor = await _proveedoresService.GetProveedor(id);
            var proveedorDto = _mapper.Map<proveedoresDto>(proveedor);
                      

            //set de objeto entidad comercial
            var entidadComercial = await _entidadComercialService.GetEntidadComercial(proveedorDto.idEntidadComercial);
            var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);           
            proveedorDto.entidadComercial=entidadComercialDto;

            //set de objeto direccion cliente
            var direccionCliente = await _direccionesService.GetDireccion(proveedorDto.idDireccion);
            var direccionClienteDto = _mapper.Map<direccionesDto>(direccionCliente);
            var municipio = await _municipiosService.GetMunicipio(direccionCliente.idMunicipio);
            var departamento = await _departamentosService.GetDepartamento(municipio.idDepartamento);
            var pais = await _paisesService.GetPais(departamento.idPais);
            //direccionClienteDto.idPais = pais.Id;
            //direccionClienteDto.idDepartamento = departamento.id;           
            proveedorDto.direccion=direccionClienteDto;


            //set de Objeto direccion fiscal
            var direccionFiscal = await _direccionesService.GetDireccion((long)entidadComercialDto.idDireccionFiscal);
            var direccionFiscalDto = _mapper.Map<direccionesDto>(direccionFiscal);
            var municipioF = await _municipiosService.GetMunicipio(direccionFiscal.idMunicipio);
            var departamentoF = await _departamentosService.GetDepartamento(municipioF.idDepartamento);
            var paisF = await _paisesService.GetPais(departamentoF.idPais);
            //direccionFiscalDto.idDepartamento = departamentoF.id;
            //direccionFiscalDto.idPais = paisF.Id;
                       
            proveedorDto.direccionFiscal=direccionFiscalDto;

            var response = new AguilaResponse<proveedoresDto>(proveedorDto);

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo proveedor
        /// </summary>
        /// <param name="proveedorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<proveedoresDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(proveedoresDto proveedorDto)
        {
            if (_proveedoresService.existeCodigo(proveedorDto.codigo, proveedorDto.idEmpresa))
            {
                throw new AguilaException("Codigo de Proveedor ya Registrado!!....", 406);
            }

            //1. se verifica si el nit ya existe en entidad comercial
            var filtroEnditadComercial = new entidadComercialQueryFilter();            
            filtroEnditadComercial.nit = proveedorDto.entidadComercial.nit;

            var currentEntidadComercial = _entidadComercialService.GetEntidadComercial(filtroEnditadComercial);

            if (currentEntidadComercial.Count > 0)
            {
                //if (currentEntidadComercial.ElementAtOrDefault(0).tipo.Equals("C"))
                if (_entidadComercialService.getTipo(currentEntidadComercial.ElementAtOrDefault(0).id, proveedorDto.idEmpresa).Equals("C"))
                {                    
                    proveedorDto.entidadComercial.tipo = "A";
                    proveedorDto.entidadComercial.id = currentEntidadComercial.ElementAtOrDefault(0).id;
                    proveedorDto.entidadComercial.idDireccionFiscal = currentEntidadComercial.ElementAtOrDefault(0).idDireccionFiscal;
                    proveedorDto.direccionFiscal.id = (long)currentEntidadComercial.ElementAtOrDefault(0).idDireccionFiscal;
                    proveedorDto.idEntidadComercial = currentEntidadComercial.ElementAtOrDefault(0).id;
                }
                else
                {
                    throw new AguilaException("Proveedor ya Registrado!!....", 406);
                }
                                
                var entidadComerdial = _mapper.Map<entidadComercial>(proveedorDto.entidadComercial);
                await _entidadComercialService.UpdateEntidadComercial(entidadComerdial);
                                
                var direccioneFisal = _mapper.Map<direcciones>(proveedorDto.direccionFiscal);
                await _direccionesService.UpdateDireccion(direccioneFisal);

                //se guarda la direccion del cliente                
                var direccion = _mapper.Map<direcciones>(proveedorDto.direccion);
                await _direccionesService.InsertDireccion(direccion);
                //se asigna el id que nos genero la insercion de la nueva direccion
                proveedorDto.idDireccion = direccion.id;
            }
            else//2.si no existe 
            {
                //se setea la entidad comercial a tipo  P de Proveedor               
                proveedorDto.entidadComercial.tipo = "P";

                //se guarda la direccion del cliente                
                var direccion = _mapper.Map<direcciones>(proveedorDto.direccion);
                await _direccionesService.InsertDireccion(direccion);

                //se asigna el id que nos genero la insercion de la nueva direccion
                proveedorDto.idDireccion = direccion.id;

                //se guarda la direccion Fiscal                
                var direccionFiscal = _mapper.Map<direcciones>(proveedorDto.direccionFiscal);
                await _direccionesService.InsertDireccion(direccionFiscal);

                // se asigna el id que nos genero la insercion de la nueva direccion Fiscal               
                proveedorDto.entidadComercial.idDireccionFiscal = direccionFiscal.id;

                //se guarda la entidad comercial                
                var entidadComercial = _mapper.Map<entidadComercial>(proveedorDto.entidadComercial);
                await _entidadComercialService.InsertEntidadComercial(entidadComercial);

                //se asigna el id que nos genero la insercion de entidad comercial al cliente
                proveedorDto.idEntidadComercial = entidadComercial.id;

                //se guarda la direccion comercial en el listado de direcciones del cliente
                var direccionComercial = new entidadesComercialesDirecciones();
                direccionComercial.descripcion = "Direccion Comercial";
                direccionComercial.idDireccion = direccion.id;
                direccionComercial.idEntidadComercial = entidadComercial.id;
                await _entidadesComercialesDireccionesService.InsertEntidadComercialDireccion(direccionComercial);

            }


            var proveedor = _mapper.Map<proveedores>(proveedorDto);
            await _proveedoresService.InsertProveedor(proveedor);
            proveedorDto = _mapper.Map<proveedoresDto>(proveedor);
                       
            var response = new AguilaResponse<proveedoresDto>(proveedorDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un proveedor, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proveedorDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<proveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, proveedoresDto proveedorDto)
        {
            //se actualiza Proveedor
            var proveedor = _mapper.Map<proveedores>(proveedorDto);
            proveedor.id = id;

            var result = await _proveedoresService.UpdateProveedor(proveedor);
            if (result)
            {
                //se actualiza entidad comercial
                //var entidad = _mapper.Map<entidadComercial>(proveedorDto.entidadComercial.ElementAtOrDefault(0));
                var entidad = _mapper.Map<entidadComercial>(proveedorDto.entidadComercial);
                await _entidadComercialService.UpdateEntidadComercial(entidad);

                //se actualiza direccion fiscal
                //var direccionFiscal = _mapper.Map<direcciones>(proveedorDto.direccionFiscal.ElementAtOrDefault(0));
                var direccionFiscal = _mapper.Map<direcciones>(proveedorDto.direccionFiscal);
                await _direccionesService.UpdateDireccion(direccionFiscal);

                //se actualiza direccion de cliente
                //var direccion = _mapper.Map<direcciones>(proveedorDto.direccion.ElementAtOrDefault(0));
                var direccion = _mapper.Map<direcciones>(proveedorDto.direccion);
                await _direccionesService.UpdateDireccion(direccion);
            }

            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un proveedor, enviamos id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<proveedoresDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _proveedoresService.DeleteProveedor(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// inactiva un proveedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("/api/proveedores/inactivar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> inactivar(int id)
        {
            var result = await _proveedoresService.inactivar(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/Proveedores/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _proveedoresService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
