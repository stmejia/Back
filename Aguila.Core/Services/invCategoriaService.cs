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
    public class invCategoriaService : IinvCategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public invCategoriaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<invCategoria> GetInvCategoria(invCategoriaQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var invCategoria = _unitOfWork.invCategoriaRepository.GetAll();

            if (filter.codigo != null)
            {
                invCategoria = invCategoria.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.descripcion != null)
            {
                invCategoria = invCategoria.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            if (filter.idEmpresa != null)
            {
                invCategoria = invCategoria.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            var pagedInvCategoria = PagedList<invCategoria>.create(invCategoria, filter.PageNumber, filter.PageSize);
            return pagedInvCategoria;
        }

        public async Task<invCategoria> GetInvCategoria(int id)
        {
            return await _unitOfWork.invCategoriaRepository.GetByID(id);
        }

        public async Task InsertInvCategoria(invCategoria invCategoria)
        {
            //Insertamos la fecha de ingreso del registro
            invCategoria.id = 0;
            invCategoria.fechaCreacion = DateTime.Now;

            await _unitOfWork.invCategoriaRepository.Add(invCategoria);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateInvCategoria(invCategoria invCategoria)
        {
            var currentCategoria = await _unitOfWork.invCategoriaRepository.GetByID(invCategoria.id);
            if (currentCategoria == null)
            {
                throw new AguilaException("Categoria no existente...");
            }

            currentCategoria.codigo = invCategoria.codigo;
            currentCategoria.descripcion = invCategoria.descripcion;
            currentCategoria.idEmpresa = invCategoria.idEmpresa;

            _unitOfWork.invCategoriaRepository.Update(currentCategoria);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteInvCategoria(int id)
        {
            var currentCategoria = await _unitOfWork.invCategoriaRepository.GetByID(id);
            if (currentCategoria == null)
            {
                throw new AguilaException("Categoria no existente...");
            }

            await _unitOfWork.invCategoriaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
