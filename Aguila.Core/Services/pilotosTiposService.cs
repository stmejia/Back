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
    public class pilotosTiposService : IpilotosTiposService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public pilotosTiposService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<pilotosTipos> GetPilotosTipos(pilotosTiposQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var pilotosTipos = _unitOfWork.pilotosTiposRepository.GetAll();

            if (filter.codigo != null)
            {
                pilotosTipos = pilotosTipos.Where(e => e.codigo == filter.codigo);
            }

            var pagedPilotosTipos = PagedList<pilotosTipos>.create(pilotosTipos, filter.PageNumber, filter.PageSize);

            return pagedPilotosTipos;
        }

        public async Task<pilotosTipos> GetPilotoTipo(int id)
        {
            return await _unitOfWork.pilotosTiposRepository.GetByID(id);
        }

        public async Task InsertPilotoTipo(pilotosTipos pilotoTipo)
        {
            //Insertamos la fecha de ingreso del registro
            pilotoTipo.id = 0;
            pilotoTipo.fechaCreacion = DateTime.Now;

            await _unitOfWork.pilotosTiposRepository.Add(pilotoTipo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePilotoTipo(pilotosTipos pilotoTipo)
        {
            var currentPilotoTipoDto = await _unitOfWork.pilotosTiposRepository.GetByID(pilotoTipo.id);
            if (currentPilotoTipoDto == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            //currentPilotoTipoDto.codigo = pilotoTipo.codigo;
            currentPilotoTipoDto.descripcion = pilotoTipo.descripcion;

            _unitOfWork.pilotosTiposRepository.Update(currentPilotoTipoDto);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeletePilotoTipo(int id)
        {
            var currentPilotoTipoDto = await _unitOfWork.pilotosTiposRepository.GetByID(id);
            if (currentPilotoTipoDto == null)
            {
                throw new AguilaException("Piloto no existente...");
            }

            await _unitOfWork.pilotosTiposRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
