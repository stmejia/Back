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
    public class RecursosAtributosService : IRecursosAtributosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public RecursosAtributosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //devuelve el listado de RecursosAtributos Existentes 
        public PagedList<RecursosAtributos> GetRecursosAtributos(RecursosAtributosQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var recursosAtributos = _unitOfWork.RecursosAtributosRepository.GetAll();

            if(filter.Nombre != null)
            {
                recursosAtributos = recursosAtributos.Where(x => x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if(filter.RecursoId != null)
            {
                recursosAtributos = recursosAtributos.Where(x => x.RecursoId == filter.RecursoId);
            }

            var pagedRecursosAtributos = PagedList<RecursosAtributos>.create(recursosAtributos, filter.PageNumber, filter.PageSize);

            return pagedRecursosAtributos;
        }

        //devuelve un recursoAtributo por su ID
        public async Task<RecursosAtributos> GetRecursoAtributo(int id)
        {
            return await _unitOfWork.RecursosAtributosRepository.GetByID(id);
        }

        //inserta un nuevo recursoAtributo
        public async Task InsertRecursoAtributo(RecursosAtributos recursoAtributo)
        {
            //valida que existe el recurso antes de insertar el recursoAtributo
            var recurso = await _unitOfWork.RecursosRepository.GetByID(recursoAtributo.RecursoId);
            if (recurso == null)
            {
                throw new AguilaException("Recurso No Existente!....");
            }
            recursoAtributo.Id =0;//reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)

            await _unitOfWork.RecursosAtributosRepository.Add(recursoAtributo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> updateRecursoAtributo(RecursosAtributos recursoAtributo)
        {
            var currentRecursoAtributo = await _unitOfWork.RecursosAtributosRepository.GetByID(recursoAtributo.Id);
            if (currentRecursoAtributo == null)
            {
                throw new AguilaException("RecursoAtributo No Existente!....");
            }

            //valida que el Recurso a actualizar exista
            var recurso = await _unitOfWork.RecursosRepository.GetByID(recursoAtributo.RecursoId);
            if (recurso == null)
            {
                throw new AguilaException("Recurso No Existente!....");
            }

            currentRecursoAtributo.Codigo = recursoAtributo.Codigo;
            currentRecursoAtributo.Nombre = recursoAtributo.Nombre;
            currentRecursoAtributo.RecursoId = recursoAtributo.RecursoId;

            _unitOfWork.RecursosAtributosRepository.Update(currentRecursoAtributo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteRecursoAtributo(int id)
        {
            //se valida que el recursoAtributo a eliminar exista
            var currentRecursoAtributo = await _unitOfWork.RecursosAtributosRepository.GetByID(id);
            if (currentRecursoAtributo == null)
            {
                throw new AguilaException("RecursoAtributo No Existente!....");
            }

            await _unitOfWork.RecursosAtributosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
