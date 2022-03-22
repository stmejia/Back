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
    public class entidadComercialService : IentidadComercialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        //private readonly IclientesService _clientesService;
        //private readonly IproveedoresService _proveedoresService;

        public entidadComercialService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            //_clientesService = clientesService;
            //_proveedoresService = proveedoresService;
        }

        public PagedList<entidadComercial> GetEntidadComercial(entidadComercialQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var entidadComercial = _unitOfWork.entidadComercialRepository.GetAll();

            if (filter.nombre != null)
            {
                entidadComercial = entidadComercial.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.razonSocial != null)
            {
                entidadComercial = entidadComercial.Where(e => e.razonSocial.ToLower().Contains(filter.razonSocial.ToLower()));
            }

            if (filter.idDireccionFiscal != null)
            {
                entidadComercial = entidadComercial.Where(e => e.idDireccionFiscal == filter.idDireccionFiscal); 
            }
           

            if (filter.nit != null)
            {
                entidadComercial = entidadComercial.Where(e => e.nit == filter.nit);
            }

            if (filter.tipoNit != null)
            {
                entidadComercial = entidadComercial.Where(e => e.tipoNit == filter.nit);
            }

            var pagedEntidadComercial = PagedList<entidadComercial>.create(entidadComercial, filter.PageNumber, filter.PageSize);

            return pagedEntidadComercial;
        }

        public async Task<entidadComercial> GetEntidadComercial(long id)
        {
            return await _unitOfWork.entidadComercialRepository.GetByID(id); 
        }

        public async Task InsertEntidadComercial(entidadComercial entidadComercial)
        {
            //Insertamos la fecha de ingreso del registro
            entidadComercial.id = 0;
            entidadComercial.fechaCreacion = DateTime.Now;

            await _unitOfWork.entidadComercialRepository.Add(entidadComercial);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateEntidadComercial(entidadComercial entidadComercial)
        {
            var currentEntidadComercial = await _unitOfWork.entidadComercialRepository.GetByID(entidadComercial.id);
            if (currentEntidadComercial == null)
            {
                throw new AguilaException("Entidad no existente...");
            }

            currentEntidadComercial.nombre = entidadComercial.nombre;
            currentEntidadComercial.razonSocial = entidadComercial.razonSocial;
            currentEntidadComercial.idDireccionFiscal = entidadComercial.idDireccionFiscal;
            //currentEntidadComercial.tipo = entidadComercial.tipo;
            currentEntidadComercial.nit = entidadComercial.nit;
            currentEntidadComercial.tipoNit = entidadComercial.tipoNit;
            currentEntidadComercial.idCorporacion = entidadComercial.idCorporacion;

            _unitOfWork.entidadComercialRepository.Update(currentEntidadComercial);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteEntidadComercial(long id)
        {
            var currentEntidadComercial = await _unitOfWork.entidadComercialRepository.GetByID(id);
            if (currentEntidadComercial == null)
            {
                throw new AguilaException("Entidad no existente...");
            }

            await _unitOfWork.entidadComercialRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }


        public string getTipo(long idEntidad, byte idEmpresa)
        {
            string tipo = "";
            bool esCliente = false;
            bool esProveedor = false;



            var clientes = _unitOfWork.clientesRepository.GetAll();
            clientes = clientes.Where(x=>x.idEntidadComercial==idEntidad && x.idEmpresa==idEmpresa);

            var proveedores = _unitOfWork.proveedoresRepository.GetAll();
            proveedores = proveedores.Where(x => x.idEntidadComercial == idEntidad && x.idEmpresa == idEmpresa);

           

            if (clientes.LongCount() > 0) { esCliente = true; tipo = "C"; }

            if (proveedores.LongCount() > 0) { esProveedor = true; tipo = "P"; }

            if (esCliente && esProveedor) { tipo = "A"; }

            return tipo;
        }
    }
}
