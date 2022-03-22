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
    public class listasService : IlistasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public listasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<listas> GetListas(listasQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var listas = _unitOfWork.listasRepository.GetAll();

            if (filter.valor != null)
            {
                listas = listas.Where(x => x.valor.ToLower().Contains(filter.valor.ToLower()));

            }

            if (filter.descripcion != null)
            {
                listas = listas.Where(x => x.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            if (filter.idEmpresa is null)
            {
                throw new AguilaException("Debe ingresar un id de Empresa");
            }
            else
            {
                listas = listas.Where(x => x.idEmpresa == filter.idEmpresa);
            }

            if (filter.idTipoLista != null)
            {
                listas = listas.Where(x => x.idTipoLista == filter.idTipoLista);
            }

            var pagedListas = PagedList<listas>.create(listas, filter.PageNumber, filter.PageSize);

            return pagedListas;
        }

        public async Task<listas> GetLista(int id)
        {
            return await _unitOfWork.listasRepository.GetByID(id);
        }

        public async Task InserLista(listas lista)
        {
            lista.id = 0;
            lista.fechaCreacion = DateTime.Now;           
            await _unitOfWork.listasRepository.Add(lista);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateLista(listas lista)
        {
            var currentLista = await _unitOfWork.listasRepository.GetByID(lista.id);
            if (currentLista == null)
            {
                throw new AguilaException("Lista No Existente!....");
            }

            currentLista.valor = lista.valor; ;
            currentLista.descripcion = lista.descripcion;
            currentLista.idEmpresa = lista.idEmpresa;
            currentLista.idTipoLista = lista.idTipoLista;

            _unitOfWork.listasRepository.Update(currentLista);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteLista(int id)
        {
            var currentLista = await _unitOfWork.listasRepository.GetByID(id);
            if (currentLista == null)
            {
                throw new AguilaException("Lista No Existente!....");
            }

            await _unitOfWork.listasRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
