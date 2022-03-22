using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aguila.Core.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public SucursalService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Sucursales> GetSucursales(SucursalQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var sucursales = _unitOfWork.SucursalRepository.GetAll();

            if (filter.Activa != null)
            {
                sucursales = sucursales.Where(x => x.Activa == filter.Activa);
            }

            if (filter.Nombre != null)
            {
                sucursales = sucursales.Where(x => x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if(filter.EmpresaId != null)
            {
                sucursales = sucursales.Where(x => x.EmpresaId == filter.EmpresaId);
            }

            var pagedSucursales = PagedList<Sucursales>.create(sucursales, filter.PageNumber, filter.PageSize);

            return  pagedSucursales;
        }

        public async Task<Sucursales> GetSucursal(short id)
        {
            return await _unitOfWork.SucursalRepository.GetByID(id);
        }

        public async Task InsertSucursal(Sucursales sucursal)
        {
            //valida que exista la empresa a la que pertenece la nueva sucursal
            var existeEmpresa = await _unitOfWork.EmpresaRepository.GetByID(sucursal.EmpresaId);
            if(existeEmpresa == null)
            {
                throw new AguilaException("Empresa de Sucursal No Existente!....");
            }

            sucursal.Id = 0;
            sucursal.FchCreacion = DateTime.Now;
            await _unitOfWork.SucursalRepository.Add(sucursal);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> updateSucursal(Sucursales sucursal)
        {
            //valida que la sucursal a actualizar exista
            var currentSucursal = await _unitOfWork.SucursalRepository.GetByID(sucursal.Id);
            if (currentSucursal == null)
            {
                throw new AguilaException("Sucursal No Existente!....");
            }

            //valida que la empresa a actualizar exista
            var existeEmpresa = await _unitOfWork.EmpresaRepository.GetByID(sucursal.EmpresaId);                        
            if (existeEmpresa == null)
            {
                throw new AguilaException("Empresa de Sucursal No Existente!....");
            }

            //currentSucursal.Codigo = sucursal.Codigo;
            currentSucursal.Nombre = sucursal.Nombre;
            currentSucursal.Direccion = sucursal.Direccion;
            //currentSucursal.Activa = sucursal.Activa;
            //currentSucursal.FchCreacion = sucursal.FchCreacion;

            _unitOfWork.SucursalRepository.Update(currentSucursal);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteSucursal(short id)
        {
            //se valida que la empresa a eliminar exista
            var sucursal = await _unitOfWork.SucursalRepository.GetByID(id);
            if (sucursal == null)
            {
                throw new AguilaException("Sucursal No Existente!....");
            }

            await _unitOfWork.SucursalRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
