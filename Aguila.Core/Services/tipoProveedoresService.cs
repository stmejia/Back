using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
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
    public class tipoProveedoresService : ItipoProveedoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoProveedoresService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoProveedores> GetTiposProveedores(tipoProveedoresQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipos = _unitOfWork.tipoProveedoresRepository.GetAll();

            if (filter.codigo != null)
            {
                tipos = tipos.Where(x => x.codigo.ToLower().Contains(filter.codigo.ToLower()));

            }

            if (filter.descripcion != null)
            {
                tipos = tipos.Where(x => x.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }


            var pagedTipos = PagedList<tipoProveedores>.create(tipos, filter.PageNumber, filter.PageSize);

            return pagedTipos;
        }

        public async Task<tipoProveedores> GetTipoProveedor(int id)
        {
            return await _unitOfWork.tipoProveedoresRepository.GetByID(id);
        }

        public async Task InsertTipoProveedor(tipoProveedores tipo)
        {
            tipo.id = 0;
            tipo.fechaCreacion = DateTime.Now;
            await _unitOfWork.tipoProveedoresRepository.Add(tipo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoProveedor(tipoProveedores tipo)
        {
            var currentTipo = await _unitOfWork.tipoProveedoresRepository.GetByID(tipo.id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Proveedor No Existente!....");
            }

            //currentTipo.codigo = tipo.codigo;
            currentTipo.descripcion = tipo.descripcion;


            _unitOfWork.tipoProveedoresRepository.Update(currentTipo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoProveedor(int id)
        {
            var currentTipo = await _unitOfWork.tipoProveedoresRepository.GetByID(id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Proveedor No Existente!....");
            }

            await _unitOfWork.tipoProveedoresRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
