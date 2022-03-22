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
    public class detalleCondicionService : IdetalleCondicionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public detalleCondicionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<detalleCondicion> GetCondicionDetalle(detalleCondicionQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var detalleCondicion = _unitOfWork.detalleCondicionRepository.GetAllIncludes();

            if (filter.idUsuario != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.idUsuario == filter.idUsuario);
            }

            if (filter.idUsuarioAutoriza != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.idUsuarioAutoriza == filter.idUsuarioAutoriza);
            }

            if (filter.idCondicion != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.idCondicion == filter.idCondicion);
            }

            if (filter.idReparacion != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.idReparacion == filter.idReparacion);
            }

            if (filter.cantidad != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.cantidad == filter.cantidad);
            }

            if (filter.aprobado != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.aprobado == filter.aprobado);
            }

            if (filter.nombreAutoriza != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.nombreAutoriza.ToLower().Trim().Contains(filter.nombreAutoriza.ToLower().Trim()));
            }

            if (filter.observaciones != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.observaciones.ToLower().Trim().Contains(filter.observaciones.ToLower().Trim()));
            }

            if (filter.fechaAprobacion != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.fechaAprobacion == filter.fechaAprobacion);
            }

            if (filter.fechaEstimadoReparacion != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.fechaEstimadoReparacion == filter.fechaEstimadoReparacion);
            }

            if (filter.fechaFinalizacionRep != null)
            {
                detalleCondicion = detalleCondicion.Where(e => e.fechaFinalizacionRep == filter.fechaFinalizacionRep);
            }

            var pagedDetalleCondicion = PagedList<detalleCondicion>.create(detalleCondicion, filter.PageNumber, filter.PageSize);
            return pagedDetalleCondicion;
        }

        public async Task<detalleCondicion> GetCondicionDetalle(long id)
        {
            var detalleCondicion = await _unitOfWork.detalleCondicionRepository.GetByIdIncludes(id);

            return detalleCondicion;
        }

        public async Task InsertCondicionDetalle(detalleCondicion detalleCondicion)
        {
            detalleCondicion.id = 0;
            detalleCondicion.fechaCreacion = DateTime.Now;

            var currentCondicion = await _unitOfWork.condicionTallerVehiculoRepository.GetByID(detalleCondicion.idCondicion);
            if (currentCondicion == null)
                throw new AguilaException("Condicion de taller No Existente!...");

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.detalleCondicionRepository.Add(detalleCondicion);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("No ha sido posible registrar el detalle", ex); 
            }
        }

        public async Task<bool> UpdateCondicionDetalle(detalleCondicion detalleCondicion)
        {
            var currentDetalle = await _unitOfWork.detalleCondicionRepository.GetByID(detalleCondicion.id);
            if (currentDetalle == null)
                throw new AguilaException("Detalle no existente");

            currentDetalle.idUsuario = detalleCondicion.idUsuario;
            currentDetalle.idUsuarioAutoriza = detalleCondicion.idUsuarioAutoriza;
            currentDetalle.idCondicion = detalleCondicion.idCondicion;
            currentDetalle.idReparacion = detalleCondicion.idReparacion;
            currentDetalle.cantidad = detalleCondicion.cantidad;
            currentDetalle.aprobado = detalleCondicion.aprobado;
            currentDetalle.nombreAutoriza = detalleCondicion.nombreAutoriza;
            currentDetalle.observaciones = detalleCondicion.observaciones;
            currentDetalle.fechaAprobacion = currentDetalle.fechaAprobacion;

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.detalleCondicionRepository.Update(currentDetalle);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("No se ha podido actualizar el detalle", ex);
            }

            return true;
        }

        public async Task<bool> DeleteCondicionDetalle(long id)
        {
            var currentDetalle = _unitOfWork.detalleCondicionRepository.GetByID(id);
            if (currentDetalle == null)
                throw new AguilaException("Detalle no existente...");

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.detalleCondicionRepository.Delete(id);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();                
            }

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
