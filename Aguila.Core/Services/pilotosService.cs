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
    public class pilotosService : IpilotosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public pilotosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<pilotos> GetPilotos(pilotosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var pilotos = _unitOfWork.pilotosRepository.GetAll();

            if (filter.codigoPiloto != null)
            {
                pilotos = pilotos.Where(e => e.codigoPiloto.ToLower().Contains(filter.codigoPiloto.ToLower()));
            }

            if (filter.idEmpleado != null)
            {
                pilotos = pilotos.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            if (filter.idTipoPilotos != null)
            {
                pilotos = pilotos.Where(e => e.idTipoPilotos == filter.idTipoPilotos);
            }

            var pagedPilotos = PagedList<pilotos>.create(pilotos, filter.PageNumber, filter.PageSize);
            return pagedPilotos;
        }

        public async Task<pilotos> GetPiloto(int id)
        {
            return await _unitOfWork.pilotosRepository.GetByID(id);
        }

        public async Task InsertPiloto(pilotos piloto)
        {
            //Insertamos la fecha de ingreso del registro
            piloto.id = 0;
            piloto.fechaCreacion = DateTime.Now;

            await _unitOfWork.pilotosRepository.Add(piloto);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePiloto(pilotos piloto)
        {
            var currentPiloto = await _unitOfWork.pilotosRepository.GetByID(piloto.id);
            if (currentPiloto == null)
            {
                throw new AguilaException("Piloto no existente...");
            }

            currentPiloto.idTipoPilotos = piloto.idTipoPilotos;
            currentPiloto.idEmpleado = piloto.idEmpleado;
            currentPiloto.fechaIngreso = piloto.fechaIngreso;
            currentPiloto.fechaBaja = piloto.fechaBaja;

            _unitOfWork.pilotosRepository.Update(currentPiloto);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeletePiloto(int id)
        {
            var currentPiloto = await _unitOfWork.pilotosRepository.GetByID(id);
            if (currentPiloto == null)
            {
                throw new AguilaException("Piloto no existente...");
            }

            await _unitOfWork.pilotosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
