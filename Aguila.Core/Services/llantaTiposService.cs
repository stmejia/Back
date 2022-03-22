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
    public class llantaTiposService : IllantaTiposService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public llantaTiposService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<llantaTipos> GetLlantaTipos(llantaTiposQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var llantaTipos = _unitOfWork.llantaTiposRepository.GetAll();

            if (filter.codigo != null)
            {
                llantaTipos = llantaTipos.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.descripcion != null)
            {
                llantaTipos = llantaTipos.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            //if (filter.proposito != null)
            //{
            //    llantaTipos = llantaTipos.Where(e => e.proposito == filter.proposito);
            //}

            var pagedLlantaTipos = PagedList<llantaTipos>.create(llantaTipos, filter.PageNumber, filter.PageSize);
            return pagedLlantaTipos;
        }

        public async Task<llantaTipos> GetLlantaTipo(int id)
        {
            return await _unitOfWork.llantaTiposRepository.GetByID(id);
        }

        public async Task InsertLlantaTipo(llantaTipos llantaTipo)
        {
            //Insertamos la fecha de ingreso del registro
            llantaTipo.id = 0;
            llantaTipo.fechaCreacion = DateTime.Now;

            await _unitOfWork.llantaTiposRepository.Add(llantaTipo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateLlantaTipo(llantaTipos llantaTipo)
        {
            var currentLlantaTipo = await _unitOfWork.llantaTiposRepository.GetByID(llantaTipo.id);

            if (currentLlantaTipo == null)
            {
                throw new AguilaException("Tipo de Llanta no existente...");
            }

            //currentLlantaTipo.codigo = llantaTipo.codigo;
            currentLlantaTipo.descripcion = llantaTipo.descripcion;
            //currentLlantaTipo.proposito = llantaTipo.proposito;

            _unitOfWork.llantaTiposRepository.Update(currentLlantaTipo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteLlantaTipo(int id)
        {
            var currentLlantaTipo = await _unitOfWork.llantaTiposRepository.GetByID(id);
            if (currentLlantaTipo == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            await _unitOfWork.llantaTiposRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
