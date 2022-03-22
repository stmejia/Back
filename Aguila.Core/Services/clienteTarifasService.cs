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
    public class clienteTarifasService : IclienteTarifasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public clienteTarifasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<clienteTarifas> GetClienteTarifas(clienteTarifasQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var clienteTarifas = _unitOfWork.clienteTarifasRepository.GetAll();

            if (filter.idCliente != null)
            {
                clienteTarifas = clienteTarifas.Where(e => e.idCliente == filter.idCliente);
            }

            if (filter.idTarifa != null)
            {
                clienteTarifas = clienteTarifas.Where(e => e.idTarifa == filter.idTarifa);
            }

            if (filter.precio != null)
            {
                clienteTarifas = clienteTarifas.Where(e => e.precio == filter.precio);
            }

            if (filter.activa != null)
            {
                clienteTarifas = clienteTarifas.Where(e => e.activa == filter.activa);
            }

            clienteTarifas = clienteTarifas.OrderByDescending(e => e.vigenciaHasta);

            var pagedClienteTarifas = PagedList<clienteTarifas>.create(clienteTarifas, filter.PageNumber, filter.PageSize);
            return pagedClienteTarifas;
        }

        public async Task<clienteTarifas> GetClienteTarifa(int id)
        {
            return await _unitOfWork.clienteTarifasRepository.GetByID(id);
        }

        public async Task InsertClienteTarifa(clienteTarifas clienteTarifa)
        {
            //Insertamos la fecha de ingreso del registro
            clienteTarifa.id = 0;
            clienteTarifa.fechaCreacion = DateTime.Now;

            //Buscamos si existe el registro
            var filtro = new clienteTarifasQueryFilter { idTarifa = clienteTarifa.idTarifa, idCliente = clienteTarifa.idCliente, activa = true };
            var clienteTarifas = GetClienteTarifas(filtro);

            //Si encuentra un registro lo cancela 
            if (clienteTarifas.LongCount() > 0)
            {
                clienteTarifas.ElementAtOrDefault(0).vigenciaHasta = DateTime.Now;
                clienteTarifas.ElementAtOrDefault(0).activa = false;
                await UpdateClienteTarifa(clienteTarifas.ElementAtOrDefault(0));
            }

            clienteTarifa.activa = true;
            await _unitOfWork.clienteTarifasRepository.Add(clienteTarifa);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateClienteTarifa(clienteTarifas clienteTarifa)
        {
            var currentClienteTarifa = await _unitOfWork.clienteTarifasRepository.GetByID(clienteTarifa.id);
            if (currentClienteTarifa == null)
            {
                throw new AguilaException("Tarifa no existente...");
            }

            currentClienteTarifa.idCliente = clienteTarifa.idCliente;
            currentClienteTarifa.idTarifa = clienteTarifa.idTarifa;
            currentClienteTarifa.precio = clienteTarifa.precio;
            currentClienteTarifa.activa = clienteTarifa.activa;
            currentClienteTarifa.vigenciaHasta = clienteTarifa.vigenciaHasta;

            _unitOfWork.clienteTarifasRepository.Update(currentClienteTarifa);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteClienteTarifa(int id)
        {
            var currentClienteTarifa = await _unitOfWork.clienteTarifasRepository.GetByID(id);
            if (currentClienteTarifa == null)
            {
                throw new AguilaException("Tarifa no existente...");
            }

            await _unitOfWork.clienteTarifasRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
