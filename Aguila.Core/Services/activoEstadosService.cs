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
    public class activoEstadosService : IactivoEstadosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public activoEstadosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<activoEstados> GetActivoEstados(activoEstadosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var activoEstados = _unitOfWork.activoEstadosRepository.GetAll();

            if (filter.idActivo != null)
            {
                activoEstados = activoEstados.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idEstado != null)
            {
                activoEstados = activoEstados.Where(e => e.idEstado == filter.idEstado);
            }

            if (filter.observacion != null)
            {
                activoEstados = activoEstados.Where(e => e.observacion.ToLower().Contains(filter.observacion.ToLower()));
            }

            var pagedActivoEstados = PagedList<activoEstados>.create(activoEstados, filter.PageNumber, filter.PageSize);
            return pagedActivoEstados;
        }

        public async Task<activoEstados> GetActivoEstado(int id)
        {
            return await _unitOfWork.activoEstadosRepository.GetByID(id);
        }

        public async Task InsertActivoEstado(activoEstados activoEstado)
        {
            //Insertamos la fecha de ingreso del registro
            activoEstado.id = 0;
            activoEstado.fechaCreacion = DateTime.Now;

            await _unitOfWork.activoEstadosRepository.Add(activoEstado);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateActivoEstado(activoEstados activoEstado)
        {
            var currentActivoEstado = await _unitOfWork.activoEstadosRepository.GetByID(activoEstado.id);
            if (currentActivoEstado == null)
            {
                throw new AguilaException("Estado de activo no existente...");
            }

            currentActivoEstado.idActivo = activoEstado.idActivo;
            currentActivoEstado.idEstado = activoEstado.idEstado;
            currentActivoEstado.observacion = activoEstado.observacion;

            _unitOfWork.activoEstadosRepository.Update(currentActivoEstado);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteActivoEstado(int id)
        {
            var currentCategoria = await _unitOfWork.invCategoriaRepository.GetByID(id);
            if (currentCategoria == null)
            {
                throw new AguilaException("Estado de activo no existente...");
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
