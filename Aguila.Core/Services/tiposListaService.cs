using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using System;

namespace Aguila.Core.Services
{
    public class tiposListaService : ItiposListaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IlistasService _listasService;

        public tiposListaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IlistasService listaService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _listasService = listaService;
        }

        public PagedList<tiposLista> GetTiposLista(tiposListaQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipos = _unitOfWork.tiposListaRepository.GetAll();

            if (filter.idRecurso != null)
            {
                tipos = tipos.Where(x => x.idRecurso == filter.idRecurso);

            }

            if (filter.tipoDato != null)
            {
                tipos = tipos.Where(x => x.tipoDato.ToLower().Contains(filter.tipoDato.ToLower()));
            }

            if (filter.campo != null)
            {
                tipos = tipos.Where(x => x.campo.ToLower().Equals(filter.campo.ToLower()));
            }

            var pagedTipos = PagedList<tiposLista>.create(tipos, filter.PageNumber, filter.PageSize);

            return pagedTipos;
        }

        public async Task<tiposLista> GetTipoLista(int id)
        {
            return await _unitOfWork.tiposListaRepository.GetByID(id);
        }

        public async Task InsertTipoLista(tiposLista tipo)
        {
            tipo.id = 0;
            tipo.fechaCreacion = DateTime.Now;
            await _unitOfWork.tiposListaRepository.Add(tipo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoLista(tiposLista tipo)
        {
            var currentTipo = await _unitOfWork.tiposListaRepository.GetByID(tipo.id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Lista No Existente!....");
            }

            currentTipo.descripcion = tipo.descripcion;
            currentTipo.tipoDato = tipo.tipoDato;
            currentTipo.campo = tipo.campo;


            _unitOfWork.tiposListaRepository.Update(currentTipo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }


        public async Task<bool> DeleteTipoLista(int id)
        {
            var currentTipo = await _unitOfWork.tiposListaRepository.GetByID(id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Lista No Existente!....");
            }

            await _unitOfWork.tiposListaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }


        //busqueda personalizada de elementos de una lista por el id de tipo de lista, campo y id de empresa
        public PagedList<listas> GetLista(tiposListaQueryFilter filter)
        {

            var tiposListas =  GetTiposLista(filter); //se busca el tipo de lista por medio del id del recurso y el campo
            var tipoLista = tiposListas.FirstOrDefault();

            if (tipoLista == null) { throw new AguilaException("Tipo Lista No Existente! Revise sus Datos..."); }

            listasQueryFilter filterLista = new listasQueryFilter() { idTipoLista = tipoLista.id, idEmpresa=filter.idEmpresa }; //se prepara el filtro para buscar los items de las listas

            var itemsLista =  _listasService.GetListas(filterLista); //se obtienen los items de las listas        

            return itemsLista;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
