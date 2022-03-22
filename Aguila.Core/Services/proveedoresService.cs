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
    public class proveedoresService : IproveedoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IentidadComercialService _entidadComercialService;
        private readonly PaginationOptions _paginationOptions;

        public proveedoresService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                               IentidadComercialService entidadComercialService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _entidadComercialService = entidadComercialService;
        }

        public PagedList<proveedores> GetProveedores(proveedoresQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            //var proveedores = _unitOfWork.proveedoresRepository.GetAll();
            var queryProveedores = _unitOfWork.proveedoresRepository.GetAll();

            Boolean tieneFilter = false;

            if (filter.nit != null)
            {
                tieneFilter = true;
                var filterEntidad = new entidadComercialQueryFilter();
                filterEntidad.nit = filter.nit;

                var entidadComercial = _entidadComercialService.GetEntidadComercial(filterEntidad);
                if (entidadComercial.Count > 0)
                {


                    //if (entidadComercial.ElementAtOrDefault(0).tipo.ToLower().Equals("p") || entidadComercial.ElementAtOrDefault(0).tipo.ToLower().Equals("a"))
                    //{
                    queryProveedores = queryProveedores.Where(e => e.idEntidadComercial == entidadComercial.ElementAtOrDefault(0).id);

                        if (queryProveedores.Count() > 0)
                        {
                            if (queryProveedores.ElementAtOrDefault(0).fechaBaja != null)//si el cliente esta dado de baja
                            {
                                throw new AguilaException("Proveedor inactivo desde " + queryProveedores.ElementAtOrDefault(0).fechaBaja, 400);
                            }
                        }
                        else
                        {
                            throw new AguilaException("Proveedor No existente", 406);
                        }


                    //}
                    //else
                    //{
                    //    throw new AguilaException("Proveedor No existente", 406);
                    //}

                }
                else
                {
                    throw new AguilaException("Proveedor No existente", 406);
                }
            }

            //si la busqueda no fue por NIT se excluyen los clientes dados de baja
            //queryProveedores = queryProveedores.Where(e => e.fechaBaja is null);//se excluyen los clientes dados de baja
            queryProveedores = queryProveedores.Where(e => e.fechaBaja == null);//se excluyen los clientes dados de baja

            if (filter.codigo != null)
            {
                queryProveedores = queryProveedores.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
                tieneFilter = true;
            }

            if (filter.idDireccion != null)
            {
                queryProveedores = queryProveedores.Where(e => e.idDireccion == filter.idDireccion);
                tieneFilter = true;
            }

            if (filter.idTipoProveedor != null)
            {
                queryProveedores = queryProveedores.Where(e => e.idTipoProveedor == filter.idTipoProveedor);
                tieneFilter = true;
            }

            //if (filter.idCorporacion != null)
            //{
            //    proveedores = proveedores.Where(e => e.idCorporacion == filter.idCorporacion);
            //    tieneFilter = true;
            //}

            if (filter.idEntidadComercial != null)
            {
                queryProveedores = queryProveedores.Where(e => e.idEntidadComercial == filter.idEntidadComercial);
                tieneFilter = true;
            }

            if (filter.idEmpresa != null)
            {
                queryProveedores = queryProveedores.Where(e => e.idEmpresa == filter.idEmpresa);
                tieneFilter = true;
            }

            if (tieneFilter == false)
            {
                throw new AguilaException("Debe enviar al menos un criterio de Filtro!....", 406);
            }

            queryProveedores = queryProveedores.OrderByDescending(e => e.fechaCreacion);

            var pagedProveedores = PagedList<proveedores>.create(queryProveedores, filter.PageNumber, filter.PageSize);
            return pagedProveedores;
        }

        public async Task<proveedores> GetProveedor(long id)
        {
            var proveedor = await _unitOfWork.proveedoresRepository.GetByID(id);
            if (proveedor == null)
            {
                throw new AguilaException("Proveedor No existente", 406);
            }

            if (proveedor.fechaBaja != null)
            {
                throw new AguilaException("Cliente inactivo desde " + proveedor.fechaBaja);
            }

            return proveedor;
        }

        public async Task InsertProveedor(proveedores proveedor)
        {
            proveedoresQueryFilter filter = new proveedoresQueryFilter();
            filter.codigo = proveedor.codigo;
            filter.idTipoProveedor = proveedor.idTipoProveedor;
            //filter.idCorporacion = proveedor.idCorporacion;

            var currentProveedor = GetProveedores(filter);
            if (currentProveedor.LongCount() > 0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este código en el tipo o corporacion indicados....", 406);
            }

            //Insertamos la fecha de ingreso del registro
            proveedor.id = 0;
            proveedor.fechaCreacion = DateTime.Now;
            //proveedor.corporacion = null;
            proveedor.direccion = null;
            proveedor.entidadComercial = null;
            proveedor.empresa = null;
            proveedor.tipoProveedor = null;

            await _unitOfWork.proveedoresRepository.Add(proveedor);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateProveedor(proveedores proveedor)
        {
            var currentProveedor = await _unitOfWork.proveedoresRepository.GetByID(proveedor.id);
            if (currentProveedor == null)
            {
                throw new AguilaException("Proveedor no existente...");
            }

            currentProveedor.idDireccion = proveedor.idDireccion;
            currentProveedor.idTipoProveedor = proveedor.idTipoProveedor;
            //currentProveedor.idCorporacion = proveedor.idCorporacion;
            currentProveedor.idEntidadComercial = proveedor.idEntidadComercial;
            currentProveedor.idEmpresa = proveedor.idEmpresa;
            currentProveedor.fechaBaja = proveedor.fechaBaja;

            _unitOfWork.proveedoresRepository.Update(currentProveedor);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteProveedor(long id)
        {
            var currentProveedor = await _unitOfWork.proveedoresRepository.GetByID(id);
            if (currentProveedor == null)
            {
                throw new AguilaException("Proveedor no existente...");
            }

            await _unitOfWork.proveedoresRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> inactivar(int id)
        {
            var proveedor = await _unitOfWork.proveedoresRepository.GetByID(id);
            if (proveedor == null)
            {
                throw new AguilaException("Proveedor no existente...");
            }

            if (proveedor.fechaBaja != null)
            {
                throw new AguilaException("Este Proveedor ya se encuentra de baja...!");
            }


            proveedor.fechaBaja = DateTime.Now;

            _unitOfWork.proveedoresRepository.Update(proveedor);
            await _unitOfWork.SaveChangeAsync();

            return true;

        }

        public bool existeCodigo(string codigo, byte empresa)
        {
            var filter = new proveedoresQueryFilter();
            filter.codigo = codigo;
            filter.idEmpresa = empresa;

            var proveedor = GetProveedores(filter);

            if (proveedor.Count > 0)
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
