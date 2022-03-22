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
    public class tipoClientesService : ItipoClientesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoClientesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoClientes> GetTipoClientes(tipoClientesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipoClientes = _unitOfWork.tipoClientesRepository.GetAll();

            if (filter.codigo != null)
            {
                tipoClientes = tipoClientes.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.descripcion != null)
            {
                tipoClientes = tipoClientes.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            if (filter.naviera != null)
            {
                tipoClientes = tipoClientes.Where(e => e.naviera == filter.naviera);
            }

            var pagedTipoClientes = PagedList<tipoClientes>.create(tipoClientes, filter.PageNumber, filter.PageSize);

            return pagedTipoClientes;
        }

        public async Task<tipoClientes> GetTipoCliente(int id)
        {
            return await _unitOfWork.tipoClientesRepository.GetByID(id);
        }

        public async Task InsertTipoCliente(tipoClientes tipoCliente)
        {
            //Insertamos la fecha de ingreso del registro
            tipoCliente.id = 0;
            tipoCliente.fechaCreacion = DateTime.Now;

            await _unitOfWork.tipoClientesRepository.Add(tipoCliente);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoCliente(tipoClientes tipoCliente)
        {
            var currentTipoCliente = await _unitOfWork.tipoClientesRepository.GetByID(tipoCliente.id);
            if (currentTipoCliente == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            //currentTipoCliente.codigo = tipoCliente.codigo;
            currentTipoCliente.descripcion= tipoCliente.descripcion;
            currentTipoCliente.naviera= tipoCliente.naviera;

            _unitOfWork.tipoClientesRepository.Update(currentTipoCliente);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoCliente(int id)
        {
            var currentTipoCliente = await _unitOfWork.tipoClientesRepository.GetByID(id);
            if (currentTipoCliente == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            await _unitOfWork.tipoClientesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
