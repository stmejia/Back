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
    public class tipoMecanicosService : ItipoMecanicosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoMecanicosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoMecanicos> GetTipoMecanicos(tipoMecanicosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipoMovimientos = _unitOfWork.tipoMecanicosRepository.GetAll();

            if (filter.codigo != null)
            {
                tipoMovimientos = tipoMovimientos.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.nombre != null)
            {
                tipoMovimientos = tipoMovimientos.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.especialidad != null)
            {
                tipoMovimientos = tipoMovimientos.Where(e => e.especialidad.ToLower().Contains(filter.especialidad.ToLower()));
            }

            var pagedTipoMovimientos = PagedList<tipoMecanicos>.create(tipoMovimientos, filter.PageNumber, filter.PageSize);
            return pagedTipoMovimientos;
        }

        public async Task<tipoMecanicos> GetTipoMecanico(int id)
        {
            return await _unitOfWork.tipoMecanicosRepository.GetByID(id);
        }

        public async Task InsertTipoMecanico(tipoMecanicos tipoMecanico)
        {
            //Insertamos la fecha de ingreso del registro
            tipoMecanico.id = 0;
            tipoMecanico.fechaCreacion = DateTime.Now;

            await _unitOfWork.tipoMecanicosRepository.Add(tipoMecanico);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoMecanico(tipoMecanicos tipoMecanico)
        {
            var currentTipoMecanico = await _unitOfWork.tipoMecanicosRepository.GetByID(tipoMecanico.id);

            if (currentTipoMecanico == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            currentTipoMecanico.nombre = tipoMecanico.nombre;
            currentTipoMecanico.descripcion = tipoMecanico.descripcion;
            currentTipoMecanico.especialidad = tipoMecanico.especialidad;

            _unitOfWork.tipoMecanicosRepository.Update(currentTipoMecanico);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoMecanico(int id)
        {
            var currentTipoMecanico = await _unitOfWork.tipoMecanicosRepository.GetByID(id);
            if (currentTipoMecanico == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            await _unitOfWork.tipoMecanicosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
