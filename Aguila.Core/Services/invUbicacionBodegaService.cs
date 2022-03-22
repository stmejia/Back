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
    public class invUbicacionBodegaService : IinvUbicacionBodegaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public invUbicacionBodegaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<invUbicacionBodega> GetInvUbicacionBodega(invUbicacionBodegaQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var invUbicacionBodega = _unitOfWork.invUbicacionBodegaRepository.GetAll();

            if (filter.idBodega != null)
            {
                invUbicacionBodega = invUbicacionBodega.Where(e => e.idBodega == filter.idBodega);
            }

            if (filter.estante != null)
            {
                invUbicacionBodega = invUbicacionBodega.Where(e => e.estante == filter.estante);
            }

            if (filter.pasillo != null)
            {
                invUbicacionBodega = invUbicacionBodega.Where(e => e.pasillo == filter.pasillo);
            }

            if (filter.nivel != null)
            {
                invUbicacionBodega = invUbicacionBodega.Where(e => e.nivel == filter.nivel);
            }

            if (filter.lugar != null)
            {
                invUbicacionBodega = invUbicacionBodega.Where(e => e.lugar == filter.lugar);
            }

            if (filter.idProducto != null)
            {
                invUbicacionBodega = invUbicacionBodega.Where(e => e.idProducto == filter.idProducto);
            }

            var pagedInvUbicacionBodega = PagedList<invUbicacionBodega>.create(invUbicacionBodega, filter.PageNumber, filter.PageSize);
            return pagedInvUbicacionBodega;
        }

        public async Task<invUbicacionBodega> GetInvUbicacionBodega(int id)
        {
            return await _unitOfWork.invUbicacionBodegaRepository.GetByID(id);
        }

        public async Task InsertInvUbicacionBodega(invUbicacionBodega invUbicacionBodega)
        {
            //Insertamos la fecha de ingreso del registro
            invUbicacionBodega.id = 0;
            invUbicacionBodega.fechaCreacion = DateTime.Now;

            await _unitOfWork.invUbicacionBodegaRepository.Add(invUbicacionBodega);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateInvUbicacionBodega(invUbicacionBodega invUbicacionBodega)
        {
            var currentInvUbicacionBodega = await _unitOfWork.invUbicacionBodegaRepository.GetByID(invUbicacionBodega.id);
            if (currentInvUbicacionBodega == null)
            {
                throw new AguilaException("Ubicacion no existente...");
            }

            currentInvUbicacionBodega.idBodega = invUbicacionBodega.idBodega;
            currentInvUbicacionBodega.estante = invUbicacionBodega.estante;
            currentInvUbicacionBodega.pasillo = invUbicacionBodega.pasillo;
            currentInvUbicacionBodega.nivel = invUbicacionBodega.nivel;
            currentInvUbicacionBodega.lugar = invUbicacionBodega.lugar;
            currentInvUbicacionBodega.idProducto = invUbicacionBodega.idProducto;

            _unitOfWork.invUbicacionBodegaRepository.Update(currentInvUbicacionBodega);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteInvUbicacionBodega(int id)
        {
            var currentUbicacionBodega = await _unitOfWork.invUbicacionBodegaRepository.GetByID(id);
            if (currentUbicacionBodega == null)
            {
                throw new AguilaException("Ubicacion no existente...");
            }

            await _unitOfWork.invUbicacionBodegaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
