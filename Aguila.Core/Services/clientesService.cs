using Aguila.Core.Interfaces.Services;
using Aguila.Core.Entities;
using Aguila.Core.CustomEntities;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Services
{
    public class clientesService : IclientesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly PaginationOptions _paginationOptions;

        public clientesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                               IentidadComercialService entidadComercialService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _entidadComercialService = entidadComercialService;
        }

        public PagedList<clientes> GetClientes(clientesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var clientes = _unitOfWork.clientesRepository.GetAll();
            

            if (filter.nit != null)
            {
               
                var filterEntidad = new entidadComercialQueryFilter();
                filterEntidad.nit = filter.nit;

                var entidadComercial = _entidadComercialService.GetEntidadComercial(filterEntidad);
                if (entidadComercial.Count > 0)
                {


                    //if (entidadComercial.ElementAtOrDefault(0).tipo.ToLower().Equals("c") || entidadComercial.ElementAtOrDefault(0).tipo.ToLower().Equals("a"))
                    //{
                        clientes = clientes.Where(e => e.idEntidadComercial == entidadComercial.ElementAtOrDefault(0).id);

                        if (clientes.Count() > 0) {
                            if (clientes.ElementAtOrDefault(0).fechaBaja != null)//si el cliente esta dado de baja
                            {
                                throw new AguilaException("Cliente inactivo desde " + clientes.ElementAtOrDefault(0).fechaBaja, 400);
                            }
                        }
                        else
                        {
                            throw new AguilaException("Cliente No existente", 406);
                        }


                   // }
                    //else
                    //{
                    //    throw new AguilaException("Cliente No existente", 406);
                    //}

                }
                else
                {
                    throw new AguilaException("Cliente No existente", 406);
                }
            }

            //si la busqueda no fue por NIT se excluyen los clientes dados de baja
            clientes = clientes.Where(e => e.fechaBaja == null);//se excluyen los clientes dados de baja

            if (filter.idTipoCliente != null)
            {
                clientes = clientes.Where(e => e.idTipoCliente== filter.idTipoCliente );
            }

            if (filter.idDireccion != null)
            {
                clientes = clientes.Where(e => e.idDireccion == filter.idDireccion );
            }

            if (filter.idEntidadComercial != null)
            {
                clientes = clientes.Where(e => e.idEntidadComercial == filter.idEntidadComercial );
            }

            if (filter.codigo != null)
            {
               clientes = clientes.Where(e => e.codigo == filter.codigo );
            }

            if (filter.diasCredito != null)
            {
                clientes = clientes.Where(e => e.diasCredito == filter.diasCredito);
            }

            if (filter.idEmpresa != null)
            {
                clientes = clientes.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            clientes = clientes.OrderByDescending(e => e.fechaCreacion);

            var pagedClientes = PagedList<clientes>.create(clientes, filter.PageNumber, filter.PageSize);
            return pagedClientes;
        }

        public async Task<clientes> GetCliente(int id)
        {
            var currentCliente = await _unitOfWork.clientesRepository.GetByID(id);
            if (currentCliente == null)
            {
                throw new AguilaException("Cliente no existente...");
            }

            if (currentCliente.fechaBaja != null)
            {
                throw new AguilaException("Cliente inactivo desde " + currentCliente.fechaBaja);
            }

            return await _unitOfWork.clientesRepository.GetByID(id);
        }

        public async Task InsertCliente(clientes cliente)
        {
            //Insertamos la fecha de ingreso del registro
            cliente.id = 0;
            cliente.fechaCreacion = DateTime.Now;

            await _unitOfWork.clientesRepository.Add(cliente);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateCliente(clientes cliente)
        {
            var currentCliente = await _unitOfWork.clientesRepository.GetByID(cliente.id);
            if (currentCliente == null)
            {
                throw new AguilaException("Cliente no existente...");
            }

            currentCliente.idTipoCliente = cliente.idTipoCliente;
            currentCliente.idDireccion = cliente.idDireccion;
            currentCliente.idEntidadComercial = cliente.idEntidadComercial;
            //currentCliente.idCorporacion = cliente.idCorporacion;
            currentCliente.diasCredito = cliente.diasCredito;
            //currentCliente.fechaBaja = cliente.fechaBaja;

            _unitOfWork.clientesRepository.Update(currentCliente);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

       
        public async Task<bool> DeleteCliente(int id)
        {
            var currentCliente = await _unitOfWork.clientesRepository.GetByID(id);
            if (currentCliente == null)
            {
                throw new AguilaException("Cliente no existente...");
            }           

            await _unitOfWork.clientesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

           
            return true;
        }

        public async Task<bool> inactivar(int id)
        {
            var currentCliente = await _unitOfWork.clientesRepository.GetByID(id);
            if (currentCliente == null)
            {
                throw new AguilaException("Cliente no existente...");
            }

            if (currentCliente.fechaBaja !=null)
            {
                throw new AguilaException("Este cliente ya se encuentra de baja...!");
            }

           
            currentCliente.fechaBaja = DateTime.Now;

            _unitOfWork.clientesRepository.Update(currentCliente);
            await _unitOfWork.SaveChangeAsync();

            return true;

        }

        public bool existeCodigo(string codigo, byte empresa)
        {            
            var filter = new clientesQueryFilter();
            filter.codigo = codigo;
            filter.idEmpresa = empresa;

            var cliente = GetClientes(filter);

            if (cliente.Count > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
