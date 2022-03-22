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
    public class clientesController : ControllerBase
    {
        private readonly IclientesService _clientesService;
        private readonly IdireccionesService _direccionesService;
        private readonly ImunicipiosService _municipiosService;
        private readonly IdepartamentosService _departamentosService;
        private readonly IpaisesService _paisesService;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IentidadesComercialesDireccionesService _entidadesComercialesDireccionesService;
        private readonly ItipoClientesService _tipoClientesService;
        private readonly IcorporacionesService _corporacionesService;

        public clientesController(IclientesService clientesService, IMapper mapper, IPasswordService passwordService,
                                  IdireccionesService direccionesService,
                                  IentidadComercialService entidadComercialService,
                                  ImunicipiosService municipiosService,
                                  IdepartamentosService departamentosService,
                                  IpaisesService paisesService,
                                  IentidadesComercialesDireccionesService entidadesComercialesDireccionesService,
                                  ItipoClientesService tipoClientesService,
                                  IcorporacionesService corporacionesService)
        {
            _clientesService = clientesService;
            _direccionesService = direccionesService;
            _entidadComercialService = entidadComercialService;
            _municipiosService = municipiosService;
            _departamentosService = departamentosService;
            _paisesService = paisesService;
            _mapper = mapper;
            _passwordService = passwordService;
            _entidadesComercialesDireccionesService = entidadesComercialesDireccionesService;
            _tipoClientesService = tipoClientesService;
            _corporacionesService = corporacionesService;
        }

        /// <summary>
        /// Obtiene las entidades registradas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<entidadComercialDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClientes([FromQuery] clientesQueryFilter filter)
        {
            var clientes = _clientesService.GetClientes(filter);
            var clientesDto = _mapper.Map<IEnumerable<clientesDto>>(clientes);

            //si se filtro por Nit
            //if (filter.nit != null)
            //{
            foreach (var cliente in clientesDto)
            {
                //set de objeto entidad comercial
                var entidadComercial = await _entidadComercialService.GetEntidadComercial(cliente.idEntidadComercial);
                var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);
                //cliente.entidadComercial.Add(entidadComercialDto);
                cliente.entidadComercial = entidadComercialDto;

                //set de objeto direccion cliente
                var direccionCliente = await _direccionesService.GetDireccion(cliente.idDireccion);
                var direccionClienteDto = _mapper.Map<direccionesDto>(direccionCliente);
                var municipio = await _municipiosService.GetMunicipio(direccionCliente.idMunicipio);
                var departamento = await _departamentosService.GetDepartamento(municipio.idDepartamento);
                var pais = await _paisesService.GetPais(departamento.idPais);
                //clientesDto.ElementAtOrDefault(0).direccion.Add(direccionClienteDto);
                cliente.direccion = null;

                //set de Objeto direccion fiscal
                var direccionFiscal = await _direccionesService.GetDireccion((long)entidadComercialDto.idDireccionFiscal);
                var direccionFiscalDto = _mapper.Map<direccionesDto>(direccionFiscal);
                var municipioF = await _municipiosService.GetMunicipio(direccionFiscal.idMunicipio);
                var departamentoF = await _departamentosService.GetDepartamento(municipioF.idDepartamento);
                var paisF = await _paisesService.GetPais(departamentoF.idPais);
                //direccionFiscalDto.idDepartamento = entidadComercialDto.direccionFiscal.idDepartamento;
                //direccionFiscalDto.idPais = entidadComercialDto.direccionFiscal.idPais;

                //clientesDto.ElementAtOrDefault(0).direccionFiscal.Add(direccionFiscalDto);
                cliente.direccionFiscal = null;


                cliente.vDireccion = direccionClienteDto.direccion + ", " + direccionClienteDto.colonia + ", " +
                                                              direccionClienteDto.zona + ", Codigo Postal " + direccionClienteDto.codigoPostal + ", " +
                                                              municipio.nombreMunicipio + ", " + departamento.nombre + ", " + pais.Nombre;

                cliente.vDireccionFiscal = direccionFiscalDto.direccion + ", " + direccionFiscalDto.colonia + ", " +
                                                                     direccionFiscalDto.zona + ", Codigo Postal " + direccionFiscalDto.codigoPostal + ", " +
                                                                     municipioF.nombreMunicipio + ", " + departamentoF.nombre + ", " + paisF.Nombre;

                //se busca y se setea el tipo de cliente 
                var tipoDeCliente = await _tipoClientesService.GetTipoCliente(cliente.idTipoCliente);
                var tipoClienteDto = _mapper.Map<tipoClientesDto>(tipoDeCliente);
                cliente.tipoCliente = tipoClienteDto;

                //se busca y se setea la corporacion
                if(!(entidadComercialDto.idCorporacion is null)) {
                    var corporacion = await _corporacionesService.GetCorporacion((int)entidadComercialDto.idCorporacion);
                    var corporacionDto = _mapper.Map<corporacionesDto>(corporacion);
                    cliente.corporacion = corporacionDto;
                }
                else
                {
                    cliente.corporacion = null;
                }
                

            }
            //}

            var metadata = new Metadata
            {
                TotalCount = clientes.TotalCount,
                PageSize = clientes.PageSize,
                CurrentPage = clientes.CurrentPage,
                TotalPages = clientes.TotalPages,
                HasNextPage = clientes.HasNextPage,
                HasPreviousPage = clientes.HasPreviousPage
            };
            // clientesDto.ElementAtOrDefault(0).entidadComercial.ElementAtOrDefault(0).direccionFiscal = null;
            var response = new AguilaResponse<IEnumerable<clientesDto>>(clientesDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }


        /// <summary>
        /// Consulta de cliente, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<clientesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _clientesService.GetCliente(id);
            var clienteDto = _mapper.Map<clientesDto>(cliente);


            //set de objeto entidad comercial
            var entidadComercial = await _entidadComercialService.GetEntidadComercial(clienteDto.idEntidadComercial);
            var entidadComercialDto = _mapper.Map<entidadComercialDto>(entidadComercial);
            //clienteDto.entidadComercial.Add(entidadComercialDto);
            clienteDto.entidadComercial = entidadComercialDto;

            //set de objeto direccion cliente
            var direccionCliente = await _direccionesService.GetDireccion(clienteDto.idDireccion);
            var direccionClienteDto = _mapper.Map<direccionesDto>(direccionCliente);
            var municipio = await _municipiosService.GetMunicipio(direccionCliente.idMunicipio);
            var departamento = await _departamentosService.GetDepartamento(municipio.idDepartamento);
            var pais = await _paisesService.GetPais(departamento.idPais);
            //direccionClienteDto.idPais = pais.Id;
            //direccionClienteDto.idDepartamento = departamento.id;
            //clienteDto.direccion.Add(direccionClienteDto);
            clienteDto.direccion = direccionClienteDto;


            //set de Objeto direccion fiscal
            var direccionFiscal = await _direccionesService.GetDireccion((long)entidadComercialDto.idDireccionFiscal);
            var direccionFiscalDto = _mapper.Map<direccionesDto>(direccionFiscal);
            var municipioF = await _municipiosService.GetMunicipio(direccionFiscal.idMunicipio);
            var departamentoF = await _departamentosService.GetDepartamento(municipioF.idDepartamento);
            var paisF = await _paisesService.GetPais(departamentoF.idPais);
            //direccionFiscalDto.idDepartamento = departamentoF.id;
            //direccionFiscalDto.idPais = paisF.Id;

            //clienteDto.direccionFiscal.Add(direccionFiscalDto);
            clienteDto.direccionFiscal = direccionFiscalDto;
            //clientesDto.ElementAtOrDefault(0).direccionFiscal = null;

            var response = new AguilaResponse<clientesDto>(clienteDto);
            return Ok(response);
        }



        /// <summary>
        /// Crea Cliente Nuevo
        /// </summary>      
        /// <param name="clientesDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<clientesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(clientesDto clientesDto)
        {

            if(_clientesService.existeCodigo(clientesDto.codigo, clientesDto.idEmpresa))
            {
                throw new AguilaException("Codigo de Cliente ya Registrado!!....", 406);
            }

            //1. se verifica si el nit ya existe en entidad comercial
            var filtroEnditadComercial = new entidadComercialQueryFilter();
            filtroEnditadComercial.nit = clientesDto.entidadComercial.nit;

            var currentEntidadComercial = _entidadComercialService.GetEntidadComercial(filtroEnditadComercial);

            if (currentEntidadComercial.Count > 0)
            {
                //if (currentEntidadComercial.ElementAtOrDefault(0).tipo.Equals("P")) 
                if (_entidadComercialService.getTipo(currentEntidadComercial.ElementAtOrDefault(0).id, clientesDto.idEmpresa).Equals("P"))
                {

                    clientesDto.entidadComercial.tipo = "A";
                    clientesDto.entidadComercial.id = currentEntidadComercial.ElementAtOrDefault(0).id;
                    clientesDto.entidadComercial.idDireccionFiscal = currentEntidadComercial.ElementAtOrDefault(0).idDireccionFiscal;
                    clientesDto.direccionFiscal.id = (long)currentEntidadComercial.ElementAtOrDefault(0).idDireccionFiscal;

                    clientesDto.idEntidadComercial = currentEntidadComercial.ElementAtOrDefault(0).id;
                }
                else
                {
                    throw new AguilaException("Cliente ya Registrado!!....", 406);
                }

                var entidadComerdial = _mapper.Map<entidadComercial>(clientesDto.entidadComercial);
                await _entidadComercialService.UpdateEntidadComercial(entidadComerdial);

                var direccioneFisal = _mapper.Map<direcciones>(clientesDto.direccionFiscal);
                await _direccionesService.UpdateDireccion(direccioneFisal);

                //se guarda la direccion del cliente               
                var direccion = _mapper.Map<direcciones>(clientesDto.direccion);
                await _direccionesService.InsertDireccion(direccion);
                //se asigna el id que nos genero la insercion de la nueva direccion
                clientesDto.idDireccion = direccion.id;
            }
            else//2.si no existe 
            {
                //se setea la entidad comercial a tipo  C de cliente                
                clientesDto.entidadComercial.tipo = "C";

                //se guarda la direccion del cliente              
                var direccion = _mapper.Map<direcciones>(clientesDto.direccion);
                await _direccionesService.InsertDireccion(direccion);

                //se asigna el id que nos genero la insercion de la nueva direccion
                clientesDto.idDireccion = direccion.id;

                //se guarda la direccion Fiscal                
                var direccionFiscal = _mapper.Map<direcciones>(clientesDto.direccionFiscal);
                await _direccionesService.InsertDireccion(direccionFiscal);

                // se asigna el id que nos genero la insercion de la nueva direccion Fiscal               
                clientesDto.entidadComercial.idDireccionFiscal = direccionFiscal.id;

                //se guarda la entidad comercial                
                var entidadComercial = _mapper.Map<entidadComercial>(clientesDto.entidadComercial);
                await _entidadComercialService.InsertEntidadComercial(entidadComercial);

                //se asigna el id que nos genero la insercion de entidad comercial al cliente
                clientesDto.idEntidadComercial = entidadComercial.id;

                //se guarda la direccion comercial en el listado de direcciones del cliente
                var direccionComercial = new entidadesComercialesDirecciones();
                direccionComercial.descripcion = "Direccion Comercial";
                direccionComercial.idDireccion = direccion.id;
                direccionComercial.idEntidadComercial = entidadComercial.id;
                await _entidadesComercialesDireccionesService.InsertEntidadComercialDireccion(direccionComercial);
            }

            //se guarda el cliente
            var cliente = _mapper.Map<clientes>(clientesDto);
            await _clientesService.InsertCliente(cliente);

            clientesDto = _mapper.Map<clientesDto>(cliente);
            //clientesDto.id = cliente.id;
            var response = new AguilaResponse<clientesDto>(clientesDto);
            return Ok(response);
        }



        /// <summary>
        /// Actualizacion Clientes, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clienteDTo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, clientesDto clienteDTo)
        {
            //se actualiza cliente
            var cliente = _mapper.Map<clientes>(clienteDTo);
            cliente.id = id;

            var result = await _clientesService.UpdateCliente(cliente);

            if (result)
            {
                //se actualiza entidad comercial
                //var entidad = _mapper.Map<entidadComercial>(clienteDTo.entidadComercial.ElementAtOrDefault(0));
                var entidad = _mapper.Map<entidadComercial>(clienteDTo.entidadComercial);
                await _entidadComercialService.UpdateEntidadComercial(entidad);

                //se actualiza direccion fiscal
                //var direccionFiscal = _mapper.Map<direcciones>(clienteDTo.direccionFiscal.ElementAtOrDefault(0));
                var direccionFiscal = _mapper.Map<direcciones>(clienteDTo.direccionFiscal);
                await _direccionesService.UpdateDireccion(direccionFiscal);

                //se actualiza direccion de cliente
                //var direccion = _mapper.Map<direcciones>(clienteDTo.direccion.ElementAtOrDefault(0));
                var direccion = _mapper.Map<direcciones>(clienteDTo.direccion);
                await _direccionesService.UpdateDireccion(direccion);
            }


            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }



        /// <summary>
        /// inactiva un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("/api/clientes/inactivar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> inactivar(int id)
        {
            var result = await _clientesService.inactivar(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }


        /// <summary>
        /// Obtiene el recurso
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("api/clientes/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<Recursos>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var recurso = await _clientesService.GetRecursoByControlador(controlador);
            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
