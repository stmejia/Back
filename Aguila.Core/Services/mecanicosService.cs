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
    public class mecanicosService : ImecanicosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public mecanicosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<mecanicos> GetMecanicos(mecanicosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var mecanicos = _unitOfWork.mecanicosRepository.GetAll();

            if (filter.codigo != null)
            {
                mecanicos = mecanicos.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idTipoMecanico != null)
            {
                mecanicos = mecanicos.Where(e => e.idTipoMecanico == filter.idTipoMecanico);
            }

            if (filter.idEmpleado != null)
            {
                mecanicos = mecanicos.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            if (filter.fechaBaja != null)
            {
                mecanicos = mecanicos.Where(e => e.fechaBaja == filter.fechaBaja);
            }

            var pagedMecanicos = PagedList<mecanicos>.create(mecanicos, filter.PageNumber, filter.PageSize);
            return pagedMecanicos;
        }

        public async Task<mecanicos> GetMecanico(int id)
        {
            return await _unitOfWork.mecanicosRepository.GetByID(id);
        }

        public async Task InsertMecanico(mecanicos mecanico)
        {
            //Insertamos la fecha de ingreso del registro
            mecanico.id = 0;
            mecanico.fechaCreacion = DateTime.Now;

            await _unitOfWork.mecanicosRepository.Add(mecanico);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateMecanico(mecanicos mecanico)
        {
            var currentMecanico = await _unitOfWork.mecanicosRepository.GetByID(mecanico.id);
            if (currentMecanico == null)
            {
                throw new AguilaException("Mecanico no existente...");
            }

            currentMecanico.idTipoMecanico = mecanico.idTipoMecanico;
            currentMecanico.idEmpleado = mecanico.idEmpleado;
            currentMecanico.fechaBaja = mecanico.fechaBaja;

            _unitOfWork.mecanicosRepository.Update(currentMecanico);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteMecanico(int id)
        {
            var currentMecanico = await _unitOfWork.mecanicosRepository.GetByID(id);
            if (currentMecanico == null)
            {
                throw new AguilaException("Mecanico no existente...");
            }

            await _unitOfWork.mecanicosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
