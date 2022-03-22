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
    public class clienteServiciosService : IclienteServicioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public clienteServiciosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<clienteServicios> GetClienteServicios(clienteServiciosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var clienteServicios = _unitOfWork.clienteServiciosRepository.GetAll();

            if (filter.idCliente != null)
            {
                clienteServicios = clienteServicios.Where(e => e.idCliente == filter.idCliente);
            }

            if (filter.idServicio != null)
            {
                clienteServicios = clienteServicios.Where(e => e.idServicio == filter.idServicio);
            }

            if (filter.precio != null)
            {
                clienteServicios = clienteServicios.Where(e => e.precio == filter.precio);
            }

            if (filter.vigenciaHasta != null)
            {
                clienteServicios = clienteServicios.Where(e => e.vigenciaHasta == filter.vigenciaHasta);
            }

            var pagedClienteServicios = PagedList<clienteServicios>.create(clienteServicios, filter.PageNumber, filter.PageSize);
            return pagedClienteServicios;
        }

        public async Task<clienteServicios> GetClienteServicio(int id)
        {
            return await _unitOfWork.clienteServiciosRepository.GetByID(id);
        }

        public async Task InsertClienteServicio(clienteServicios clienteServicio)
        {
            //Insertamos la fecha de ingreso del registro
            clienteServicio.id = 0;
            clienteServicio.fechaCreacion = DateTime.Now;

            await _unitOfWork.clienteServiciosRepository.Add(clienteServicio);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateClienteServicio(clienteServicios clienteServicio)
        {
            var currentClienteServicio = await _unitOfWork.clienteServiciosRepository.GetByID(clienteServicio.id);
            if (currentClienteServicio == null)
            {
                throw new AguilaException("Servicio no existente...");
            }

            currentClienteServicio.idCliente = clienteServicio.idCliente;
            currentClienteServicio.idServicio = clienteServicio.idServicio;
            currentClienteServicio.precio = clienteServicio.precio;
            currentClienteServicio.vigenciaHasta = clienteServicio.vigenciaHasta;

            _unitOfWork.clienteServiciosRepository.Update(currentClienteServicio);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteClienteServicio(int id)
        {
            var currentClienteServicio = await _unitOfWork.clienteServiciosRepository.GetByID(id);
            if (currentClienteServicio == null)
            {
                throw new AguilaException("Servicio no existente...");
            }

            await _unitOfWork.clienteServiciosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
