using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
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
    public class condicionTallerVehiculoService : IcondicionTallerVehiculoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public condicionTallerVehiculoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<PagedList<condicionTallerVehiculo>> GetCondicionTallerVehiculo(condicionTallerVehiculoQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var condicionTaller = _unitOfWork.condicionTallerVehiculoRepository.GetAllIncludes();

            if (filter.idActivo != null)
            {
                condicionTaller = condicionTaller.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idEmpleado != null)
            {
                condicionTaller = condicionTaller.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            if (filter.idUsuario != null)
            {
                condicionTaller = condicionTaller.Where(e => e.idUsuario == filter.idUsuario);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionTaller = condicionTaller.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.serie != null)
            {
                condicionTaller = condicionTaller.Where(e => e.serie.ToLower().Trim().Contains(filter.serie.ToLower().Trim()));
            }

            if (filter.numero != null)
            {
                condicionTaller = condicionTaller.Where(e => e.numero == filter.numero);
            }

            if (filter.vidrios != null)
            {
                condicionTaller = condicionTaller.Where(e => e.vidrios.ToLower().Trim().Contains(filter.serie));
            }

            if (filter.llantas != null)
            {
                condicionTaller = condicionTaller.Where(e => e.llantas.ToLower().Trim().Contains(filter.llantas.ToLower().Trim()));
            }

            if (filter.tanqueCombustible != null)
            {
                condicionTaller = condicionTaller.Where(e => e.llantas.ToLower().Trim().Contains(filter.llantas.ToLower().Trim()));
            }

            if (filter.observaciones != null)
            {
                condicionTaller = condicionTaller.Where(e => e.observaciones.ToLower().Trim().Contains(filter.observaciones.ToLower().Trim()));
            }

            var pagedCondicionTaller = PagedList<condicionTallerVehiculo>.create(condicionTaller, filter.PageNumber, filter.PageSize);
            return pagedCondicionTaller;
        }

        public async Task<condicionTallerVehiculo> GetCondicionTallerVehiculo(int idCondicion)
        {
            var condicionTaller = await _unitOfWork.condicionTallerVehiculoRepository.GetByIdIncludes(idCondicion);

            return condicionTaller;
        }

        public async Task InsertCondicionTallerVehiculo(condicionTallerVehiculo condicionTallerVehiculo)
        {
            condicionTallerVehiculo.id = 0;
            condicionTallerVehiculo.fechaCreacion = DateTime.Now;

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.condicionTallerVehiculoRepository.Add(condicionTallerVehiculo);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("No ha sido posible registrar la condicion", ex);
            }
        }

        public async Task<bool> UpdateCondicionTallerVehiculo(condicionTallerVehiculo condicionTallerVehiculo)
        {
            var currentCondicionTaller = await _unitOfWork.condicionTallerVehiculoRepository.GetByID(condicionTallerVehiculo.id);
            if (currentCondicionTaller == null)
                throw new AguilaException("Condicion no existente");

            currentCondicionTaller.idActivo = condicionTallerVehiculo.idActivo;
            currentCondicionTaller.idEmpleado = condicionTallerVehiculo.idEmpleado;
            currentCondicionTaller.idUsuario = condicionTallerVehiculo.idUsuario;
            currentCondicionTaller.idEstacionTrabajo = condicionTallerVehiculo.idEstacionTrabajo;
            currentCondicionTaller.serie = condicionTallerVehiculo.serie;
            currentCondicionTaller.numero = condicionTallerVehiculo.numero;
            currentCondicionTaller.vidrios = condicionTallerVehiculo.vidrios;
            currentCondicionTaller.llantas = condicionTallerVehiculo.llantas;
            currentCondicionTaller.tanqueCombustible = condicionTallerVehiculo.tanqueCombustible;
            currentCondicionTaller.observaciones = condicionTallerVehiculo.observaciones;
            currentCondicionTaller.fechaAprobacion = condicionTallerVehiculo.fechaAprobacion;
            currentCondicionTaller.fechaRechazo = condicionTallerVehiculo.fechaRechazo;
            currentCondicionTaller.fechaSalida = condicionTallerVehiculo.fechaSalida;

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.condicionTallerVehiculoRepository.Update(currentCondicionTaller);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();                
            }

            return true;
        }

        public async Task<bool> DeleteCondicionTallerVehiculo(int id)
        {
            var currentCondicionTaller = _unitOfWork.condicionTallerVehiculoRepository.Delete(id);
            if (currentCondicionTaller == null)
                throw new AguilaException("Condicion no existente...");

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.condicionTallerVehiculoRepository.Delete(id);
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
