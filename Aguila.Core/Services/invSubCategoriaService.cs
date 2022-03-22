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
    public class invSubCategoriaService : IinvSubCategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public invSubCategoriaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<invSubCategoria> GetInvSubCategoria(invSubCategoriaQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var invSubCategoria = _unitOfWork.invSubCategoriaRepository.GetAll();

            if (filter.idInvCategoria != null)
            {
                invSubCategoria = invSubCategoria.Where(e => e.idInvCategoria == filter.idInvCategoria);
            }

            if (filter.codigo != null)
            {
                invSubCategoria = invSubCategoria.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.descripcion != null)
            {
                invSubCategoria = invSubCategoria.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            var pagedInvSubCategoria = PagedList<invSubCategoria>.create(invSubCategoria, filter.PageNumber, filter.PageSize);
            return pagedInvSubCategoria;
        }

        public async Task<invSubCategoria> GetInvSubCategoria(int id)
        {
            return await _unitOfWork.invSubCategoriaRepository.GetByID(id);
        }

        public async Task InsertInvSubCategoria(invSubCategoria invSubCategoria)
        {
            //Insertamos la fecha de ingreso del registro
            invSubCategoria.id = 0;
            invSubCategoria.fechaCreacion = DateTime.Now;

            await _unitOfWork.invSubCategoriaRepository.Add(invSubCategoria);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateInvSubCategoria(invSubCategoria invSubCategoria)
        {
            var currentSubCategoria = await _unitOfWork.invSubCategoriaRepository.GetByID(invSubCategoria.id);
            if (currentSubCategoria == null)
            {
                throw new AguilaException("Sub Categoria no existente...");
            }

            currentSubCategoria.idInvCategoria = invSubCategoria.idInvCategoria;
            currentSubCategoria.codigo = invSubCategoria.codigo;
            currentSubCategoria.descripcion = invSubCategoria.descripcion;

            _unitOfWork.invSubCategoriaRepository.Update(currentSubCategoria);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteInvSubCategoria(int id)
        {
            var currentSubCategoria = await _unitOfWork.invSubCategoriaRepository.GetByID(id);
            if (currentSubCategoria == null)
            {
                throw new AguilaException("Sub Categoria no existente...");
            }

            await _unitOfWork.invSubCategoriaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
